using CoreConnection.DTOs;
using DTO.DTOs;
using DTO.Enums;
using Microsoft.EntityFrameworkCore;
using PrimumCore.Models;

namespace PrimumCore.Services
{
    public class TeacherIterator(IPrimumContext context)
    {
        public IEnumerable<LessonDto> GetLessons(int userId)
        {
            var user = context.Set<User>()
                .Include(u => u.TeacherProfile)
                .ThenInclude(t => t.Courses)
                .ThenInclude(s => s.Abonements)
                .ThenInclude(a => a.Lessons)
                .FirstOrDefault(x => x.Id == userId);
            if (user is null || user.TeacherProfile is null) { throw new Exception("Teacher not found"); }

            return user
                .TeacherProfile
                .Courses
                .SelectMany(x => x.Abonements)
                .SelectMany(x => x.Lessons)
                .Where(l => l.DateTime >= DateTime.Now)
                .Select(l => new LessonDto
                {
                    DateTime = l.DateTime,
                    CourseName = l.Abonement.Course.Name,
                    CourseId = l.Abonement.CourseId,
                    AbonementId = l.AbonementId,
                    LessonLink = l.TeacherLink ?? string.Empty,
                    StudentName = l.Abonement.Student.User.DisplayName,
                    LessonStatus = (LessonStatusDto)l.Status
                });
        }

        public IEnumerable<CourseDto> GetCourses(int userId)
        {
            var user = context.Set<User>()
                .Include(u => u.TeacherProfile)
                .ThenInclude(a => a.Courses)
                .FirstOrDefault(x => x.Id == userId);
            if (user is null || user.TeacherProfile is null) { throw new Exception("Teacher not found"); }

            return user
                .TeacherProfile
                .Courses
                .Select(c => new CourseDto
                {
                    CourseId = c.CourseId,
                    Name = c.Name,
                    TeacherName = c.Teacher.User.DisplayName,
                    Price = c.Price,
                    MaxLessons = c.MaxLessons,
                    FreeLessons = c.FreeLessons,
                    TeacherAbout = c.Teacher.About,
                    ApproveStatus = (ApproveStatusDto)c.ApproveStatus
                });
        }

        public IEnumerable<SheduleDto> GetShedules(int userId)
        {
            var user = context.Set<User>()
                .Include(u => u.TeacherProfile)
                .ThenInclude(a => a.TeacherShedules)
                .ThenInclude(x => x.AbonementShedule)
                .ThenInclude(x => x.Abonement)
                .ThenInclude(x => x.Student)
                .ThenInclude(x => x.User)
                .FirstOrDefault(x => x.Id == userId);
            if (user is null || user.TeacherProfile is null) { throw new Exception("Teacher not found"); }

            return user
                .TeacherProfile
                .TeacherShedules
                .Select(s => new SheduleDto
                {
                    DayOfWeek = s.DayOfWeek,
                    Time = s.Time,
                    StudentName = s.AbonementShedule is null ?
                    string.Empty : s.AbonementShedule.Abonement.Student.User.DisplayName
                });
        }
    }
}
