using CoreConnection.DTOs;
using CoreDBModel.Extensions;
using CoreDBModel.Models;
using CoreDBModel.Models.Enums;
using Microsoft.EntityFrameworkCore;
using PrimumCore.Extentions;

namespace PrimumCore.Services.Utilities
{
    public class IncidentCollector(PrimumContext context)
    {
        public virtual async Task<IEnumerable<IncidentDto>> GetIncedents(Permission[] permissions)
        {
            var incidents = new List<IncidentDto>();
            foreach (var permission in permissions)
            {
                if(collectionRules.TryGetValue(permission, out var rule))
                {
                    incidents.AddRange(rule.Invoke());
                }
            }

            foreach (var incident in incidents) 
            {
                incident.LinkedLogs = await context.Set<IncidentLog>()
                    .Include(x => x.AdminProfile)
                    .ThenInclude(x => x.User)
                    .Where(x => x.ObjectId != null && x.Meaning != null)
                    .Where(x => x.ObjectId == incident.ObjectId && x.Meaning == incident.Meaning)
                    .Select(x => new IncidentLogDto
                    {
                        AdminUserId = x.AdminProfile.UserId,
                        AdminDisplayName = x.AdminProfile.User.DisplayName,
                        LogId = x.LogId,
                        Description = x.Description,
                        DateTime = x.DecisionDate
                    })
                    .ToArrayAsync();
            }

            return incidents;
        }

        private Dictionary<Permission, Func<IEnumerable<IncidentDto>>> collectionRules = new Dictionary<Permission, Func<IEnumerable<IncidentDto>>>
        {
            {
                Permission.ModerateTeachers,
                () => context.Set<User>()
                    .Include(x => x.TeacherProfile)
                    .Where(x => x.TeacherProfile.ApproveStatus == ApproveStatus.NeedModeratorReview)
                    .Select(x => new IncidentDto
                    {
                        ObjectId = x.Id,
                        Status = IncidentStatus.NeedModeration,
                        Meaning = IncidentMeaning.Teacher,
                        Decisions = Permission.ModerateTeachers.GetAvailableIncidentsAttributes().Select(x => x.Decision),
                        CommonInfo = 
                        $"Name: {x.Name}\n" +
                        $"Surname: {x.Surname}\n" +
                        $"Patronymic: {x.Patronymic}\n" +
                        $"Mail: {x.MailAdress}\n" +
                        $"About: {x.TeacherProfile.About}",
                        LinkedLogs = null
                    })
                    .ToList()
            },
            {
                Permission.ModerateStudents,
                () => context.Set<User>()
                    .Include(x => x.StudentProfile)
                    .Where(x => x.StudentProfile.ApproveStatus == ApproveStatus.NeedModeratorReview)
                    .Select(x => new IncidentDto
                    {
                        ObjectId = x.Id,
                        Status = IncidentStatus.NeedModeration,
                        Meaning = IncidentMeaning.Student,
                        Decisions = Permission.ModerateStudents.GetAvailableIncidentsAttributes().Select(x => x.Decision),
                        CommonInfo =
                        $"Name: {x.Name}\n" +
                        $"Surname: {x.Surname}\n" +
                        $"Patronymic: {x.Patronymic}\n" +
                        $"Mail: {x.MailAdress}",
                        LinkedLogs = null
                    })
                    .ToList()
            },
            {
                Permission.ModerateCourses,
                () => context.Set<Course>()
                    .Include(x => x.Teacher)
                    .ThenInclude(x => x.User)
                    .Where(x => x.ApproveStatus == ApproveStatus.NeedModeratorReview)
                    .Select(x => new IncidentDto
                    {
                        ObjectId = x.CourseId,
                        Status = IncidentStatus.NeedModeration,
                        Meaning = IncidentMeaning.Course,
                        Decisions = Permission.ModerateCourses.GetAvailableIncidentsAttributes().Select(x => x.Decision),
                        CommonInfo =
                        $"Name: {x.Name}\n" +
                        $"Teacher Name: {x.Teacher.User.DisplayName}\n" +
                        $"Price per lesson: {x.Price}\n" +
                        $"Maximum lessons: {x.MaxLessons}\n" +
                        $"Free lessons: {x}",
                        LinkedLogs = null
                    })
                    .ToList()
            },
            {
                Permission.AdministrateCourses,
                () => context.Set<Course>()
                    .Include(x => x.Teacher)
                    .ThenInclude(x => x.User)
                    .Where(x => x.ApproveStatus == ApproveStatus.NeedAdministratorReview)
                    .Select(x => new IncidentDto
                    {
                        ObjectId = x.CourseId,
                        Status = IncidentStatus.NeedModeration,
                        Meaning = IncidentMeaning.Course,
                        Decisions = Permission.AdministrateCourses.GetAvailableIncidentsAttributes().Select(x => x.Decision),
                        CommonInfo =
                        $"Name: {x.Name}\n" +
                        $"Teacher Name: {x.Teacher.User.DisplayName}\n" +
                        $"Price per lesson: {x.Price}\n" +
                        $"Maximum lessons: {x.MaxLessons}\n" +
                        $"Free lessons: {x.FreeLessons}",
                        LinkedLogs = null
                    })
                    .ToList()
            },
            {
                Permission.AdministrateTeachers,
                () => context.Set<User>()
                    .Include(x => x.TeacherProfile)
                    .Where(x => x.TeacherProfile.ApproveStatus == ApproveStatus.NeedAdministratorReview)
                    .Select(x => new IncidentDto
                    {
                        ObjectId = x.Id,
                        Status = IncidentStatus.NeedModeration,
                        Meaning = IncidentMeaning.Teacher,
                        Decisions = Permission.AdministrateTeachers.GetAvailableIncidentsAttributes().Select(x => x.Decision),
                        CommonInfo =
                        $"Name: {x.Name}\n" +
                        $"Surname: {x.Surname}\n" +
                        $"Patronymic: {x.Patronymic}\n" +
                        $"Mail: {x.MailAdress}\n" +
                        $"About: {x.TeacherProfile.About}",
                        LinkedLogs = null
                    })
                    .ToList()
            },
            {
                Permission.AdministrateStudents,
                () => context.Set<User>()
                    .Include(x => x.StudentProfile)
                    .Where(x => x.StudentProfile.ApproveStatus == ApproveStatus.NeedAdministratorReview)
                    .Select(x => new IncidentDto
                    {
                        ObjectId = x.Id,
                        Status = IncidentStatus.NeedModeration,
                        Meaning = IncidentMeaning.Student,
                        Decisions = Permission.AdministrateStudents.GetAvailableIncidentsAttributes().Select(x => x.Decision),
                        CommonInfo =
                        $"Name: {x.Name}\n" +
                        $"Surname: {x.Surname}\n" +
                        $"Patronymic: {x.Patronymic}\n" +
                        $"Mail: {x.MailAdress}",
                        LinkedLogs = null
                    })
                    .ToList()
            },
            {
                Permission.ApproveCourses,
                () => context.Set<Course>()
                    .Include(x => x.Teacher)
                    .ThenInclude(x => x.User)
                    .Where(x => x.ApproveStatus == ApproveStatus.NeedManagerReview)
                    .Select(x => new IncidentDto
                    {
                        ObjectId = x.CourseId,
                        Status = IncidentStatus.NeedModeration,
                        Meaning = IncidentMeaning.Course,
                        Decisions = Permission.ApproveCourses.GetAvailableIncidentsAttributes().Select(x => x.Decision),
                        CommonInfo =
                        $"Name: {x.Name}\n" +
                        $"Teacher Name: {x.Teacher.User.DisplayName}\n" +
                        $"Price per lesson: {x.Price}\n" +
                        $"Maximum lessons: {x.MaxLessons}\n" +
                        $"Free lessons: {x.FreeLessons}",
                        LinkedLogs = null
                    })
                    .ToList()
            },
            {
                Permission.ApproveTeachers,
                () => context.Set<User>()
                    .Include(x => x.TeacherProfile)
                    .Where(x => x.TeacherProfile.ApproveStatus == ApproveStatus.NeedManagerReview)
                    .Select(x => new IncidentDto
                    {
                        ObjectId = x.Id,
                        Status = IncidentStatus.NeedModeration,
                        Meaning = IncidentMeaning.Teacher,
                        Decisions = Permission.ApproveTeachers.GetAvailableIncidentsAttributes().Select(x => x.Decision),
                        CommonInfo =
                        $"Name: {x.Name}\n" +
                        $"Surname: {x.Surname}\n" +
                        $"Patronymic: {x.Patronymic}\n" +
                        $"Mail: {x.MailAdress}\n" +
                        $"About: {x.TeacherProfile.About}",
                        LinkedLogs = null
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
                    .Select(x => new IncidentDto
                    {
                        ObjectId = x.LessonId,
                        Status = IncidentStatus.NeedInspectation,
                        Meaning = IncidentMeaning.Lesson,
                        Decisions = Permission.InspectMissedLessons.GetAvailableIncidentsAttributes().Select(x => x.Decision),
                        CommonInfo =
                        $"Student: {x.Abonement.Student.User.DisplayName}\n" +
                        $"Student Id: {x.Abonement.Student.User.Id}\n" +
                        $"Student mail: {x.Abonement.Student.User.MailAdress}" +
                        $"Teacher: {x.Abonement.Course.Teacher.User.DisplayName}\n" +
                        $"Teacher Id: {x.Abonement.Course.Teacher.User.Id}\n" +
                        $"Course: {x.Abonement.Course.Name}\n" +
                        $"CourseTheme: {x.Abonement.Course.CourseTheme.ThemeName}\n" +
                        $"DateTime: {x.DateTime.ToString("HH:mm dd:MM:yyyy")}\n",
                        LinkedLogs = null
                    })
                    .ToList()
            },
        };
    }
}
