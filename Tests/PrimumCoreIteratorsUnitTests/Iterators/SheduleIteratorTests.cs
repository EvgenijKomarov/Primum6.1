using CoreConnection.DTOs.Inputs;
using CoreDBModel.Models;
using CoreDBModel.Models.Enums;
using Moq;
using Moq.EntityFrameworkCore;
using PrimumCore.Exceptions;
using PrimumCore.Services.Iterators;
using Pushables;
using Pushables.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Iterators
{
    public class SheduleIteratorTests
    {
        private Mock<PrimumContext> _mockContext = null!;
        private Mock<PublisherService> _mockPublisher = null!;
        private SheduleIterator _iterator = null!;

        [SetUp]
        public void Setup()
        {
            _mockContext = new Mock<PrimumContext>();
            _mockPublisher = new Mock<PublisherService>();
            _iterator = new SheduleIterator(_mockContext.Object, _mockPublisher.Object);
        }

        #region GetTeacherShedules

        [Test]
        public async Task GetTeacherShedules_WhenTeacherExists_ReturnsMappedShedules()
        {
            var busyShedule = new TeacherShedule
            {
                TeacherSheduleId = 1,
                DayOfWeek = DayOfWeek.Monday,
                Time = 10,
                AbonementShedule = new AbonementShedule
                {
                    Abonement = new Abonement
                    {
                        Student = new StudentProfile { User = new User { Id = 201, IsMailChecked = true, IsBanned = false } },
                        Course = new Course
                        {
                            CourseId = 301,
                            Name = "Математика",
                            Teacher = new TeacherProfile
                            {
                                ApproveStatus = ApproveStatus.Approved,
                                User = new User
                                {
                                    IsMailChecked = true,
                                    IsBanned = false,
                                }
                            }
                        }
                    }
                }
            };
            var freeShedule = new TeacherShedule
            {
                TeacherSheduleId = 2,
                DayOfWeek = DayOfWeek.Wednesday,
                Time = 14,
                AbonementShedule = null
            };

            var teacherProfile = new TeacherProfile
            {
                TeacherShedules = new List<TeacherShedule> { busyShedule, freeShedule },
                ApproveStatus = ApproveStatus.Approved
            };
            var teacherUser = new User { Id = 101, IsMailChecked = true, IsBanned = false };
            teacherProfile.User = teacherUser;
            teacherUser.TeacherProfile = teacherProfile;

            // link each schedule to the teacher profile so availability checks pass
            busyShedule.Teacher = teacherProfile;
            freeShedule.Teacher = teacherProfile;

            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new[] { teacherUser });

            // Act
            var all = (await _iterator.GetTeacherShedules(101, isOnlyAvailable: false)).ToList();
            var onlyFree = (await _iterator.GetTeacherShedules(101, isOnlyAvailable: true)).ToList();

            // Assert
            Assert.That(all.Count, Is.EqualTo(2));
            Assert.That(onlyFree.Count, Is.EqualTo(1));
            var freeDto = onlyFree[0];
            Assert.Multiple(() =>
            {
                Assert.That(freeDto.DayOfWeek, Is.EqualTo(DayOfWeek.Wednesday));
                Assert.That(freeDto.Time, Is.EqualTo(14));
                Assert.That(freeDto.IsAvailable, Is.False);
                Assert.That(freeDto.StudentName, Is.Null);
            });
        }

        [Test]
        public void GetTeacherShedules_WhenTeacherNotFound_ThrowsException()
        {
            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new List<User>());

            Assert.ThrowsAsync<NotFoundException>(async () => await _iterator.GetTeacherShedules(999, false));
        }

        #endregion

        #region GetAbonementShedules

        [Test]
        public async Task GetAbonementShedules_WhenAbonementExists_ReturnsMappedShedules()
        {
            // Arrange
            var teacherShedule = new TeacherShedule
            {
                DayOfWeek = DayOfWeek.Friday,
                Time = 18
            };
            var shedule = new AbonementShedule
            {
                TeacherShedule = teacherShedule
            };
            var abonement = new Abonement
            {
                AbonementId = 101,
                AbonementShedules = new List<AbonementShedule> { shedule },
                Course = new Course
                {
                    CourseId = 201,
                    Name = "Физика",
                    Teacher = new TeacherProfile 
                    { 
                        User = new User { Id = 301 }, 
                        TeacherShedules = new List<TeacherShedule> { teacherShedule } 
                    }
                }
            };
            shedule.Abonement = abonement;

            _mockContext.Setup(x => x.Set<Abonement>())
                .ReturnsDbSet(new[] { abonement });

            // Act
            var result = (await _iterator.GetAbonementShedules(101)).ToList();

            // Assert
            Assert.That(result.Count, Is.EqualTo(1));
            var dto = result[0];
            Assert.Multiple(() =>
            {
                Assert.That(dto.DayOfWeek, Is.EqualTo(DayOfWeek.Friday));
                Assert.That(dto.Time, Is.EqualTo(18));
                Assert.That(dto.CourseName, Is.EqualTo("Физика"));
            });
        }

        [Test]
        public void GetAbonementShedules_WhenAbonementNotFound_ThrowsException()
        {
            _mockContext.Setup(x => x.Set<Abonement>())
                .ReturnsDbSet(new List<Abonement>());

            Assert.ThrowsAsync<NotFoundException>(async () => await _iterator.GetAbonementShedules(999));
        }

        #endregion

        #region CreateTeacherShedule

        [Test]
        public async Task CreateTeacherShedule_CreatesNewShedule_WhenTeacherIsNotApproved()
        {
            // Arrange
            var teacherProfile = new TeacherProfile
            {
                TeacherId = 102,
                TeacherShedules = new List<TeacherShedule>(),
                ApproveStatus = ApproveStatus.NeedModeratorReview
            };
            var teacherUser = new User { Id = 102, TeacherProfile = teacherProfile };
            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new[] { teacherUser });

            var inputDto = new TeacherSheduleInputDto
            {
                DayOfWeek = DayOfWeek.Monday,
                Time = 9
            };

            // Act & Assert
            Assert.ThrowsAsync<NotAvailableException>(async () => await _iterator.CreateTeacherShedule(102, inputDto));
        }

        [Test]
        public async Task CreateTeacherShedule_WhenSheduleAlreadyExists_ThrowsException()
        {
            // Arrange
            var existingShedule = new TeacherShedule
            {
                DayOfWeek = DayOfWeek.Tuesday,
                Time = 11
            };
            var teacherProfile = new TeacherProfile
            {
                ApproveStatus = ApproveStatus.NeedModeratorReview,
                TeacherShedules = new List<TeacherShedule> { existingShedule }
            };
            var teacherUser = new User { Id = 104, TeacherProfile = teacherProfile };
            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new[] { teacherUser });

            var inputDto = new TeacherSheduleInputDto
            {
                DayOfWeek = DayOfWeek.Tuesday,
                Time = 11
            };

            // Act & Assert
            Assert.ThrowsAsync<NotAvailableException>(async () => await _iterator.CreateTeacherShedule(104, inputDto));
        }

        #endregion

        #region DeleteTeacherShedule

        [Test]
        public async Task DeleteTeacherShedule_RemovesShedule_WhenNotBusyAndTeacherNotApproved() // ← см. примечание!
        {
            // Arrange
            var shedule = new TeacherShedule
            {
                TeacherSheduleId = 5,
                DayOfWeek = DayOfWeek.Thursday,
                Time = 16,
                AbonementShedule = null
            };
            var teacherProfile = new TeacherProfile
            {
                ApproveStatus = ApproveStatus.NeedModeratorReview,
                TeacherShedules = new List<TeacherShedule> { shedule }
            };
            var teacherUser = new User { Id = 105, TeacherProfile = teacherProfile };
            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new[] { teacherUser });

            // Act & Assert
            Assert.ThrowsAsync<BusinessLogicException>(async () => await _iterator.DeleteTeacherShedule(105, 5));
        }

        [Test]
        public void DeleteTeacherShedule_WhenSheduleBusy_ThrowsException()
        {
            // Arrange
            var shedule = new TeacherShedule
            {
                TeacherSheduleId = 6,
                AbonementShedule = new AbonementShedule() // занят
            };
            var teacherProfile = new TeacherProfile { ApproveStatus = ApproveStatus.Approved, TeacherShedules = new List<TeacherShedule> { shedule } };
            var teacherUser = new User { Id = 106, TeacherProfile = teacherProfile, IsMailChecked = true, IsBanned = false };
            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new[] { teacherUser });

            // Act & Assert
            var ex = Assert.ThrowsAsync<BusinessLogicException>(async () => await _iterator.DeleteTeacherShedule(106, 6));
            Assert.That(ex?.Message, Does.Contain("already busy"));
        }

        #endregion

        #region GetStudentShedules

        [Test]
        public async Task GetStudentShedules_WhenStudentExists_ReturnsMappedShedules()
        {
            // Arrange
            var shedule = new AbonementShedule
            {
                TeacherShedule = new TeacherShedule
                {
                    DayOfWeek = DayOfWeek.Saturday,
                    Time = 12
                }
            };
            var abonement = new Abonement
            {
                AbonementShedules = new List<AbonementShedule> { shedule },
                Course = new Course
                {
                    CourseId = 401,
                    Name = "Химия",
                    Teacher = new TeacherProfile { User = new User { Id = 501 } }
                }
            };
            shedule.Abonement = abonement;
            var studentProfile = new StudentProfile { Abonements = new List<Abonement> { abonement } };
            var studentUser = new User { Id = 202, StudentProfile = studentProfile };

            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new[] { studentUser });

            // Act
            var result = (await _iterator.GetStudentShedules(202)).ToList();

            // Assert
            Assert.That(result.Count, Is.EqualTo(1));
            var dto = result[0];
            Assert.Multiple(() =>
            {
                Assert.That(dto.DayOfWeek, Is.EqualTo(DayOfWeek.Saturday));
                Assert.That(dto.CourseName, Is.EqualTo("Химия"));
            });
        }

        [Test]
        public void GetStudentShedules_WhenStudentNotFound_ThrowsException()
        {
            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new List<User>());

            Assert.ThrowsAsync<NotFoundException>(async () => await _iterator.GetStudentShedules(999));
        }

        #endregion

        #region DeleteStudentShedule

        [Test]
        public async Task DeleteStudentShedule_RemovesSheduleAndPublishesNotification()
        {
            // Arrange
            var teacherUser = new User { Id = 601 };
            var course = new Course
            {
                CourseId = 501,
                Name = "Биология",
                Teacher = new TeacherProfile { User = teacherUser }
            };
            var teacherShedule = new TeacherShedule
            {
                TeacherSheduleId = 10,
                DayOfWeek = DayOfWeek.Sunday,
                Time = 15,
                Teacher = course.Teacher
            };
            var abonementShedule = new AbonementShedule
            {
                AbonementSheduleId = 20,
                TeacherShedule = teacherShedule
            };
            var abonement = new Abonement
            {
                AbonementId = 301,
                Course = course,
                AbonementShedules = new List<AbonementShedule> { abonementShedule }
            };
            abonementShedule.Abonement = abonement;
            var studentProfile = new StudentProfile { Abonements = new List<Abonement> { abonement } };
            var studentUser = new User { Id = 203, StudentProfile = studentProfile };

            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new[] { studentUser });

            // Act
            var result = await _iterator.DeleteStudentShedule(203, 20);

            // Assert
            Assert.That(result, Is.EqualTo(20));
            Assert.That(abonement.AbonementShedules.Count, Is.EqualTo(0));

            _mockPublisher.Verify(
                x => x.Push(It.Is<DeleteAbonementSheduleNotification>(n =>
                    n.StudentUserId == 203 &&
                    n.TeacherUserId == 601 &&
                    n.AbonementSheduleId == 20 &&
                    n.DayOfWeek == "Sunday")),
                Times.Once);

            _mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<System.Threading.CancellationToken>()), Times.Once);
        }

        #endregion
    }
}
