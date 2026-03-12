using CoreConnection.DTOs;
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

        private IQueryable<LessonDto> ToDto(IQueryable<Lesson> queryable, bool isStudentLink) => queryable
            .Select(x => new LessonDto
            {
                DateTime = x.DateTime,
                CourseName = x.Abonement.Course.Name,
                CourseId = x.Abonement.Course.CourseId,
                TeacherDisplayName = x.Abonement.Course.Teacher.User.DisplayName,
                TeacherId = x.Abonement.Course.Teacher.User.Id,
                StudentDisplayName = x.Abonement.Student.User.DisplayName,
                StudentId = x.Abonement.Student.User.Id,
                LessonLink = isStudentLink ? (x.StudentLink ?? string.Empty) : (x.TeacherLink ?? string.Empty),
                AbonementId = x.Abonement.AbonementId,
                Price = x.Price,
                LessonId = x.LessonId,
                LessonStatus = x.Status,
                Grade = x.Grading == null ? null : x.Grading.GetFinalGrade()
            });

        public async Task<IEnumerable<LessonDto>> GetAbonementLessons(int abonementId, bool isStudentLink)
        {
            return await ToDto(
                    Lessons(x => x.Abonement.AbonementId == abonementId), isStudentLink
                ).ToArrayAsync();
        }

        public async Task<IEnumerable<LessonDto>> GetTeacherLessons(int teacherId)
        {
            return await ToDto(
                    Lessons(x => x.Abonement.Course.Teacher.User.Id == teacherId), false
                ).ToArrayAsync();
        }

        public async Task<LessonDto> GetTeacherLesson(int teacherId, int lessonId)
        {
            return await ToDto(
                    Lessons(x => x.Abonement.Course.Teacher.User.Id == teacherId), false
                ).FirstOrDefaultAsync(x => x.LessonId == lessonId) ?? throw new NotFoundException("Lesson");
        }

        public async Task<IEnumerable<LessonDto>> GetTeacherFutureLessons(int teacherId)
        {
            return await ToDto(
                    Lessons(x => x.Abonement.Course.Teacher.User.Id == teacherId).Where(x => x.DateTime > DateTime.Now).OrderBy(x => x.DateTime), 
                    false
                ).ToArrayAsync();
        }

        public async Task<IEnumerable<LessonDto>> GetStudentLessons(int studentId)
        {
            return await ToDto(
                    Lessons(x => x.Abonement.Student.User.Id == studentId), true
                ).ToArrayAsync();
        }

        public async Task<IEnumerable<LessonDto>> GetStudentFutureLessons(int studentId)
        {
            return await ToDto(
                    Lessons(x => x.Abonement.Student.User.Id == studentId).Where(x => x.DateTime > DateTime.Now).OrderBy(x => x.DateTime),
                    true
                ).ToArrayAsync();
        }

        public async Task<LessonDto> GetStudentLesson(int studentId, int lessonId)
        {
            return await ToDto(
                    Lessons(x => x.Abonement.Student.User.Id == studentId), true
                ).FirstOrDefaultAsync(x => x.LessonId == lessonId) ?? throw new NotFoundException("Lesson");
        }
    }
}
