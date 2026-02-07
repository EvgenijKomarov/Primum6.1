using CoreConnection.DTOs.Inputs;
using CoreConnection.Enums;
using Moq;
using Moq.EntityFrameworkCore;
using PrimumCore.Exceptions;
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
    public class IncidentSolverTests
    {
        private Mock<IPrimumContext> _mockContext;
        private IncidentSolver _solver;
        private const int AdminProfileId = 999;

        [SetUp]
        public void Setup()
        {
            _mockContext = new Mock<IPrimumContext>();
            _solver = new IncidentSolver(_mockContext.Object);
            _mockContext.Setup(x => x.Set<IncidentLog>())
                .ReturnsDbSet(new List<IncidentLog>());
        }

        #region Course Cases

        [TestCase(ApproveStatus.NeedModeratorReview,
            IncidentDecisionDto.SendToAdministrator,
            Permission.ModerateCourses,
            ApproveStatus.NeedAdministratorReview)]
        [TestCase(ApproveStatus.NeedAdministratorReview,
            IncidentDecisionDto.SendToManager,
            Permission.AdministrateCourses,
            ApproveStatus.NeedManagerReview)]
        [TestCase(ApproveStatus.NeedModeratorReview,
            IncidentDecisionDto.SendToManager,
            Permission.ModerateCourses,
            ApproveStatus.NeedManagerReview)]
        [TestCase(ApproveStatus.NeedManagerReview,
            IncidentDecisionDto.Approve,
            Permission.ApproveCourses,
            ApproveStatus.Approved)]
        [TestCase(ApproveStatus.NeedModeratorReview,
            IncidentDecisionDto.Delete,
            Permission.ModerateCourses,
            null)] // удаление — статус не меняется (сущность удаляется)
        public async Task SolveIncident_Course_HandlesAllDecisions(
            ApproveStatus initialStatus,
            IncidentDecisionDto decision,
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

            var dto = new IncidentDecisionInputDto
            {
                Meaning = IncidentMeaningDto.Course,
                Decision = decision,
                ObjectId = 301,
                DecisionExplanation = "Test course"
            };

            var result = await _solver.SolveIncident(AdminProfileId, new[] { requiredPermission }, dto, 1);

            Assert.That(result, Is.EqualTo(301));

            if (expectedStatus.HasValue)
            {
                Assert.That(course.ApproveStatus, Is.EqualTo(expectedStatus.Value));
            }

            // Проверка удаления
            if (decision == IncidentDecisionDto.Delete)
            {
                _mockContext.Verify(x => x.Set<AbonementShedule>().RemoveRange(abonementShedules), Times.Once);
                _mockContext.Verify(x => x.Set<Lesson>().RemoveRange(lessons), Times.Once);
                _mockContext.Verify(x => x.Set<Abonement>().RemoveRange(abonements), Times.Once);
                _mockContext.Verify(x => x.Set<Course>().Remove(course), Times.Once);
            }

            _mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
            _mockContext.Verify(x => x.Set<IncidentLog>().Add(It.Is<IncidentLog>(log =>
                log.AdminProfileId == AdminProfileId &&
                log.Description.Contains("Course") &&
                log.Description.Contains(decision.ToString()))), Times.Once);
        }

        #endregion

        #region Teacher Cases

        [TestCase(ApproveStatus.NeedModeratorReview,
            IncidentDecisionDto.SendToAdministrator,
            Permission.ModerateTeachers,
            ApproveStatus.NeedAdministratorReview)]
        [TestCase(ApproveStatus.NeedModeratorReview,
            IncidentDecisionDto.SendToManager,
            Permission.ModerateTeachers,
            ApproveStatus.NeedManagerReview)]
        [TestCase(ApproveStatus.NeedAdministratorReview,
            IncidentDecisionDto.SendToManager,
            Permission.AdministrateTeachers,
            ApproveStatus.NeedManagerReview)]
        [TestCase(ApproveStatus.NeedModeratorReview,
            IncidentDecisionDto.Delete,
            Permission.ModerateTeachers,
            null)]
        public async Task SolveIncident_Teacher_HandlesAllDecisions(
            ApproveStatus initialStatus,
            IncidentDecisionDto decision,
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

            var dto = new IncidentDecisionInputDto
            {
                Meaning = IncidentMeaningDto.Teacher,
                Decision = decision,
                ObjectId = 101,
                DecisionExplanation = "Test teacher"
            };

            var result = await _solver.SolveIncident(AdminProfileId, new[] { requiredPermission }, dto, 1);

            Assert.That(result, Is.EqualTo(101));

            if (expectedStatus.HasValue)
            {
                Assert.That(user.TeacherProfile.ApproveStatus, Is.EqualTo(expectedStatus.Value));
            }

            if (decision == IncidentDecisionDto.Delete)
            {
                _mockContext.Verify(x => x.Set<TeacherShedule>().RemoveRange(teacherShedules), Times.Once);
                _mockContext.Verify(x => x.Set<Course>().RemoveRange(courses), Times.Once);
                _mockContext.Verify(x => x.Set<TeacherProfile>().Remove(tProfile), Times.Once);
            }

            _mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
            _mockContext.Verify(x => x.Set<IncidentLog>().Add(It.Is<IncidentLog>(log =>
                log.AdminProfileId == AdminProfileId &&
                log.Description.Contains("Teacher") &&
                log.Description.Contains(decision.ToString()))), Times.Once);
        }

        #endregion

        #region Student Cases

        [TestCase(ApproveStatus.NeedModeratorReview,
            IncidentDecisionDto.SendToAdministrator,
            Permission.ModerateStudents,
            ApproveStatus.NeedAdministratorReview)]
        [TestCase(ApproveStatus.NeedAdministratorReview,
            IncidentDecisionDto.Approve,
            Permission.AdministrateStudents,
            ApproveStatus.Approved)]
        [TestCase(ApproveStatus.NeedModeratorReview,
            IncidentDecisionDto.Delete,
            Permission.ModerateStudents,
            null)]
        public async Task SolveIncident_Student_HandlesAllDecisions(
            ApproveStatus initialStatus,
            IncidentDecisionDto decision,
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

            var dto = new IncidentDecisionInputDto
            {
                Meaning = IncidentMeaningDto.Student,
                Decision = decision,
                ObjectId = 201,
                DecisionExplanation = "Test student"
            };

            var result = await _solver.SolveIncident(AdminProfileId, new[] { requiredPermission }, dto, 1);

            Assert.That(result, Is.EqualTo(201));

            if (expectedStatus.HasValue)
            {
                Assert.That(user.StudentProfile.ApproveStatus, Is.EqualTo(expectedStatus.Value));
            }

            if (decision == IncidentDecisionDto.Delete)
            {
                _mockContext.Verify(x => x.Set<AbonementShedule>().RemoveRange(abonementShedules), Times.Once);
                _mockContext.Verify(x => x.Set<Lesson>().RemoveRange(lessons), Times.Once);
                _mockContext.Verify(x => x.Set<Abonement>().RemoveRange(abonements), Times.Once);
                _mockContext.Verify(x => x.Set<StudentProfile>().Remove(user.StudentProfile), Times.Once);
            }

            _mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
            _mockContext.Verify(x => x.Set<IncidentLog>().Add(It.Is<IncidentLog>(log =>
                log.AdminProfileId == AdminProfileId &&
                log.Description.Contains("Student") &&
                log.Description.Contains(decision.ToString()))), Times.Once);
        }

        #endregion

        #region Lesson Cases

        [TestCase(IncidentDecisionDto.Delete, Permission.InspectMissedLessons, null)]
        [TestCase(IncidentDecisionDto.Revisioned, Permission.InspectMissedLessons, LessonStatus.MissedWithoutReason)]
        public async Task SolveIncident_Lesson_HandlesDelete(
            IncidentDecisionDto decision,
            Permission requiredPermission,
            LessonStatus? expectedStatus)
        {
            var lesson = new Lesson { LessonId = 401, Status = LessonStatus.Missed };
            _mockContext.Setup(x => x.Set<Lesson>())
                .ReturnsDbSet(new List<Lesson> { lesson });

            var dto = new IncidentDecisionInputDto
            {
                Meaning = IncidentMeaningDto.Lesson,
                Decision = decision,
                ObjectId = 401,
                DecisionExplanation = "Missed lesson"
            };

            var result = await _solver.SolveIncident(AdminProfileId, new[] { requiredPermission }, dto, 1);

            Assert.That(result, Is.EqualTo(401));
            if (decision is IncidentDecisionDto.Delete)
            {
                _mockContext.Verify(x => x.Set<Lesson>().Remove(It.Is<Lesson>(l => l.LessonId == 401)), Times.Once);
            }
            if(expectedStatus is not null)
            {
                Assert.That(lesson.Status, Is.EqualTo(expectedStatus));
            }
            _mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
            _mockContext.Verify(x => x.Set<IncidentLog>().Add(It.Is<IncidentLog>(log =>
                log.AdminProfileId == AdminProfileId &&
                log.Description.Contains("Lesson") &&
                log.Description.Contains(decision.ToString()))), Times.Once);
        }

        #endregion

        #region Permission Validation (общий кейс)

        [Test]
        public void SolveIncident_WithoutRequiredPermission_ThrowsException()
        {
            var course = new Course { CourseId = 301, ApproveStatus = ApproveStatus.NeedModeratorReview };
            _mockContext.Setup(x => x.Set<Course>())
                .ReturnsDbSet(new List<Course> { course });

            var dto = new IncidentDecisionInputDto
            {
                Meaning = IncidentMeaningDto.Course,
                Decision = IncidentDecisionDto.SendToAdministrator,
                ObjectId = 301,
                DecisionExplanation = "test"
            };

            // Нет нужного разрешения
            var permissions = new[] { Permission.EditPermissions };

            Assert.ThrowsAsync<NoPermissionException>(async () =>
                await _solver.SolveIncident(AdminProfileId, permissions, dto, 1));
        }

        #endregion
    }
}
