using CoreConnection.DTOs;
using CoreConnection.Entities;
using CoreDBModel.Constants;
using CoreDBModel.Models;
using Microsoft.EntityFrameworkCore;
using PrimumCore.Exceptions;
using PrimumCore.Extentions;
using System.Linq.Expressions;

namespace PrimumCore.Services.Iterators
{
    public class LessonIterator(PrimumContext context)
    {
        private IQueryable<Lesson> Lessons(Expression<Func<Lesson, bool>>? predicate) => context
            .Set<Lesson>()
            .WhereIf(predicate is not null, predicate!)
            .Include(x => x.Abonement)
            .ThenInclude(x => x.Course)
            .ThenInclude(x => x.Teacher)
            .ThenInclude(x => x.User)
            .Include(x => x.Abonement)
            .ThenInclude(x => x.Student)
            .ThenInclude(x => x.User);

        public async Task<PageResult<LessonDto>> GetAbonementLessons(int abonementId, bool isStudentLink, int _page, int _pageSize)
        {
            return await Lessons(x => x.Abonement.Id == abonementId).ToDto(isStudentLink).ToPageResult(_page, _pageSize);
        }

        public async Task<PageResult<LessonDto>> GetTeacherLessons(int teacherId, int _page, int _pageSize)
        {
            return await Lessons(x => x.Abonement.Course.Teacher.User.Id == teacherId).ToDto(false).ToPageResult(_page, _pageSize);
        }

        public async Task<LessonDto> GetTeacherLesson(int teacherId, int lessonId)
        {
            return await Lessons(x => x.Abonement.Course.Teacher.User.Id == teacherId).ToDto(false).One(x => x.Id == lessonId);
        }

        public async Task<PageResult<LessonDto>> GetTeacherFutureLessons(int teacherId, int _page, int _pageSize)
        {
            return await Lessons(x => x.Abonement.Course.Teacher.User.Id == teacherId)
                .Where(x => x.DateTime > DateTime.Now)
                .ToDto(false)
                .ToPageResult(_page, _pageSize);
        }

        public async Task<PageResult<LessonDto>> GetStudentLessons(int studentId, int _page, int _pageSize)
        {
            return await Lessons(x => x.Abonement.Student.User.Id == studentId).ToDto(true).ToPageResult(_page, _pageSize);
        }

        public async Task<PageResult<LessonDto>> GetStudentFutureLessons(int studentId, int _page, int _pageSize)
        {
            return await Lessons(x => x.Abonement.Student.User.Id == studentId)
                .Where(x => x.DateTime > DateTime.Now)
                .ToDto(false)
                .ToPageResult(_page, _pageSize);
        }

        public async Task<LessonDto> GetStudentLesson(int studentId, int lessonId)
        {
            return await Lessons(x => x.Abonement.Student.User.Id == studentId)
                .ToDto(true)
                .One(x => x.Id == lessonId);
        }
    }
}
