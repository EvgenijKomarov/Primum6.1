using CoreConnection.DTOs;
using CoreConnection.Entities;
using CoreDBModel.Extensions;
using CoreDBModel.Models;
using CoreDBModel.Models.Enums;
using Microsoft.EntityFrameworkCore;
using PrimumCore.Extentions;
using System.Data;

namespace PrimumCore.Services.Utilities
{
    public class IncidentCollector(PrimumContext context)
    {
        public virtual async Task<PageResult<IncidentDto>> GetIncedents(Permission[] permissions, int _page, int _pageSize)
        {
            IQueryable<IncidentDto>? query = null;
            foreach (var permission in permissions)
            {
                if(collectionRules.TryGetValue(permission, out var rule))
                {
                    var ruleQuery = rule.Invoke();
                    query = query == null ? ruleQuery : query.Union(ruleQuery);
                }
            }
            if (query == null)
            {
                return new PageResult<IncidentDto> 
                { 
                    Items = new List<IncidentDto>(),
                    TotalItemsCount = 0,
                    CurrentPage = _page,
                    TotalPages = 0,
                };
            }

            return await query.ToPageResult(_page, _pageSize);
        }


        private Dictionary<Permission, Func<IQueryable<IncidentDto>>> collectionRules = new Dictionary<Permission, Func<IQueryable<IncidentDto>>>
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
                        LinkedLogs = context.Set<IncidentLog>().LoadIncidentLogs(x.Id, IncidentMeaning.Teacher).ToArray()
                    })
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
                        LinkedLogs = context.Set<IncidentLog>().LoadIncidentLogs(x.Id, IncidentMeaning.Student).ToArray()
                    })
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
                        LinkedLogs = context.Set<IncidentLog>().LoadIncidentLogs(x.CourseId, IncidentMeaning.Course).ToArray()
                    })
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
                        LinkedLogs = context.Set<IncidentLog>().LoadIncidentLogs(x.CourseId, IncidentMeaning.Course).ToArray()
                    })
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
                        LinkedLogs = context.Set<IncidentLog>().LoadIncidentLogs(x.Id, IncidentMeaning.Teacher).ToArray()
                    })
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
                        LinkedLogs = context.Set<IncidentLog>().LoadIncidentLogs(x.Id, IncidentMeaning.Student).ToArray()
                    })
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
                        $"Teacher Mail: {x.Teacher.User.MailAdress}\n" +
                        $"Price per lesson: {x.Price}\n" +
                        $"Maximum lessons: {x.MaxLessons}\n" +
                        $"Free lessons: {x.FreeLessons}",
                        LinkedLogs = context.Set <IncidentLog>().LoadIncidentLogs(x.CourseId, IncidentMeaning.Course).ToArray()
                    })
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
                        LinkedLogs = context.Set<IncidentLog>().LoadIncidentLogs(x.Id, IncidentMeaning.Teacher).ToArray()
                    })
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
                        LinkedLogs = context.Set<IncidentLog>().LoadIncidentLogs(x.LessonId, IncidentMeaning.Lesson).ToArray()
                    })
            },
        };
    }
}
