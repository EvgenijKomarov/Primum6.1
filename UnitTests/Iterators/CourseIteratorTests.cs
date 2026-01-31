using CoreConnection.DTOs.Inputs;
using CoreConnection.Enums;
using Moq;
using Moq.EntityFrameworkCore;
using PrimumCore.Models;
using PrimumCore.Services.Iterators;
using PrimumPlatformModel.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Iterators
{
    public class CourseIteratorTests
    {
        private Mock<IPrimumContext> _mockContext = null!;
        private CourseIterator _iterator = null!;

        [SetUp]
        public void Setup()
        {
            _mockContext = new Mock<IPrimumContext>();
            _iterator = new CourseIterator(_mockContext.Object);
        }

        #region Вспомогательный метод для создания учителя

        private User CreateTeacherUser(int userId, string displayName, bool isApproved)
        {
            var status = isApproved ? ApproveStatus.Approved : ApproveStatus.NeedModeratorReview;
            return new User
            {
                Id = userId,
                TeacherProfile = new TeacherProfile
                {
                    TeacherId = userId,
                    User = new User { Id = userId },
                    Courses = new List<Course>(),
                    ApproveStatus = status
                }
            };
        }

        #endregion

        #region GetCoursesByTeacher

        [Test]
        public async Task GetCoursesByTeacher_WhenTeacherExists_ReturnsMappedCourses()
        {
            var courseTheme = new CourseTheme { CourseThemeId = 10, ThemeName = "Web" };
            var teacherUser = new User { Id = 101 };
            var course = new Course
            {
                CourseId = 201,
                Name = "ASP.NET Core",
                Price = 1500,
                MaxLessons = 10,
                FreeLessons = 2,
                IsActive = true,
                ApproveStatus = ApproveStatus.Approved, // → IsAvailable = true
                Teacher = new TeacherProfile { TeacherId = 101, User = teacherUser, About = "Senior dev" },
                CourseTheme = courseTheme,
                CourseThemeId = courseTheme.CourseThemeId
            };

            var user = CreateTeacherUser(101, "Иван", isApproved: true);
            user.TeacherProfile.Courses.Add(course);

            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new[] { user });

            var result = await _iterator.GetCoursesByTeacher(101, isOnlyAvailable: false);
            var dto = result.Single();

            Assert.Multiple(() =>
            {
                Assert.That(dto.CourseId, Is.EqualTo(201));
                Assert.That(dto.Name, Is.EqualTo("ASP.NET Core"));
                Assert.That(dto.CourseThemeName, Is.EqualTo("Web"));
                Assert.That(dto.Price, Is.EqualTo(1500));
                Assert.That(dto.IsActive, Is.True);
                Assert.That(dto.ApproveStatus, Is.EqualTo(StatusApprove.Approved));
            });
        }

        [Test]
        public async Task GetCoursesByTeacher_RespectsIsOnlyAvailableFilter()
        {
            var user = CreateTeacherUser(102, "Тест", isApproved: true);
            user.TeacherProfile.Courses = new[]
            {
                new Course { CourseId = 202, Name = "Доступный", ApproveStatus = ApproveStatus.Approved, IsActive = true, Teacher = user.TeacherProfile, CourseTheme = new CourseTheme { ThemeName = "Тема" } },
                new Course { CourseId = 203, Name = "Неодобренный", ApproveStatus = ApproveStatus.NeedModeratorReview, IsActive = true, Teacher = user.TeacherProfile, CourseTheme = new CourseTheme { ThemeName = "Тема" } },
                new Course { CourseId = 204, Name = "Неактивный", ApproveStatus = ApproveStatus.Approved, IsActive = false, Teacher = user.TeacherProfile, CourseTheme = new CourseTheme { ThemeName = "Тема" } }
            };

            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new[] { user });

            var all = (await _iterator.GetCoursesByTeacher(102, isOnlyAvailable: false)).ToList();
            var onlyAvailable = (await _iterator.GetCoursesByTeacher(102, isOnlyAvailable: true)).ToList();

            Assert.That(all.Count, Is.EqualTo(3));
            Assert.That(onlyAvailable.Count, Is.EqualTo(1));
            Assert.That(onlyAvailable[0].Name, Is.EqualTo("Доступный")); // только Approved + Active
        }

        [Test]
        public void GetCoursesByTeacher_WhenTeacherNotFound_ThrowsException()
        {
            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new List<User>());

            Assert.ThrowsAsync<Exception>(async () => await _iterator.GetCoursesByTeacher(999, false));
        }

        #endregion

        #region GetCourse

        [Test]
        public async Task GetCourse_WhenCourseExistsAndAvailable_ReturnsDto()
        {
            var course = new Course
            {
                CourseId = 301,
                Name = "Python",
                ApproveStatus = ApproveStatus.Approved,
                IsActive = true, // → IsAvailable = true
                Price = 1000,
                Teacher = new TeacherProfile { User = new User { Id = 201 } },
                CourseTheme = new CourseTheme { ThemeName = "Data Science", CourseThemeId = 20 }
            };

            _mockContext.Setup(x => x.Set<Course>())
                .ReturnsDbSet(new[] { course });

            var result = await _iterator.GetCourse(301);

            Assert.That(result.CourseId, Is.EqualTo(301));
        }

        [Test]
        public void GetCourse_WhenCourseNotAvailable_ThrowsException()
        {
            // Не одобрен
            var course1 = new Course
            {
                CourseId = 302,
                ApproveStatus = ApproveStatus.NeedModeratorReview,
                IsActive = true,
                Teacher = new TeacherProfile { User = new User() },
                CourseTheme = new CourseTheme()
            };
            // Одобрен, но не активен
            var course2 = new Course
            {
                CourseId = 303,
                ApproveStatus = ApproveStatus.Approved,
                IsActive = false,
                Teacher = new TeacherProfile { User = new User() },
                CourseTheme = new CourseTheme()
            };

            _mockContext.Setup(x => x.Set<Course>())
                .ReturnsDbSet(new[] { course1, course2 });

            Assert.ThrowsAsync<Exception>(async () => await _iterator.GetCourse(302));
            Assert.ThrowsAsync<Exception>(async () => await _iterator.GetCourse(303));
        }

        #endregion

        #region EditCourse

        [Test]
        public async Task EditCourse_UpdatesCourseFields_WhenTeacherIsApproved()
        {
            var user = CreateTeacherUser(301, "Борис", isApproved: true);
            var course = new Course
            {
                CourseId = 601,
                Price = 1000,
                MaxLessons = 5,
                FreeLessons = 1,
                Teacher = user.TeacherProfile,
                ApproveStatus = ApproveStatus.Approved,
                IsActive = true
            };
            user.TeacherProfile.Courses.Add(course);

            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new[] { user });

            var inputDto = new CourseInputDto
            {
                Price = 2000,
                MaxLessons = 8,
                FreeLessons = 3
            };

            var result = await _iterator.EditCourse(301, 601, inputDto);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.EqualTo(601));
                Assert.That(course.Price, Is.EqualTo(2000));
                Assert.That(course.MaxLessons, Is.EqualTo(8));
                Assert.That(course.FreeLessons, Is.EqualTo(3));
            });
            _mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<System.Threading.CancellationToken>()), Times.Once);
        }

        [Test]
        public void EditCourse_WhenTeacherNotApproved_ThrowsException()
        {
            var user = CreateTeacherUser(302, "Неодобренный", isApproved: false); // ← не одобрен
            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new[] { user });

            var ex = Assert.ThrowsAsync<Exception>(async () => await _iterator.EditCourse(302, 1, new CourseInputDto()));
            Assert.That(ex?.Message, Does.Contain("not approved"));
        }

        [Test]
        public void EditCourse_WhenCourseNotFound_ThrowsException()
        {
            var user = CreateTeacherUser(303, "Одобренный", isApproved: true);
            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new[] { user });

            Assert.ThrowsAsync<Exception>(async () => await _iterator.EditCourse(303, 999, new CourseInputDto()));
        }

        #endregion

        #region CreateCourse

        [Test]
        public async Task CreateCourse_AddsNewCourse_WhenTeacherIsApproved()
        {
            var user = CreateTeacherUser(401, "Создатель", isApproved: true);
            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new[] { user });

            var inputDto = new CourseInputDto
            {
                Name = "Новый курс",
                Price = 1200,
                MaxLessons = 6,
                FreeLessons = 2,
                CourseThemeId = 70
            };

            await _iterator.CreateCourse(401, inputDto);

            Assert.That(user.TeacherProfile.Courses.Count, Is.EqualTo(1));
            var newCourse = user.TeacherProfile.Courses.First();
            Assert.Multiple(() =>
            {
                Assert.That(newCourse.Name, Is.EqualTo("Новый курс"));
                Assert.That(newCourse.Price, Is.EqualTo(1200));
                Assert.That(newCourse.CourseThemeId, Is.EqualTo(70));
                // По умолчанию: NeedModeratorReview, IsActive = true
                Assert.That(newCourse.ApproveStatus, Is.EqualTo(ApproveStatus.NeedModeratorReview));
                Assert.That(newCourse.IsActive, Is.True);
            });
            _mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<System.Threading.CancellationToken>()), Times.Once);
        }

        [Test]
        public void CreateCourse_WhenTeacherNotApproved_ThrowsException()
        {
            var user = CreateTeacherUser(402, "Заблокированный", isApproved: false);
            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new[] { user });

            Assert.ThrowsAsync<Exception>(async () => await _iterator.CreateCourse(402, new CourseInputDto()));
        }

        #endregion

        #region Activate/DeactivateCourse

        [Test]
        public async Task DeactivateCourse_SetsIsActiveToFalse()
        {
            var user = CreateTeacherUser(501, "Активатор", isApproved: true);
            var course = new Course
            {
                CourseId = 701,
                IsActive = true,
                ApproveStatus = ApproveStatus.Approved,
                Teacher = user.TeacherProfile
            };
            user.TeacherProfile.Courses.Add(course);

            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new[] { user });

            await _iterator.DeactivateCourse(501, 701);
            Assert.That(course.IsActive, Is.False);
        }

        [Test]
        public async Task ActivateCourse_SetsIsActiveToTrue()
        {
            var user = CreateTeacherUser(502, "Деактиватор", isApproved: true);
            var course = new Course
            {
                CourseId = 702,
                IsActive = false,
                ApproveStatus = ApproveStatus.Approved,
                Teacher = user.TeacherProfile
            };
            user.TeacherProfile.Courses.Add(course);

            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new[] { user });

            await _iterator.ActivateCourse(502, 702);
            Assert.That(course.IsActive, Is.True);
        }

        #endregion
    }
}
