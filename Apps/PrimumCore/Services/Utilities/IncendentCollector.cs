using CoreConnection.DTOs;
using PrimumCore.Entities;
using CoreDBModel.Extensions;
using CoreDBModel.Models;
using CoreDBModel.Models.Enums;
using Microsoft.EntityFrameworkCore;
using PrimumCore.Extentions;
using PrimumCore.Services.Iterators;
using System.Data;

namespace PrimumCore.Services.Utilities
{
    public class IncidentCollector(DatabaseIterator dbIterator)
    {
        private record IncidentKey(int ObjectId, IncidentMeaning Meaning, Permission PermissionBy, int SortKey);

        public virtual async Task<PageResult<IncidentDto>> GetIncedents(
            Permission[] permissions,
            int page,
            int pageSize,
            CancellationToken cancellationToken = default)
        {
            // 🔹 ФАЗА 1: Собираем ключи для пагинации (только серверные запросы)
            var allKeys = new List<IncidentKey>();

            foreach (var permission in permissions)
            {
                var keys = await GetIncidentKeysAsync(permission, cancellationToken);
                allKeys.AddRange(keys);
            }

            // 🔹 Пагинация ключей в памяти
            var orderedKeys = allKeys.OrderBy(k => k.SortKey).ToList();
            var totalCount = orderedKeys.Count;
            var totalPages = totalCount == 0 ? 0 : (int)Math.Ceiling((double)totalCount / pageSize);

            var pageKeys = orderedKeys
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToList();

            if (pageKeys.Count == 0)
            {
                return new PageResult<IncidentDto>
                {
                    Items = new List<IncidentDto>(),
                    TotalItemsCount = 0,
                    CurrentPage = page,
                    TotalPages = 0,
                };
            }

            // 🔹 ФАЗА 2: Загружаем полные DTO только для записей страницы
            var items = new List<IncidentDto>();
            foreach (var key in pageKeys)
            {
                var dto = await LoadIncidentDtoAsync(key, cancellationToken);
                if (dto != null)
                {
                    // Загрузка связанных логов (отдельный запрос, только для страницы)
                    dto.LinkedLogs = await dbIterator.IncidentLogs(false)
                        .LoadIncidentLogs(dto.ObjectId, dto.Meaning)
                        .ToListAsync(cancellationToken);
                    items.Add(dto);
                }
            }

            return new PageResult<IncidentDto>
            {
                Items = items,
                TotalItemsCount = totalCount,
                TotalPages = totalPages,
                CurrentPage = page
            };
        }

        // 🔹 Получение ключей для одного permission (серверный запрос)
        private async Task<List<IncidentKey>> GetIncidentKeysAsync(
            Permission permission,
            CancellationToken cancellationToken)
        {
            return permission switch
            {
                Permission.ModerateTeachers =>
                    await dbIterator.Users(false)
                        .Where(x => x.TeacherProfile.ApproveStatus == ApproveStatus.NeedModeratorReview)
                        .Select(x => new IncidentKey(x.Id, IncidentMeaning.Teacher, permission, x.Id))
                        .ToListAsync(cancellationToken),

                Permission.AdministrateTeachers =>
                    await dbIterator.Users(false)
                        .Where(x => x.TeacherProfile.ApproveStatus == ApproveStatus.NeedAdministratorReview)
                        .Select(x => new IncidentKey(x.Id, IncidentMeaning.Teacher, permission, x.Id))
                        .ToListAsync(cancellationToken),

                Permission.ApproveTeachers =>
                    await dbIterator.Users(false)
                        .Where(x => x.TeacherProfile.ApproveStatus == ApproveStatus.NeedManagerReview)
                        .Select(x => new IncidentKey(x.Id, IncidentMeaning.Teacher, permission, x.Id))
                        .ToListAsync(cancellationToken),

                Permission.ModerateStudents =>
                    await dbIterator.Users(false)
                        .Where(x => x.StudentProfile.ApproveStatus == ApproveStatus.NeedModeratorReview)
                        .Select(x => new IncidentKey(x.Id, IncidentMeaning.Student, permission, x.Id))
                        .ToListAsync(cancellationToken),

                Permission.AdministrateStudents =>
                    await dbIterator.Users(false)
                        .Where(x => x.StudentProfile.ApproveStatus == ApproveStatus.NeedAdministratorReview)
                        .Select(x => new IncidentKey(x.Id, IncidentMeaning.Student, permission, x.Id))
                        .ToListAsync(cancellationToken),

                Permission.ModerateCourses =>
                    await dbIterator.Courses(false)
                        .Where(x => x.ApproveStatus == ApproveStatus.NeedModeratorReview)
                        .Select(x => new IncidentKey(x.Id, IncidentMeaning.Course, permission, x.Id))
                        .ToListAsync(cancellationToken),

                Permission.AdministrateCourses =>
                    await dbIterator.Courses(false)
                        .Where(x => x.ApproveStatus == ApproveStatus.NeedAdministratorReview)
                        .Select(x => new IncidentKey(x.Id, IncidentMeaning.Course, permission, x.Id))
                        .ToListAsync(cancellationToken),

                Permission.ApproveCourses =>
                    await dbIterator.Courses(false)
                        .Where(x => x.ApproveStatus == ApproveStatus.NeedManagerReview)
                        .Select(x => new IncidentKey(x.Id, IncidentMeaning.Course, permission, x.Id))
                        .ToListAsync(cancellationToken),

                Permission.InspectMissedLessons =>
                    await dbIterator.Lessons()
                        .Where(x => x.Status == LessonStatus.Missed)
                        .Select(x => new IncidentKey(x.Id, IncidentMeaning.Lesson, permission, x.Id))
                        .ToListAsync(cancellationToken),

                _ => new List<IncidentKey>()
            };
        }

        // 🔹 Загрузка полного DTO для одного ключа
        private async Task<IncidentDto?> LoadIncidentDtoAsync(
            IncidentKey key,
            CancellationToken cancellationToken)
        {
            var decisions = key.PermissionBy.GetAvailableIncidentsAttributes()
                .Select(a => a.Decision)
                .ToList();

            return key.Meaning switch
            {
                IncidentMeaning.Teacher => await dbIterator.Users(false)
                    .Where(x => x.Id == key.ObjectId)
                    .Select(x => new IncidentDto
                    {
                        ObjectId = x.Id,
                        Status = IncidentStatus.NeedModeration,
                        Meaning = IncidentMeaning.Teacher,
                        PermissionBy = key.PermissionBy,
                        Decisions = decisions,
                        CommonInfo =
                            $"Name: {x.Name}\n" +
                            $"Surname: {x.Surname}\n" +
                            $"Patronymic: {x.Patronymic}\n" +
                            $"Mail: {x.MailAdress}\n" +
                            $"About: {x.TeacherProfile.About}",
                        LinkedLogs = null
                    }).FirstOrDefaultAsync(cancellationToken),

                IncidentMeaning.Student => await dbIterator.Users(false)
                    .Where(x => x.Id == key.ObjectId)
                    .Select(x => new IncidentDto
                    {
                        ObjectId = x.Id,
                        Status = IncidentStatus.NeedModeration,
                        Meaning = IncidentMeaning.Student,
                        PermissionBy = key.PermissionBy,
                        Decisions = decisions,
                        CommonInfo =
                            $"Name: {x.Name}\n" +
                            $"Surname: {x.Surname}\n" +
                            $"Patronymic: {x.Patronymic}\n" +
                            $"Mail: {x.MailAdress}",
                        LinkedLogs = null
                    }).FirstOrDefaultAsync(cancellationToken),

                IncidentMeaning.Course => await dbIterator.Courses(false)
                    .Where(x => x.Id == key.ObjectId)
                    .Select(x => new IncidentDto
                    {
                        ObjectId = x.Id,
                        Status = IncidentStatus.NeedModeration,
                        Meaning = IncidentMeaning.Course,
                        PermissionBy = key.PermissionBy,
                        Decisions = decisions,
                        CommonInfo =
                            $"Name: {x.Name}\n" +
                            $"Teacher Name: {x.Teacher.User.DisplayName}\n" +
                            $"Price per lesson: {x.Price}\n" +
                            $"Maximum lessons: {x.MaxLessons}\n" +
                            $"Free lessons: {x.FreeLessons}",
                        LinkedLogs = null
                    }).FirstOrDefaultAsync(cancellationToken),

                IncidentMeaning.Lesson => await dbIterator.Lessons()
                    .Where(x => x.Id == key.ObjectId)
                    .Select(x => new IncidentDto
                    {
                        ObjectId = x.Id,
                        Status = IncidentStatus.NeedInspectation,
                        Meaning = IncidentMeaning.Lesson,
                        PermissionBy = key.PermissionBy,
                        Decisions = decisions,
                        CommonInfo =
                            $"Student: {x.Abonement.Student.User.DisplayName}\n" +
                            $"Student Id: {x.Abonement.Student.User.Id}\n" +
                            $"Student mail: {x.Abonement.Student.User.MailAdress}\n" +
                            $"Teacher: {x.Abonement.Course.Teacher.User.DisplayName}\n" +
                            $"Teacher Id: {x.Abonement.Course.Teacher.User.Id}\n" +
                            $"Course: {x.Abonement.Course.Name}\n" +
                            $"CourseTheme: {x.Abonement.Course.CourseTheme.ThemeName}\n" +
                            $"DateTime: {x.DateTime:HH:mm dd.MM.yyyy}\n",
                        LinkedLogs = null
                    }).FirstOrDefaultAsync(cancellationToken),

                _ => null
            };
        }
    }
}
