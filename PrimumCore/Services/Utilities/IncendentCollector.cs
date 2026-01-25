using CoreConnection.DTOs;
using CoreConnection.Enums;
using Microsoft.EntityFrameworkCore;
using PrimumCore.Extentions;
using PrimumCore.Models;
using PrimumCore.Models.Enums;
using PrimumPlatformModel.Models.Enums;

namespace PrimumCore.Services.Utilities
{
    public class IncendentCollector(IPrimumContext context)
    {
        public async Task<IEnumerable<IncendentDto>> GetIncedents(Permission[] permissions)
        {
            IEnumerable<IncendentDto> incidents = new List<IncendentDto>();
            foreach (var permission in permissions)
            {
                if(!collectionRules.TryGetValue(permission, out Func<IEnumerable<IncendentDto>> rule)) { continue; }
                incidents.Concat(rule.Invoke());
            }
            return incidents;
        }

        private Dictionary<Permission, Func<IEnumerable<IncendentDto>>> collectionRules = new Dictionary<Permission, Func<IEnumerable<IncendentDto>>>
        {
            {
                Permission.ModerateTeachers,
                () => context.Set<User>()
                    .Include(x => x.TeacherProfile)
                    .Where(x => x.TeacherProfile.ApproveStatus == ApproveStatus.NeedModeratorReview)
                    .Select(x => new IncendentDto
                    {
                        ObjectId = x.Id,
                        Status = IncendentStatusDto.NeedModeration,
                        Meaning = IncendentMeaningDto.Teacher,
                        Decisions = Permission.ModerateTeachers.GetAvailableIncendentsAttributes().Select(x => x.Decision),
                        CommonInfo = 
                        $"Name: {x.Name}\n" +
                        $"Surname: {x.Surname}\n" +
                        $"Patronymic: {x.Patronymic}\n" +
                        $"Login: {x.Login}\n" +
                        $"About: {x.TeacherProfile.About}"
                    })
                    .ToList()
            },
            {
                Permission.ModerateStudents,
                () => context.Set<User>()
                    .Include(x => x.StudentProfile)
                    .Where(x => x.StudentProfile.ApproveStatus == ApproveStatus.NeedModeratorReview)
                    .Select(x => new IncendentDto
                    {
                        ObjectId = x.Id,
                        Status = IncendentStatusDto.NeedModeration,
                        Meaning = IncendentMeaningDto.Student,
                        Decisions = Permission.ModerateStudents.GetAvailableIncendentsAttributes().Select(x => x.Decision),
                        CommonInfo =
                        $"Name: {x.Name}\n" +
                        $"Surname: {x.Surname}\n" +
                        $"Patronymic: {x.Patronymic}\n" +
                        $"Login: {x.Login}"
                    })
                    .ToList()
            },
            {
                Permission.ModerateCourses,
                () => context.Set<Course>()
                    .Include(x => x.Teacher)
                    .ThenInclude(x => x.User)
                    .Where(x => x.ApproveStatus == ApproveStatus.NeedModeratorReview)
                    .Select(x => new IncendentDto
                    {
                        ObjectId = x.CourseId,
                        Status = IncendentStatusDto.NeedModeration,
                        Meaning = IncendentMeaningDto.Course,
                        Decisions = Permission.ModerateCourses.GetAvailableIncendentsAttributes().Select(x => x.Decision),
                        CommonInfo =
                        $"Name: {x.Name}\n" +
                        $"Teacher Name: {x.Teacher.User.DisplayName}\n" +
                        $"Price per lesson: {x.Price}\n" +
                        $"Maximum lessons: {x.MaxLessons}\n" +
                        $"Free lessons: {x}"
                    })
                    .ToList()
            },
            {
                Permission.AdministrateCourses,
                () => context.Set<Course>()
                    .Include(x => x.Teacher)
                    .ThenInclude(x => x.User)
                    .Where(x => x.ApproveStatus == ApproveStatus.NeedAdministratorReview)
                    .Select(x => new IncendentDto
                    {
                        ObjectId = x.CourseId,
                        Status = IncendentStatusDto.NeedModeration,
                        Meaning = IncendentMeaningDto.Course,
                        Decisions = Permission.AdministrateCourses.GetAvailableIncendentsAttributes().Select(x => x.Decision),
                        CommonInfo =
                        $"Name: {x.Name}\n" +
                        $"Teacher Name: {x.Teacher.User.DisplayName}\n" +
                        $"Price per lesson: {x.Price}\n" +
                        $"Maximum lessons: {x.MaxLessons}\n" +
                        $"Free lessons: {x}"
                    })
                    .ToList()
            },
            {
                Permission.AdministrateTeachers,
                () => context.Set<User>()
                    .Include(x => x.TeacherProfile)
                    .Where(x => x.TeacherProfile.ApproveStatus == ApproveStatus.NeedAdministratorReview)
                    .Select(x => new IncendentDto
                    {
                        ObjectId = x.Id,
                        Status = IncendentStatusDto.NeedModeration,
                        Meaning = IncendentMeaningDto.Teacher,
                        Decisions = Permission.AdministrateTeachers.GetAvailableIncendentsAttributes().Select(x => x.Decision),
                        CommonInfo =
                        $"Name: {x.Name}\n" +
                        $"Surname: {x.Surname}\n" +
                        $"Patronymic: {x.Patronymic}\n" +
                        $"Login: {x.Login}\n" +
                        $"About: {x.TeacherProfile.About}"
                    })
                    .ToList()
            },
            {
                Permission.AdministrateStudents,
                () => context.Set<User>()
                    .Include(x => x.StudentProfile)
                    .Where(x => x.StudentProfile.ApproveStatus == ApproveStatus.NeedAdministratorReview)
                    .Select(x => new IncendentDto
                    {
                        ObjectId = x.Id,
                        Status = IncendentStatusDto.NeedModeration,
                        Meaning = IncendentMeaningDto.Student,
                        Decisions = Permission.AdministrateStudents.GetAvailableIncendentsAttributes().Select(x => x.Decision),
                        CommonInfo =
                        $"Name: {x.Name}\n" +
                        $"Surname: {x.Surname}\n" +
                        $"Patronymic: {x.Patronymic}\n" +
                        $"Login: {x.Login}"
                    })
                    .ToList()
            },
            {
                Permission.ApproveCourses,
                () => context.Set<Course>()
                    .Include(x => x.Teacher)
                    .ThenInclude(x => x.User)
                    .Where(x => x.ApproveStatus == ApproveStatus.NeedManagerReview)
                    .Select(x => new IncendentDto
                    {
                        ObjectId = x.CourseId,
                        Status = IncendentStatusDto.NeedModeration,
                        Meaning = IncendentMeaningDto.Course,
                        Decisions = Permission.ApproveCourses.GetAvailableIncendentsAttributes().Select(x => x.Decision),
                        CommonInfo =
                        $"Name: {x.Name}\n" +
                        $"Teacher Name: {x.Teacher.User.DisplayName}\n" +
                        $"Price per lesson: {x.Price}\n" +
                        $"Maximum lessons: {x.MaxLessons}\n" +
                        $"Free lessons: {x.FreeLessons}"
                    })
                    .ToList()
            },
            {
                Permission.ApproveTeachers,
                () => context.Set<User>()
                    .Include(x => x.TeacherProfile)
                    .Where(x => x.TeacherProfile.ApproveStatus == ApproveStatus.NeedManagerReview)
                    .Select(x => new IncendentDto
                    {
                        ObjectId = x.Id,
                        Status = IncendentStatusDto.NeedModeration,
                        Meaning = IncendentMeaningDto.Teacher,
                        Decisions = Permission.ApproveTeachers.GetAvailableIncendentsAttributes().Select(x => x.Decision),
                        CommonInfo =
                        $"Name: {x.Name}\n" +
                        $"Surname: {x.Surname}\n" +
                        $"Patronymic: {x.Patronymic}\n" +
                        $"Login: {x.Login}\n" +
                        $"About: {x.TeacherProfile.About}"
                    })
                    .ToList()
            },
            {
                Permission.InspectMissedLessons,
                () => context.Set<Lesson>()
                    .Include(x => x.Abonement)
                    .ThenInclude(x => x.Student)
                    .ThenInclude(x => x.User)
                    .Include(x => x.Abonement)
                    .ThenInclude(x => x.Course)
                    .ThenInclude(x => x.Teacher)
                    .ThenInclude(x => x.User)
                    .Include(x => x.Abonement)
                    .ThenInclude(x => x.Course)
                    .ThenInclude(x => x.CourseTheme)
                    .Where(x => x.Status == LessonStatus.Missed)
                    .Select(x => new IncendentDto
                    {
                        ObjectId = x.LessonId,
                        Status = IncendentStatusDto.NeedInspectation,
                        Meaning = IncendentMeaningDto.Lesson,
                        Decisions = Permission.InspectMissedLessons.GetAvailableIncendentsAttributes().Select(x => x.Decision),
                        CommonInfo =
                        $"Student: {x.Abonement.Student.User.DisplayName}\n" +
                        $"Student Id: {x.Abonement.Student.User.Id}\n" +
                        $"Teacher: {x.Abonement.Course.Teacher.User.DisplayName}\n" +
                        $"Teacher Id: {x.Abonement.Course.Teacher.User.Id}\n" +
                        $"Course: {x.Abonement.Course.Name}\n" +
                        $"CourseTheme: {x.Abonement.Course.CourseTheme.ThemeName}\n" +
                        $"DateTime: {x.DateTime.ToString("HH:mm dd:MM:yyyy")}\n"
                    })
                    .ToList()
            },
        };
    }
}
