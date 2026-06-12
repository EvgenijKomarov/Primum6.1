using CoreConnection.DTOs;
using PrimumCore.Entities;
using CoreDBModel.Constants;
using CoreDBModel.Models;
using Microsoft.EntityFrameworkCore;
using PrimumCore.Exceptions;
using PrimumCore.Extentions;
using System.Linq.Expressions;

namespace PrimumCore.Services.Iterators
{
    public class LessonIterator(DatabaseIterator dbIterator)
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
                .ToByDateDto(false)
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
                .ToByDateDto(false)
                .ToPageResult(_page, _pageSize);
        }

        public async Task<LessonDto> GetStudentLesson(int studentId, int lessonId)
        {
            return await dbIterator.Lessons()
                .Where(x => x.Abonement.Student.User.Id == studentId)
                .ToDto(true)
                .One(x => x.Id == lessonId);
        }
    }
}
