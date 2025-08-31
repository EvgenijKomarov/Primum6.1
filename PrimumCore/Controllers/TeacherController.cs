using CoreConnection.DTOs;
using DTO.DTOs;
using DTO.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrimumCore.Models;

namespace PrimumCore.Controllers
{
    [ApiController]
    [Route("api/teacher/{userId}")]
    public class TeacherController(IPrimumContext context) : PrimumController
    {
        [HttpGet("lessons")]
        public async Task<IActionResult> GetLessons(int userId)
        {
            var user = context.Set<User>()
                .Include(u => u.TeacherProfile)
                .ThenInclude(t => t.Courses)
                .ThenInclude(s => s.Abonements)
                .ThenInclude(a => a.Lessons)
                .FirstOrDefault(x => x.Id == userId);
            if (user is null || user.TeacherProfile is null) { return NotFound(); }

            return Ok(user
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
                })
            );
        }

        [HttpGet("courses")]
        public async Task<IActionResult> GetCourses(int userId)
        {
            var user = context.Set<User>()
                .Include(u => u.TeacherProfile)
                .ThenInclude(a => a.Courses)
                .FirstOrDefault(x => x.Id == userId);
            if (user is null || user.TeacherProfile is null) { return NotFound(); }

            return Ok(user
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
                }));
        }

        [HttpPost("courses/register")]
        public async Task<IActionResult> RegCourse(int userId, [FromBody] CourseDto newCourse)
        {
            var user = context.Set<User>()
                .Include(u => u.TeacherProfile)
                .FirstOrDefault(x => x.Id == userId);
            if (user is null || user.TeacherProfile is null) { return NotFound(); }

            var course = new Course
            {
                Name = newCourse.Name,
                TeacherId = user.TeacherProfile.TeacherId,
                Price = newCourse.Price,
                FreeLessons= newCourse.FreeLessons,
            };

            context.Set<Course>().Add(course);
            await context.SaveChangesAsync();

            return Ok(course.CourseId);
        }

        [HttpGet("shedules")]
        public async Task<IActionResult> GetShedules(int userId)
        {
            var user = context.Set<User>()
                .Include(u => u.TeacherProfile)
                .ThenInclude(a => a.TeacherShedules)
                .ThenInclude(x => x.AbonementShedule)
                .ThenInclude(x => x.Abonement)
                .ThenInclude(x => x.Student)
                .ThenInclude(x => x.User)
                .FirstOrDefault(x => x.Id == userId);
            if (user is null || user.TeacherProfile is null) { return NotFound(); }

            return Ok(user
                .TeacherProfile
                .TeacherShedules
                .Select(s => new SheduleDto
            {
                DayOfWeek = s.DayOfWeek,
                Time = s.Time,
                StudentName = s.AbonementShedule is null ? 
                    string.Empty : s.AbonementShedule.Abonement.Student.User.DisplayName
            }));
        }

        [HttpPost("shedules/register")]
        public async Task<IActionResult> RegShedules(int userId, [FromBody] SheduleDto newShedule)
        {
            var user = context.Set<User>()
                .Include(u => u.TeacherProfile)
                .FirstOrDefault(x => x.Id == userId);
            if (user is null || user.TeacherProfile is null) { return NotFound(); }

            var shedule = new TeacherShedule
            {
                TeacherId = user.TeacherProfile.TeacherId,
                DayOfWeek = newShedule.DayOfWeek,
                Time = newShedule.Time
            };

            context.Set<TeacherShedule>().Add(shedule);
            await context.SaveChangesAsync();

            return Ok(shedule.TeacherSheduleId);
        }
    }
}
