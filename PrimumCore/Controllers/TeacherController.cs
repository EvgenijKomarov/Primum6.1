using CoreConnection.DTOs;
using DTO.DTOs;
using DTO.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrimumCore.Models;
using PrimumCore.Services;

namespace PrimumCore.Controllers
{
    [ApiController]
    [Route("api/teacher/{userId}")]
    public class TeacherController(TeacherIterator iterator) : PrimumController
    {
        [HttpGet("lessons")]
        public async Task<IActionResult> GetLessons(int userId) => Ok(await iterator.GetLessons(userId));

        [HttpGet("courses")]
        public async Task<IActionResult> GetCourses(int userId) => Ok(await iterator.GetCourses(userId));

        [HttpGet("shedules")]
        public async Task<IActionResult> GetShedules(int userId) => Ok(await iterator.GetShedules(userId));

        [HttpGet("course/edit")]
        public async Task<IActionResult> EditCourse(int userId, [FromBody] CourseDto courseDto) => Ok(await iterator.EditCourse(userId, courseDto));

        [HttpGet("course/create")]
        public async Task<IActionResult> CreateCourse(int userId, [FromBody] CourseDto courseDto) => Ok(await iterator.CreateCourse(userId, courseDto));

        [HttpGet("course/activate")]
        public async Task<IActionResult> ActivateCourse(int userId, [FromBody] int courseId) => Ok(await iterator.ActivateCourse(userId, courseId));

        [HttpGet("course/deactivate")]
        public async Task<IActionResult> DeactivateCourse(int userId, [FromBody] int courseId) => Ok(await iterator.DeactivateCourse(userId, courseId));

        [HttpGet("shedule/create")]
        public async Task<IActionResult> CreateShedule(int userId, [FromBody] TeacherSheduleDto sheduleDto) => Ok(await iterator.CreateShedule(userId, sheduleDto));

        [HttpGet("shedule/delete")]
        public async Task<IActionResult> DeleteShedule(int userId, [FromBody] int sheduleId) => Ok(await iterator.DeleteShedule(userId, sheduleId));

        [HttpGet("abonements")]
        public async Task<IActionResult> GetAbonements(int userId) => Ok(await iterator.GetAbonements(userId));
    }
}
