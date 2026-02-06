using CoreConnection.DTOs;
using CoreConnection.Enums;
using Microsoft.EntityFrameworkCore;
using PrimumCore.Exceptions;
using PrimumCore.Models;

namespace PrimumCore.Services.Iterators
{
    public class LessonIterator(IPrimumContext context)
    {
        public async Task<IEnumerable<LessonDto>> GetAbonementLessons(int abonementId, bool isStudentLink)
        {
            var abonement = await context.Set<Abonement>()
                .Include(x => x.Lessons)
                .ThenInclude(x => x.Grading)
                .Include(x => x.Student)
                .ThenInclude(x => x.User)
                .Include(x => x.Course)
                .ThenInclude(x => x.Teacher)
                .ThenInclude(x => x.User)
                .FirstOrDefaultAsync(x => x.AbonementId == abonementId);
            if (abonement is null) { throw new NotFoundException("Abonement"); }

            return abonement.Lessons.Select(x => new LessonDto
            {
                DateTime = x.DateTime,
                CourseName = x.Abonement.Course.Name,
                CourseId = x.Abonement.Course.CourseId,
                TeacherDisplayName = x.Abonement.Course.Teacher.User.DisplayName,
                TeacherId = x.Abonement.Course.Teacher.User.Id,
                StudentDisplayName = x.Abonement.Student.User.DisplayName,
                StudentId = x.Abonement.Student.User.Id,
                LessonLink = isStudentLink ? x.StudentLink : x.TeacherLink,
                AbonementId = x.Abonement.AbonementId,
                Price = x.Price,
                LessonId = x.LessonId,
                LessonStatus = (StatusLesson)x.Status,
                Grade = x.Grading is null ? null : x.Grading.GetFinalGrade()
            });
        }

        public async Task<IEnumerable<LessonDto>> GetTeacherLessons(int teacherId)
        {
            var user = await context.Set<User>()
                .Include(u => u.TeacherProfile)
                .ThenInclude(t => t.Courses)
                .ThenInclude(s => s.Abonements)
                .ThenInclude(a => a.Lessons)
                .ThenInclude(l => l.Grading)
                .Include(u => u.TeacherProfile)
                .ThenInclude(t => t.Courses)
                .ThenInclude(s => s.Abonements)
                .ThenInclude(s => s.Student)
                .ThenInclude(s => s.User)
                .FirstOrDefaultAsync(x => x.Id == teacherId);
            if (user is null || user.TeacherProfile is null) { throw new NotFoundException("Teacher"); }

            return user
                .TeacherProfile
                .Courses
                .SelectMany(x => x.Abonements)
                .SelectMany(x => x.Lessons)
                .Where(l => l.DateTime >= DateTime.Now)
                .Select(l => new LessonDto
                {
                    LessonId = l.LessonId,
                    DateTime = l.DateTime,
                    CourseName = l.Abonement.Course.Name,
                    CourseId = l.Abonement.CourseId,
                    AbonementId = l.AbonementId,
                    Price = l.Price,
                    LessonLink = l.TeacherLink ?? string.Empty,
                    StudentDisplayName = l.Abonement.Student.User.DisplayName,
                    StudentId = l.Abonement.Student.User.Id,
                    TeacherDisplayName = user.DisplayName,
                    TeacherId = user.Id,
                    LessonStatus = (StatusLesson)l.Status,
                    Grade = l.Grading is null ? null : l.Grading.GetFinalGrade()
                })
                .ToArray();
        }

        public async Task<LessonDto> GetTeacherLesson(int teacherId, int lessonId)
        {
            var lesson = (await GetTeacherLessons(teacherId))
                .FirstOrDefault(x => x.LessonId == lessonId);
            if (lesson is null) { throw new NotFoundException("Lesson"); }

            return lesson;
        }

        public async Task<IEnumerable<LessonDto>> GetStudentLessons(int studentId)
        {
            var user = await context.Set<User>()
                .Include(u => u.StudentProfile)
                .ThenInclude(s => s.Abonements)
                .ThenInclude(a => a.Lessons)
                .ThenInclude(a => a.Grading)
                .Include(u => u.StudentProfile)
                .ThenInclude(s => s.Abonements)
                .ThenInclude(a => a.Course)
                .ThenInclude(c => c.Teacher)
                .ThenInclude(c => c.User)
                .FirstOrDefaultAsync(x => x.Id == studentId);
            if (user is null || user.StudentProfile is null) { throw new NotFoundException("Student"); }

            return user
                .StudentProfile
                .Abonements
                .SelectMany(x => x.Lessons)
                .Select(l => new LessonDto
                {
                    LessonId = l.LessonId,
                    DateTime = l.DateTime,
                    CourseName = l.Abonement.Course.Name,
                    CourseId = l.Abonement.CourseId,
                    AbonementId = l.AbonementId,
                    LessonLink = l.StudentLink ?? string.Empty,
                    TeacherDisplayName = l.Abonement.Course.Teacher.User.DisplayName,
                    StudentDisplayName = user.DisplayName,
                    StudentId = user.Id,
                    Price = l.Price,
                    TeacherId = l.Abonement.Course.Teacher.User.Id,
                    LessonStatus = (StatusLesson)l.Status,
                    Grade = l.Grading is null ? null : l.Grading.GetFinalGrade()
                })
                .ToArray();
        }

        public async Task<LessonDto> GetStudentLesson(int studentId, int lessonId)
        {
            var lesson = (await GetStudentLessons(studentId))
                .FirstOrDefault(x => x.LessonId == lessonId);
            if (lesson is null) { throw new NotFoundException("Lesson"); }

            return lesson;
        }
    }
}
