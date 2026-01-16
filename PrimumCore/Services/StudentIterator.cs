using DTO.DTOs;
using DTO.Enums;
using Microsoft.EntityFrameworkCore;
using PrimumCore.Models;

namespace PrimumCore.Services
{
    public class StudentIterator(IPrimumContext context)
    {
        public IEnumerable<LessonDto> GetLessons(int userId)
        {
            var user = context.Set<User>()
                .Include(u => u.StudentProfile)
                .ThenInclude(s => s.Abonements)
                .ThenInclude(a => a.Lessons)
                .Include(u => u.StudentProfile)
                .ThenInclude(s => s.Abonements)
                .ThenInclude(a => a.Course)
                .ThenInclude(c => c.Teacher)
                .FirstOrDefault(x => x.Id == userId);

            if (user is null || user.StudentProfile is null) { throw new Exception("Student not found"); }

            return user
                .StudentProfile
                .Abonements
                .SelectMany(x => x.Lessons)
                .Select(l => new LessonDto
                {
                    DateTime = l.DateTime,
                    CourseName = l.Abonement.Course.Name,
                    CourseId = l.Abonement.CourseId,
                    AbonementId = l.AbonementId,
                    LessonLink = l.StudentLink ?? string.Empty,
                    TeacherName = l.Abonement.Course.Teacher.User.DisplayName,
                    LessonStatus = (LessonStatusDto)l.Status
                });
        }

        public IEnumerable<AbonementDto> GetAbonements(int userId)
        {
            var user = context.Set<User>()
                .Include(u => u.StudentProfile)
                .ThenInclude(s => s.Abonements)
                .ThenInclude(a => a.Course)
                .ThenInclude(c => c.Teacher)
                .FirstOrDefault(x => x.Id == userId);
            if (user is null || user.StudentProfile is null) { throw new Exception("Student not found"); }

            return user
                .StudentProfile
                .Abonements
                .Select(a => new AbonementDto
                {
                    StudentName = user.DisplayName,
                    TeacherName = a.Course.Teacher.User.DisplayName,
                    CourseName = a.Course.Name,
                    CourseId = a.CourseId,
                    PricePerLesson = a.PricePerLesson,
                    AbonementStatus = (AbonementStatusDto)a.AbonementStatus
                });
        }
    }
}
