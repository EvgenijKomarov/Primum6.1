using Common.Utilities;
using CoreConnection.DTOs;
using CoreDBModel.Constants;
using CoreDBModel.Models;
using CoreDBModel.Models.Enums;
using Microsoft.EntityFrameworkCore;
using PrimumCore.Entities;
using PrimumCore.Exceptions;
using PrimumCore.Extentions;
using PublishServiceConnection;
using PublishServiceConnection.Events;
using System.Linq.Expressions;

namespace PrimumCore.Services.Iterators
{
    public class LessonIterator(DatabaseIterator dbIterator, EarningCalculationService calculationService, PublisherService publisher)
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

        public async Task<int> ChangeLessonStatus(int studentId, int lessonId)
        {
            var lesson = await dbIterator.Lessons()
                .Where(x => x.Abonement.Student.User.Id == studentId)
                .One(x => x.Id == lessonId);

            if (!(lesson.Status == LessonStatus.Freezed || lesson.Status == LessonStatus.Waiting)) throw new BusinessLogicException("Unchangeable lesson status");
            if (lesson.Status == LessonStatus.Freezed && lesson.Abonement.AbonementStatus == AbonementStatus.Freezed) throw new BusinessLogicException("Abonement is freezed");

            lesson.Status = lesson.Status == LessonStatus.Waiting ? LessonStatus.Freezed : LessonStatus.Waiting;
            await publisher.Push(new LessonChangeStatusEvent
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
                IsBecameFreezed = lesson.Status == LessonStatus.Freezed,
            });

            await dbIterator.SaveChangesAsync();
            return lesson.Id;
        }
    }
}
