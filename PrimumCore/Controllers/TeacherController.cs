using CoreConnection.DTOs;
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
        public async Task<IActionResult> GetLessons([FromRoute] int userId) => Ok(await iterator.GetLessons(userId));

        [HttpGet("courses")]
        public async Task<IActionResult> GetCourses([FromRoute] int userId) => Ok(await iterator.GetCourses(userId));

        [HttpGet("shedules")]
        public async Task<IActionResult> GetShedules([FromRoute] int userId) => Ok(await iterator.GetShedules(userId));

        [HttpPut("course/edit")]
        public async Task<IActionResult> EditCourse([FromRoute] int userId, [FromBody] CourseDto courseDto) => Ok(await iterator.EditCourse(userId, courseDto));

        [HttpPost("course/create")]
        public async Task<IActionResult> CreateCourse([FromRoute] int userId, [FromBody] CourseDto courseDto) => Ok(await iterator.CreateCourse(userId, courseDto));

        [HttpPatch("course/activate")]
        public async Task<IActionResult> ActivateCourse([FromRoute] int userId, [FromQuery] int courseId) => Ok(await iterator.ActivateCourse(userId, courseId));

        [HttpPatch("course/deactivate")]
        public async Task<IActionResult> DeactivateCourse([FromRoute] int userId, [FromQuery] int courseId) => Ok(await iterator.DeactivateCourse(userId, courseId));

        [HttpPost("shedule/create")]
        public async Task<IActionResult> CreateShedule([FromRoute] int userId, [FromBody] TeacherSheduleDto sheduleDto) => Ok(await iterator.CreateShedule(userId, sheduleDto));

        [HttpDelete("shedule/delete")]
        public async Task<IActionResult> DeleteShedule([FromRoute] int userId, [FromQuery] int sheduleId) => Ok(await iterator.DeleteShedule(userId, sheduleId));

        [HttpGet("abonements")]
        public async Task<IActionResult> GetAbonements([FromRoute] int userId) => Ok(await iterator.GetAbonements(userId));
    }
}
