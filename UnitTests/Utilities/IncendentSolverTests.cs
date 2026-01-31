using CoreConnection.DTOs.Inputs;
using CoreConnection.Enums;
using Moq;
using Moq.EntityFrameworkCore;
using PrimumCore.Models;
using PrimumCore.Models.Enums;
using PrimumCore.Services.Utilities;
using PrimumPlatformModel.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Utilities
{
    public class IncendentSolverTests
    {
        private Mock<IPrimumContext> _mockContext;
        private IncendentSolver _solver;
        private const int AdminProfileId = 999;

        [SetUp]
        public void Setup()
        {
            _mockContext = new Mock<IPrimumContext>();
            _solver = new IncendentSolver(_mockContext.Object);
            _mockContext.Setup(x => x.Set<IncendentLog>())
                .ReturnsDbSet(new List<IncendentLog>());
        }

        #region Course Cases

        [TestCase(ApproveStatus.NeedModeratorReview,
            IncendentDecisionDto.SendToAdministrator,
            Permission.ModerateCourses,
            ApproveStatus.NeedAdministratorReview)]
        [TestCase(ApproveStatus.NeedAdministratorReview,
            IncendentDecisionDto.SendToManager,
            Permission.AdministrateCourses,
            ApproveStatus.NeedManagerReview)]
        [TestCase(ApproveStatus.NeedModeratorReview,
            IncendentDecisionDto.SendToManager,
            Permission.ModerateCourses,
            ApproveStatus.NeedManagerReview)]
        [TestCase(ApproveStatus.NeedManagerReview,
            IncendentDecisionDto.Approve,
            Permission.ApproveCourses,
            ApproveStatus.Approved)]
        [TestCase(ApproveStatus.NeedModeratorReview,
            IncendentDecisionDto.Delete,
            Permission.ModerateCourses,
            null)] // удаление — статус не меняется (сущность удаляется)
        public async Task SolveIncendent_Course_HandlesAllDecisions(
            ApproveStatus initialStatus,
            IncendentDecisionDto decision,
            Permission requiredPermission,
            ApproveStatus? expectedStatus)
        {
            var abonementShedules = new List<AbonementShedule> { new(), new() };
            var lessons = new List<Lesson> { new(), new() };
            var abonements = new List<Abonement>
            {
                new() { Lessons = lessons, AbonementShedules = abonementShedules }
            };
            var course = new Course { CourseId = 301, ApproveStatus = initialStatus, Abonements = abonements };

            _mockContext.Setup(x => x.Set<Course>())
                .ReturnsDbSet(new List<Course> { course });
            _mockContext.Setup(x => x.Set<Abonement>())
                .ReturnsDbSet(abonements);
            _mockContext.Setup(x => x.Set<Lesson>())
                .ReturnsDbSet(lessons);
            _mockContext.Setup(x => x.Set<AbonementShedule>())
                .ReturnsDbSet(abonementShedules);

            var dto = new IncendentDecisionInputDto
            {
                Meaning = IncendentMeaningDto.Course,
                Decision = decision,
                ObjectId = 301,
                IncendentInfo = "Test course"
            };

            var result = await _solver.SolveIncendent(AdminProfileId, new[] { requiredPermission }, dto);

            Assert.That(result, Is.EqualTo(301));

            if (expectedStatus.HasValue)
            {
                Assert.That(course.ApproveStatus, Is.EqualTo(expectedStatus.Value));
            }

            // Проверка удаления
            if (decision == IncendentDecisionDto.Delete)
            {
                _mockContext.Verify(x => x.Set<AbonementShedule>().RemoveRange(abonementShedules), Times.Once);
                _mockContext.Verify(x => x.Set<Lesson>().RemoveRange(lessons), Times.Once);
                _mockContext.Verify(x => x.Set<Abonement>().RemoveRange(abonements), Times.Once);
                _mockContext.Verify(x => x.Set<Course>().Remove(course), Times.Once);
            }

            _mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
            _mockContext.Verify(x => x.Set<IncendentLog>().Add(It.Is<IncendentLog>(log =>
                log.AdminProfileId == AdminProfileId &&
                log.Description.Contains("Course") &&
                log.Description.Contains(decision.ToString()))), Times.Once);
        }

        #endregion

        #region Teacher Cases

        [TestCase(ApproveStatus.NeedModeratorReview,
            IncendentDecisionDto.SendToAdministrator,
            Permission.ModerateTeachers,
            ApproveStatus.NeedAdministratorReview)]
        [TestCase(ApproveStatus.NeedModeratorReview,
            IncendentDecisionDto.SendToManager,
            Permission.ModerateTeachers,
            ApproveStatus.NeedManagerReview)]
        [TestCase(ApproveStatus.NeedAdministratorReview,
            IncendentDecisionDto.SendToManager,
            Permission.AdministrateTeachers,
            ApproveStatus.NeedManagerReview)]
        [TestCase(ApproveStatus.NeedModeratorReview,
            IncendentDecisionDto.Delete,
            Permission.ModerateTeachers,
            null)]
        public async Task SolveIncendent_Teacher_HandlesAllDecisions(
            ApproveStatus initialStatus,
            IncendentDecisionDto decision,
            Permission requiredPermission,
            ApproveStatus? expectedStatus)
        {
            var teacherShedules = new List<TeacherShedule> { new(), new() };
            var courses = new List<Course> { new(), new() };
            var tProfile = new TeacherProfile
            {
                ApproveStatus = initialStatus,
                TeacherShedules = teacherShedules,
                Courses = courses
            };
            var user = new User
            {
                Id = 101,
                TeacherProfile = tProfile
            };

            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new List<User> { user });
            _mockContext.Setup(x => x.Set<TeacherShedule>())
                .ReturnsDbSet(teacherShedules);
            _mockContext.Setup(x => x.Set<Course>())
                .ReturnsDbSet(courses);
            _mockContext.Setup(x => x.Set<TeacherProfile>())
                .ReturnsDbSet(new List<TeacherProfile> { tProfile });

            var dto = new IncendentDecisionInputDto
            {
                Meaning = IncendentMeaningDto.Teacher,
                Decision = decision,
                ObjectId = 101,
                IncendentInfo = "Test teacher"
            };

            var result = await _solver.SolveIncendent(AdminProfileId, new[] { requiredPermission }, dto);

            Assert.That(result, Is.EqualTo(101));

            if (expectedStatus.HasValue)
            {
                Assert.That(user.TeacherProfile.ApproveStatus, Is.EqualTo(expectedStatus.Value));
            }

            if (decision == IncendentDecisionDto.Delete)
            {
                _mockContext.Verify(x => x.Set<TeacherShedule>().RemoveRange(teacherShedules), Times.Once);
                _mockContext.Verify(x => x.Set<Course>().RemoveRange(courses), Times.Once);
                _mockContext.Verify(x => x.Set<TeacherProfile>().Remove(tProfile), Times.Once);
            }

            _mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
            _mockContext.Verify(x => x.Set<IncendentLog>().Add(It.Is<IncendentLog>(log =>
                log.AdminProfileId == AdminProfileId &&
                log.Description.Contains("Teacher") &&
                log.Description.Contains(decision.ToString()))), Times.Once);
        }

        #endregion

        #region Student Cases

        [TestCase(ApproveStatus.NeedModeratorReview,
            IncendentDecisionDto.SendToAdministrator,
            Permission.ModerateStudents,
            ApproveStatus.NeedAdministratorReview)]
        [TestCase(ApproveStatus.NeedAdministratorReview,
            IncendentDecisionDto.Approve,
            Permission.AdministrateStudents,
            ApproveStatus.Approved)]
        [TestCase(ApproveStatus.NeedModeratorReview,
            IncendentDecisionDto.Delete,
            Permission.ModerateStudents,
            null)]
        public async Task SolveIncendent_Student_HandlesAllDecisions(
            ApproveStatus initialStatus,
            IncendentDecisionDto decision,
            Permission requiredPermission,
            ApproveStatus? expectedStatus)
        {
            var abonementShedules = new List<AbonementShedule> { new(), new() };
            var lessons = new List<Lesson> { new(), new() };
            var abonements = new List<Abonement>
            {
                new Abonement
                {
                    Lessons = lessons,
                    AbonementShedules = abonementShedules
                }
            };
            var user = new User
            {
                Id = 201,
                StudentProfile = new StudentProfile
                {
                    ApproveStatus = initialStatus,
                    Abonements = abonements
                }
            };

            _mockContext.Setup(x => x.Set<User>())
                .ReturnsDbSet(new List<User> { user });
            _mockContext.Setup(x => x.Set<Abonement>())
                .ReturnsDbSet(abonements);
            _mockContext.Setup(x => x.Set<Lesson>())
                .ReturnsDbSet(lessons);
            _mockContext.Setup(x => x.Set<AbonementShedule>())
                .ReturnsDbSet(abonementShedules);
            _mockContext.Setup(x => x.Set<StudentProfile>())
                .ReturnsDbSet(new List<StudentProfile> { user.StudentProfile });

            var dto = new IncendentDecisionInputDto
            {
                Meaning = IncendentMeaningDto.Student,
                Decision = decision,
                ObjectId = 201,
                IncendentInfo = "Test student"
            };

            var result = await _solver.SolveIncendent(AdminProfileId, new[] { requiredPermission }, dto);

            Assert.That(result, Is.EqualTo(201));

            if (expectedStatus.HasValue)
            {
                Assert.That(user.StudentProfile.ApproveStatus, Is.EqualTo(expectedStatus.Value));
            }

            if (decision == IncendentDecisionDto.Delete)
            {
                _mockContext.Verify(x => x.Set<AbonementShedule>().RemoveRange(abonementShedules), Times.Once);
                _mockContext.Verify(x => x.Set<Lesson>().RemoveRange(lessons), Times.Once);
                _mockContext.Verify(x => x.Set<Abonement>().RemoveRange(abonements), Times.Once);
                _mockContext.Verify(x => x.Set<StudentProfile>().Remove(user.StudentProfile), Times.Once);
            }

            _mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
            _mockContext.Verify(x => x.Set<IncendentLog>().Add(It.Is<IncendentLog>(log =>
                log.AdminProfileId == AdminProfileId &&
                log.Description.Contains("Student") &&
                log.Description.Contains(decision.ToString()))), Times.Once);
        }

        #endregion

        #region Lesson Cases (только Delete)

        [TestCase(IncendentDecisionDto.Delete, Permission.InspectMissedLessons)]
        public async Task SolveIncendent_Lesson_HandlesDelete(
            IncendentDecisionDto decision,
            Permission requiredPermission)
        {
            var lesson = new Lesson { LessonId = 401 };
            _mockContext.Setup(x => x.Set<Lesson>())
                .ReturnsDbSet(new List<Lesson> { lesson });

            var dto = new IncendentDecisionInputDto
            {
                Meaning = IncendentMeaningDto.Lesson,
                Decision = decision,
                ObjectId = 401,
                IncendentInfo = "Missed lesson"
            };

            var result = await _solver.SolveIncendent(AdminProfileId, new[] { requiredPermission }, dto);

            Assert.That(result, Is.EqualTo(401));
            _mockContext.Verify(x => x.Set<Lesson>().Remove(It.Is<Lesson>(l => l.LessonId == 401)), Times.Once);
            _mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
            _mockContext.Verify(x => x.Set<IncendentLog>().Add(It.Is<IncendentLog>(log =>
                log.AdminProfileId == AdminProfileId &&
                log.Description.Contains("Lesson") &&
                log.Description.Contains("Delete"))), Times.Once);
        }

        #endregion

        #region Permission Validation (общий кейс)

        [Test]
        public void SolveIncendent_WithoutRequiredPermission_ThrowsException()
        {
            var course = new Course { CourseId = 301, ApproveStatus = ApproveStatus.NeedModeratorReview };
            _mockContext.Setup(x => x.Set<Course>())
                .ReturnsDbSet(new List<Course> { course });

            var dto = new IncendentDecisionInputDto
            {
                Meaning = IncendentMeaningDto.Course,
                Decision = IncendentDecisionDto.SendToAdministrator,
                ObjectId = 301,
                IncendentInfo = "test"
            };

            // Нет нужного разрешения
            var permissions = new[] { Permission.GivePermissions };

            Assert.ThrowsAsync<Exception>(async () =>
                await _solver.SolveIncendent(AdminProfileId, permissions, dto),
                "User hasn't needed permissions");
        }

        #endregion
    }
}
