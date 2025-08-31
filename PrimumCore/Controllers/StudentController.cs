using CoreConnection.DTOs;
using DTO.DTOs;
using DTO.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrimumCore.Models;
using PrimumPlatformModel.Models.Enums;
using System.Net.Http;

namespace PrimumCore.Controllers
{
    [ApiController]
    [Route("api/student/{userId}")]
    public class StudentController(IPrimumContext context) : PrimumController
    {
        [HttpGet("lessons")]
        public async Task<IActionResult> GetLessons(int userId)
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
            if (user is null || user.StudentProfile is null) { return NotFound(); }

            return Ok(user
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
                })
            );
        }

        [HttpGet("abonements")]
        public async Task<IActionResult> GetAbonements(int userId)
        {
            var user = context.Set<User>()
                .Include(u => u.StudentProfile)
                .ThenInclude(s => s.Abonements)
                .ThenInclude(a => a.Course)
                .ThenInclude(c => c.Teacher)
                .FirstOrDefault(x => x.Id == userId);
            if (user is null || user.StudentProfile is null) { return NotFound(); }

            return Ok(user
                .StudentProfile
                .Abonements
                .Select(a => new AbonementDto
                {
                    StudentName = user.DisplayName,
                    TeacherName = a.Course.Teacher.User.DisplayName,
                    CourseName = a.Course.Name,
                    CourseId = a.CourseId,
                    PricePerLesson = a.PricePerLesson,
                    PaidLessons = a.PaidLessons,
                    AbonementStatus = (AbonementStatusDto)a.AbonementStatus
                }));
        }

        [HttpPost("subscribe-to-course/{courseId}")]
        public async Task<IActionResult> RegToCourse(int userId, int courseId, [FromBody] SheduleDto teacherSheduleDto)
        {
            var user = context.Set<User>()
                .Include(u => u.StudentProfile)
                .ThenInclude(s => s.Abonements)
                .FirstOrDefault(x => x.Id == userId);
            if (user is null || user.StudentProfile is null) { return NotFound(); }

            var abonement = user.StudentProfile.Abonements.FirstOrDefault(x => x.CourseId == courseId);
            var course = context.Set<Course>()
                .Include(x => x.Teacher)
                .First(x => x.CourseId == courseId);
            if (abonement is null) 
            {
                abonement = new Abonement
                {
                    CourseId = courseId,
                    PricePerLesson = course.Price,
                    StudentId = user.StudentProfile.StudentId
                };
                context.Set<Abonement>().Add(abonement);
            }

            var teacherShedule = context.Set<TeacherShedule>()
                .Include(x => x.AbonementShedule)
                .First(x =>
                x.TeacherId == course.TeacherId &&
                x.DayOfWeek == teacherSheduleDto.DayOfWeek &&
                x.Time == teacherSheduleDto.Time);

            if(teacherShedule.AbonementShedule is not null) {
                teacherShedule.AbonementShedule = new AbonementShedule
                {
                    AbonementId = abonement.AbonementId
                };
            }

            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
