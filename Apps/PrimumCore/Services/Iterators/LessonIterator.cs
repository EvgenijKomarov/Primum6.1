using Common.Utilities;
using CoreConnection.DTOs;
using CoreConnection.DTOs.Inputs;
using CoreDBModel.Constants;
using CoreDBModel.Models;
using CoreDBModel.Models.Enums;
using Microsoft.EntityFrameworkCore;
using PrimumCore.Entities;
using PrimumCore.Exceptions;
using PrimumCore.Extentions;
using PrimumCore.Services.Utilities;
using PublishServiceConnection;
using PublishServiceConnection.Events;
using System.Linq.Expressions;

namespace PrimumCore.Services.Iterators
{
    public class LessonIterator(
        DatabaseIterator dbIterator, 
        EarningCalculationService calculationService, 
        PublisherService publisher, 
        AllowedAdminsCollector collector,
        TeacherIterator teacherIterator)
    {
        public async Task<PageResult<LessonDto>> GetAbonementLessons(int abonementId, bool isStudentLink, int _page, int _pageSize)
        {
            return await dbIterator.Lessons()
                .Where(x => x.Abonement.Id == abonementId)
                .ToDto(isStudentLink)
                .ToPageResult(_page, _pageSize);
        }

        public async Task<PageResult<LessonDto>> GetTeacherLastLessons(int teacherId, int _page, int _pageSize)
        {
            return await dbIterator.Lessons()
                .Where(x => x.Abonement.Course.Teacher.User.Id == teacherId)
                .Where(x => x.DateTime < DateTime.UtcNow)
                .ToDto(false)
                .ToPageResult(_page, _pageSize);
        }

        public async Task<LessonDto> GetTeacherLesson(int teacherId, int lessonId)
        {
            return await dbIterator.Lessons()
                .Where(x => x.Abonement.Course.Teacher.User.Id == teacherId)
                .ToDto(false)
                .One(x => x.Id == lessonId);
        }

        public async Task<PageResult<LessonsByDateDto>> GetTeacherFutureLessons(int teacherId, int _page, int _pageSize)
        {
            return await dbIterator.Lessons()
                .Where(x => x.Abonement.Course.Teacher.User.Id == teacherId)
                .Where(x => x.DateTime > DateTime.UtcNow)
                .ToByDateDto(false, calculationService)
                .ToPageResult(_page, _pageSize);
        }

        public async Task<PageResult<LessonDto>> GetStudentLastLessons(int studentId, int _page, int _pageSize)
        {
            return await dbIterator.Lessons()
                .Where(x => x.Abonement.Student.User.Id == studentId)
                .Where(x => x.DateTime < DateTime.UtcNow)
                .ToDto(true)
                .ToPageResult(_page, _pageSize);
        }

        public async Task<PageResult<LessonsByDateDto>> GetStudentFutureLessons(int studentId, int _page, int _pageSize)
        {
            return await dbIterator.Lessons()
                .Where(x => x.Abonement.Student.User.Id == studentId)
                .Where(x => x.DateTime > DateTime.UtcNow)
                .ToByDateDto(true, calculationService)
                .ToPageResult(_page, _pageSize);
        }

        public async Task<LessonDto> GetStudentLesson(int studentId, int lessonId)
        {
            return await dbIterator.Lessons()
                .Where(x => x.Abonement.Student.User.Id == studentId)
                .ToDto(true)
                .One(x => x.Id == lessonId);
        }

        public async Task<int> CancelLesson(int studentId, int lessonId)
        {
            var lesson = await dbIterator.Lessons()
                .Where(x => x.Abonement.Student.User.Id == studentId)
                .One(x => x.Id == lessonId);

            if (lesson.Status != LessonStatus.Waiting) throw new BusinessLogicException("Unchangeable lesson status");

            lesson.Status = LessonStatus.Cancelled;
            lesson.Abonement.CancelledLessons += 1;
            await publisher.Push(new LessonCancelEvent
            {
                StudentName = lesson.Abonement.Student.User.DisplayName,
                StudentUserId = lesson.Abonement.Student.User.Id,
                TeacherName = lesson.Abonement.Course.Teacher.User.DisplayName,
                TeacherUserId = lesson.Abonement.Course.TeacherId,
                CourseName = lesson.Abonement.Course.Name,
                AbonementId = lesson.Abonement.Id,
                LessonId = lesson.Id,
                DateTime = lesson.DateTime,
                TeacherTimezoneOffset = lesson.Abonement.Course.Teacher.User.TimeZoneOffset.Hours,
            });

            await dbIterator.SaveChangesAsync();
            return lesson.Id;
        }

        public async Task<int> ReportLesson(int userId, int lessonId, LessonReportStatus reportStatus, bool isStudentReporting)
        {
            Lesson lesson;
            if (isStudentReporting)
            {
                lesson = await dbIterator.Lessons()
                   .Where(x => x.Abonement.Student.User.Id == userId)
                   .One(x => x.Id == lessonId);
            }
            else
            {
                lesson = lesson = await dbIterator.Lessons()
                   .Where(x => x.Abonement.Course.Teacher.User.Id == userId)
                   .One(x => x.Id == lessonId);
            }

            if (lesson.ReportStatus != LessonReportStatus.Ok) throw new BusinessLogicException("Lesson already reported");
            if (lesson.Status != LessonStatus.Happened) throw new BusinessLogicException("Lesson not happened");
            if (!reportStatus.ToString().StartsWith(isStudentReporting ? "Teacher" : "Student")) throw new BusinessLogicException("Wrong report status");

            lesson.ReportStatus = reportStatus;

            await publisher.Push(new LessonReportEvent
            {
                AllowedAdminIds = (await collector.GetAllowedAdmins([Permission.InspectReportedLessons])).Select(x => x.UserId),
                StudentName = lesson.Abonement.Student.User.DisplayName,
                StudentUserId = lesson.Abonement.Student.User.Id,
                TeacherName = lesson.Abonement.Course.Teacher.User.DisplayName,
                TeacherUserId = lesson.Abonement.Course.TeacherId,
                CourseName = lesson.Abonement.Course.Name,
                AbonementId = lesson.Abonement.Id,
                LessonId = lesson.Id,
                DateTime = lesson.DateTime,
                ReportStatus = reportStatus.ToString(),
            });

            await dbIterator.SaveChangesAsync();
            return lesson.Id;
        }

        public async Task<int> CreateWorkoffLesson(int userId, LessonWorkoffInputDto dto)
        {
            var dateTime = dto.DateTime.ToUniversalTime();

            var abonement = await dbIterator
                .Students()
                .Include(x => x.Abonements)
                .ThenInclude(x => x.Course)
                .ThenInclude(x => x.Teacher)
                .ThenInclude(x => x.User)
                .Include(x => x.Abonements)
                .ThenInclude(x => x.Lessons)
                .Include(x => x.User)
                .Where(x => x.User.Id == userId)
                .SelectMany(x => x.Abonements)
                .One(x => x.Id == dto.AbonementId);

            if (abonement.CancelledLessons == 0) throw new BusinessLogicException("No cancelled lessons to workoff");
            if (!(await teacherIterator.GetTeacherAvailableTime(abonement.Course.Teacher.User.Id)).Contains(dateTime))
                throw new BusinessLogicException("Date is not allowed");

            var lesson = new Lesson
            {
                Abonement = abonement,
                Price = abonement.Course.FreeLessons > abonement.FreeLessonsSpent() ? 0 : abonement.PricePerLesson,
                DateTime = dateTime,
                Status = LessonStatus.Waiting,
                IsReferal = abonement.IsReferal,
                IsWorkoff = true
            };

            //Проверка есть ли такой же слот
            var sameLesson = await dbIterator.Lessons().FirstOrDefaultAsync(x => x.DateTime == dateTime && x.Abonement.Id == abonement.Id);
            if (sameLesson is not null && sameLesson.IsNormal()) throw new BusinessLogicException("Invalid datetime");
            else
            {
                await dbIterator.AddAsync(lesson);
            }
            abonement.CancelledLessons -= 1;

            await dbIterator.SaveChangesAsync();
            await publisher.Push(new WorkoffLessonCreatedEvent
            {
                StudentName = lesson.Abonement.Student.User.DisplayName,
                StudentUserId = lesson.Abonement.Student.User.Id,
                TeacherName = lesson.Abonement.Course.Teacher.User.DisplayName,
                TeacherUserId = lesson.Abonement.Course.Teacher.User.Id,
                TeacherTimezoneOffset = lesson.Abonement.Course.Teacher.User.TimeZoneOffset.Hours,
                CourseName = lesson.Abonement.Course.Name,
                AbonementId = lesson.Abonement.Id,
                LessonId = lesson.Id,
                DateTime = lesson.DateTime
            });
            return lesson.Id;
        }
    }
}
