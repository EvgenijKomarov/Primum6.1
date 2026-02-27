using CoreDBModel.Models;
using CoreDBModel.Models.Enums;
using Moq;
using Moq.EntityFrameworkCore;
using PrimumCore.Exceptions;
using PrimumCore.Services.Iterators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Iterators
{
    public class LessonIteratorTests
    {
        private Mock<PrimumContext> _mockContext = null!;
        private LessonIterator _iterator = null!;

        [SetUp]
        public void Setup()
        {
            _mockContext = new Mock<PrimumContext>();
            _iterator = new LessonIterator(_mockContext.Object);
        }

        #region GetAbonementLessons

        [Test]
        public async Task GetAbonementLessons_WhenAbonementExists_ReturnsMappedLessons()
        {
            // Arrange
            var grading = new StudentGrading
            {
                HomeworkGrade = Grading.Excellent,
                LessonActivityGrade = Grading.Good,
                RepetitionOfMaterialGrade = Grading.Excellent,
                StudyInitiativeGrade = Grading.Satisfactory
            };
            var lesson1 = new Lesson
            {
                DateTime = new DateTime(2026, 2, 1, 10, 0, 0),
                Price = 500,
                Status = LessonStatus.Happened,
                StudentLink = "https://meet.jit.si/student123?userType=guest",
                TeacherLink = "https://meet.jit.si/teacher123?userType=admin",
                Grading = grading
            };
            var lesson2 = new Lesson
            {
                DateTime = new DateTime(2026, 2, 8, 10, 0, 0),
                Price = 500,
                Status = LessonStatus.Waiting,
                StudentLink = null,
                TeacherLink = null,
                Grading = null
            };

            var abonement = new Abonement
            {
                AbonementId = 101,
                Student = new StudentProfile { User = new User { Id = 201 } },
                Course = new Course
                {
                    CourseId = 301,
                    Name = "Математика",
                    Teacher = new TeacherProfile { User = new User { Id = 401 } }
                },
                Lessons = new List<Lesson> { lesson1, lesson2 }
            };
            lesson1.Abonement = abonement;
            lesson2.Abonement = abonement;

            _mockContext.Setup(x => x.Set<Abonement>())
                .ReturnsDbSet(new[] { abonement });

            // Act
            var studentLinks = (await _iterator.GetAbonementLessons(101, isStudentLink: true)).ToList();
            var teacherLinks = (await _iterator.GetAbonementLessons(101, isStudentLink: false)).ToList();

            // Assert
            Assert.That(studentLinks.Count, Is.EqualTo(2));
            Assert.That(teacherLinks.Count, Is.EqualTo(2));

            var l1 = studentLinks.First(l => l.DateTime == lesson1.DateTime);
            Assert.Multiple(() =>
            {
                Assert.That(l1.CourseName, Is.EqualTo("Математика"));
                Assert.That(l1.LessonLink, Is.EqualTo(lesson1.StudentLink));
                Assert.That(l1.LessonStatus, Is.EqualTo(LessonStatus.Happened));
                Assert.That(l1.Grade, Is.Not.Null);
            });

            var l2 = teacherLinks.First(l => l.DateTime == lesson2.DateTime);
            Assert.That(l2.LessonLink, Is.EqualTo(lesson2.TeacherLink ?? null));
            Assert.That(l2.Grade, Is.Null);
        }

        [Test]
        public void GetAbonementLessons_WhenAbonementNotFound_ThrowsException()
        {
            // Arrange
            _mockContext.Setup(x => x.Set<Abonement>())
                .ReturnsDbSet(new List<Abonement>());

            // Act & Assert
            Assert.ThrowsAsync<NotFoundException>(async () => await _iterator.GetAbonementLessons(999, true));
        }

        #endregion

        #region GetTeacherLessons

        [Test]
        public async Task GetTeacherLessons_WhenTeacherExists_ReturnsFutureLessons()
        {
            // Arrange
            var pastLesson = new Lesson { DateTime = DateTime.Now.AddDays(-1), Price = 300 };
            var futureLesson = new Lesson
            {
                DateTime = DateTime.Now.AddDays(1),
                Price = 400,
                StudentLink = "student-link",
                TeacherLink = "teacher-link"
            };

            var abonement = new Abonement
            {
                AbonementId = 201,
                Student = new StudentProfile { User = new User {} },
                Course = new Course { CourseId = 302, Name = "Физика" },
                Lessons = new List<Lesson> { pastLesson, futureLesson }
            };
            futureLesson.Abonement = abonement;
            var course = new Course { CourseId = 302, Name = "Физика", Abonements = new List<Abonement> { abonement } };
            var teacherProfile = new TeacherProfile
            {
                Courses = new List<Course> { course }
            };
            var teacherUser = new User
            {
                Id = 501,
                TeacherProfile = teacherProfile
            };

            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new[] { teacherUser });

            // Act
            var result = (await _iterator.GetTeacherLessons(501)).ToList();

            // Assert
            Assert.That(result.Count, Is.EqualTo(1)); // только будущие уроки
            var lessonDto = result[0];
            Assert.Multiple(() =>
            {
                Assert.That(lessonDto.TeacherId, Is.EqualTo(501));
                Assert.That(lessonDto.LessonLink, Is.EqualTo("teacher-link")); // TeacherLink
                Assert.That(lessonDto.CourseName, Is.EqualTo("Физика"));
            });
        }

        [Test]
        public void GetTeacherLessons_WhenTeacherNotFound_ThrowsException()
        {
            // Arrange
            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new List<User>());

            // Act & Assert
            Assert.ThrowsAsync<NotFoundException>(async () => await _iterator.GetTeacherLessons(999));
        }

        #endregion

        #region GetStudentLessons

        [Test]
        public async Task GetStudentLessons_WhenStudentExists_ReturnsAllLessons()
        {
            // Arrange
            var lesson = new Lesson
            {
                DateTime = DateTime.Now.AddHours(-2),
                Price = 600,
                StudentLink = "my-student-link",
                TeacherLink = "their-teacher-link"
            };
            var abonement = new Abonement
            {
                AbonementId = 301,
                Course = new Course
                {
                    CourseId = 401,
                    Name = "Химия",
                    Teacher = new TeacherProfile { User = new User { Id = 601 } }
                },
                Lessons = new List<Lesson> { lesson }
            };
            lesson.Abonement = abonement;
            var studentProfile = new StudentProfile { Abonements = new List<Abonement> { abonement } };
            var studentUser = new User
            {
                Id = 701,
                StudentProfile = studentProfile
            };

            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new[] { studentUser });

            // Act
            var result = (await _iterator.GetStudentLessons(701)).ToList();

            // Assert
            Assert.That(result.Count, Is.EqualTo(1));
            var dto = result[0];
            Assert.Multiple(() =>
            {
                Assert.That(dto.StudentId, Is.EqualTo(701));
                Assert.That(dto.LessonLink, Is.EqualTo("my-student-link")); // StudentLink
                Assert.That(dto.CourseName, Is.EqualTo("Химия"));
            });
        }

        [Test]
        public void GetStudentLessons_WhenStudentNotFound_ThrowsException()
        {
            // Arrange
            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new List<User>());

            // Act & Assert
            Assert.ThrowsAsync<NotFoundException>(async () => await _iterator.GetStudentLessons(999));
        }

        #endregion
    }
}
