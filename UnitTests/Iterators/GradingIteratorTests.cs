using CoreConnection.DTOs.Inputs;
using CoreConnection.Enums;
using Moq;
using Moq.EntityFrameworkCore;
using PrimumCore.Models;
using PrimumCore.Models.Enums;
using PrimumCore.Services.Iterators;
using PrimumPlatformModel.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Iterators
{
    public class GradingIteratorTests
    {
        private Mock<IPrimumContext> _mockContext = null!;
        private GradingIterator _iterator = null!;

        [SetUp]
        public void Setup()
        {
            _mockContext = new Mock<IPrimumContext>();
            _iterator = new GradingIterator(_mockContext.Object);
        }

        #region GradeLesson — успешный сценарий

        [Test]
        public async Task GradeLesson_WithValidData_GradesLessonAndAddsCoins()
        {
            // Arrange
            var studentProfile = new StudentProfile { Coins = 100 };
            var course = new Course
            {
                Teacher = new TeacherProfile
                {
                    User = new User { Id = 999 }
                }
            };
            var abonement = new Abonement
            {
                Course = course,
                Student = studentProfile
            };
            var lesson = new Lesson
            {
                LessonId = 501,
                Abonement = abonement,
                Price = 1000,
                Status = LessonStatus.Happened,
                Grading = null
            };

            _mockContext.Setup(x => x.Set<Lesson>())
                .ReturnsDbSet(new[] { lesson });
            var grades = new List<StudentGrading>();
            _mockContext.Setup(x => x.Set<StudentGrading>())
                .ReturnsDbSet(grades);

            var dto = new GradingInputDto
            {
                HomeworkGrade = (Grade)4,
                LessonActivityGrade = (Grade)5,
                RepetitionOfMaterialGrade = (Grade)3,
                StudyInitiativeGrade = (Grade)4
            };

            // Act
            var result = await _iterator.GradeLesson(teacherId: 999, lessonId: 501, dto);

            // Assert
            _mockContext.Verify(x => x.Set<StudentGrading>().Add(It.Is<StudentGrading>(x => 
                x.HomeworkGrade == (Grading)dto.HomeworkGrade &&
                x.LessonActivityGrade == (Grading)dto.LessonActivityGrade &&
                x.RepetitionOfMaterialGrade == (Grading)dto.RepetitionOfMaterialGrade &&
                x.StudyInitiativeGrade == (Grading)dto.StudyInitiativeGrade
            )));

            Assert.That(result, Is.EqualTo(501));
            Assert.That(lesson.Grading, Is.Not.Null);
            Assert.That(lesson.Grading.HomeworkGrade, Is.EqualTo((Grading)4));

            // Проверяем начисление монет
            // Средний балл = (4+5+3+4)/4 = 4.0
            // Cashback = (4.0 / 5) * 0.1 * 1000 = 80
            Assert.That(studentProfile.Coins, Is.EqualTo(100 + 80));
        }

        #endregion

        #region Проверки исключений

        [Test]
        public void GradeLesson_WhenLessonNotFound_ThrowsException()
        {
            // Arrange
            _mockContext.Setup(x => x.Set<Lesson>())
                .ReturnsDbSet(new List<Lesson>());

            // Act & Assert
            Assert.ThrowsAsync<Exception>(async () =>
                await _iterator.GradeLesson(1, 999, new GradingInputDto()));
        }

        [Test]
        public async Task GradeLesson_WhenTeacherIsOwner_ThrowsException()
        {
            // Arrange
            var user = new User { Id = 202 };
            var lesson = new Lesson
            {
                LessonId = 502,
                Abonement = new Abonement
                {
                    Course = new Course
                    {
                        Teacher = new TeacherProfile { User = user }
                    },
                    Student = new StudentProfile { User = user }
                },
                Status = LessonStatus.Happened
            };

            _mockContext.Setup(x => x.Set<Lesson>())
                .ReturnsDbSet(new[] { lesson });
            _mockContext.Setup(x => x.Set<StudentGrading>())
                .ReturnsDbSet(new List<StudentGrading>());

            // Act & Assert
            var ex = Assert.ThrowsAsync<Exception>(async () =>
                await _iterator.GradeLesson(teacherId: 202, lessonId: 502, new GradingInputDto()));
            Assert.That(ex?.Message, Does.Contain("can't grade"));
        }

        [Test]
        public async Task GradeLesson_WhenLessonAlreadyGraded_ThrowsException()
        {
            // Arrange
            var lesson = new Lesson
            {
                LessonId = 503,
                Abonement = new Abonement
                {
                    Course = new Course
                    {
                        Teacher = new TeacherProfile { User = new User { Id = 301 } }
                    }
                },
                Status = LessonStatus.Happened,
                Grading = new StudentGrading() // уже оценён
            };

            _mockContext.Setup(x => x.Set<Lesson>())
                .ReturnsDbSet(new[] { lesson });

            // Act & Assert
            var ex = Assert.ThrowsAsync<Exception>(async () =>
                await _iterator.GradeLesson(301, 503, new GradingInputDto()));
            Assert.That(ex?.Message, Does.Contain("already gradet")); // опечатка в коде — оставляем как есть
        }

        [Test]
        public async Task GradeLesson_WhenLessonNotHappened_ThrowsException()
        {
            // Arrange
            var lesson = new Lesson
            {
                LessonId = 504,
                Abonement = new Abonement
                {
                    Course = new Course
                    {
                        Teacher = new TeacherProfile { User = new User { Id = 302 } }
                    }
                },
                Status = LessonStatus.Waiting // не Happened
            };

            _mockContext.Setup(x => x.Set<Lesson>())
                .ReturnsDbSet(new[] { lesson });

            // Act & Assert
            var ex = Assert.ThrowsAsync<Exception>(async () =>
                await _iterator.GradeLesson(302, 504, new GradingInputDto()));
            Assert.That(ex?.Message, Does.Contain("doesn't happened"));
        }

        #endregion

        #region Тестирование CoinFormula

        [Test]
        public void CoinFormula_ReturnsCorrectCashback()
        {
            // Arrange
            var iterator = new GradingIterator(null!); // контекст не нужен для приватного метода

            // Act & Assert
            Assert.That(iterator.CoinFormula(5.0f, 1000), Is.EqualTo(100)); // макс. кэшбэк: 10%
            Assert.That(iterator.CoinFormula(0.0f, 1000), Is.EqualTo(0));
            Assert.That(iterator.CoinFormula(2.5f, 1000), Is.EqualTo(50)); // (2.5/5)*0.1*1000 = 50
            Assert.That(iterator.CoinFormula(4.0f, 500), Is.EqualTo(40));  // (4/5)*0.1*500 = 40
        }

        #endregion
    }
}
