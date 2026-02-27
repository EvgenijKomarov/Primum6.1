using CoreDBModel.Models;
using CoreDBModel.Models.Enums;
using Moq;
using Moq.EntityFrameworkCore;
using PrimumCore.Services.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Utilities
{
    public class IncidentCollectorTests
    {
        private Mock<PrimumContext> _mockContext;
        private IncidentCollector _collector;

        [SetUp]
        public void Setup()
        {
            _mockContext = new Mock<PrimumContext>();
            _collector = new IncidentCollector(_mockContext.Object);

            _mockContext.Setup(x => x.Set<IncidentLog>())
                .ReturnsDbSet(new List<IncidentLog> { });
        }

        #region ModerateTeachers

        [Test]
        public async Task GetIncedents_WithModerateTeachers_ReturnsTeacherIncidents()
        {
            // Arrange
            var teacherUser = new User
            {
                Id = 101,
                Name = "Иван",
                Surname = "Иванов",
                Patronymic = "Иванович",
                MailAdress = "ivan@example.com",
                TeacherProfile = new TeacherProfile
                {
                    ApproveStatus = ApproveStatus.NeedModeratorReview,
                    About = "Опытный учитель"
                }
            };

            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new List<User> { teacherUser });

            // Act
            var result = await _collector.GetIncedents(new[] { Permission.ModerateTeachers });

            // Assert
            Assert.That(result.Count, Is.EqualTo(1));
            var incident = result.First();
            Assert.That(incident.ObjectId, Is.EqualTo(101));
            Assert.That(incident.Meaning, Is.EqualTo(IncidentMeaning.Teacher));
            Assert.That(incident.Status, Is.EqualTo(IncidentStatus.NeedModeration));
            Assert.That(incident.CommonInfo, Does.Contain("Иванов"));
            Assert.That(incident.Decisions, Is.Not.Empty);
        }

        #endregion

        #region ModerateStudents

        [Test]
        public async Task GetIncedents_WithModerateStudents_ReturnsStudentIncidents()
        {
            var studentUser = new User
            {
                Id = 201,
                Name = "Анна",
                Surname = "Петрова",
                Patronymic = "Сергеевна",
                MailAdress = "anna@example.com",
                StudentProfile = new StudentProfile
                {
                    ApproveStatus = ApproveStatus.NeedModeratorReview
                }
            };

            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new List<User> { studentUser });

            var result = await _collector.GetIncedents(new[] { Permission.ModerateStudents });

            Assert.That(result.Count, Is.EqualTo(1));
            var incident = result.First();
            Assert.That(incident.ObjectId, Is.EqualTo(201));
            Assert.That(incident.Meaning, Is.EqualTo(IncidentMeaning.Student));
            Assert.That(incident.CommonInfo, Does.Contain("Петрова"));
        }

        #endregion

        #region InspectMissedLessons

        [Test]
        public async Task GetIncedents_WithInspectMissedLessons_ReturnsLessonIncidents()
        {
            var lesson = new Lesson
            {
                LessonId = 501,
                DateTime = new DateTime(2026, 1, 27, 15, 0, 0),
                Status = LessonStatus.Missed,
                Abonement = new Abonement
                {
                    Student = new StudentProfile
                    {
                        User = new User { Id = 201 }
                    },
                    Course = new Course
                    {
                        Name = "Математика",
                        Teacher = new TeacherProfile
                        {
                            User = new User { Id = 101 }
                        },
                        CourseTheme = new CourseTheme { ThemeName = "Алгебра" },
                        FreeLessons = 3
                    }
                }
            };

            _mockContext.Setup(x => x.Set<Lesson>())
                .ReturnsDbSet(new List<Lesson> { lesson });

            var result = await _collector.GetIncedents(new[] { Permission.InspectMissedLessons });

            Assert.That(result.Count, Is.EqualTo(1));
            var incident = result.First();
            Assert.That(incident.ObjectId, Is.EqualTo(501));
            Assert.That(incident.Meaning, Is.EqualTo(IncidentMeaning.Lesson));
            Assert.That(incident.CommonInfo, Does.Contain("15:00 27:01:2026"));
        }

        #endregion

        #region Unsupported Permission

        [Test]
        public async Task GetIncedents_WithUnsupportedPermission_IgnoresIt()
        {
            // Допустим, GivePermissions не в collectionRules
            var result = await _collector.GetIncedents(new[] { Permission.EditPermissions });

            Assert.That(result, Is.Empty);
        }

        #endregion

        #region No Matching Records

        [Test]
        public async Task GetIncedents_WhenNoRecords_ReturnsEmpty()
        {
            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new List<User>()); // пусто

            var result = await _collector.GetIncedents(new[] { Permission.ModerateTeachers });

            Assert.That(result, Is.Empty);
        }

        #endregion
    }
}
