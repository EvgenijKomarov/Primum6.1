using CoreConnection.DTOs;
using Microsoft.AspNetCore.Mvc;
using PrimumCore.Services.Iterators;

namespace PrimumCore.Controllers
{
    [ApiController]
    [Route("api/teacher/{userId}")]
    public class TeacherController(
        TeacherIterator teacherIterator,
        CommonIterator commonIterator) : PrimumController
    {
        [HttpGet("lessons")]
        public async Task<IActionResult> GetLessons([FromRoute] int userId) 
            => Ok(await teacherIterator.GetLessons(userId, userId));

        [HttpGet("courses")]
        public async Task<IActionResult> GetCourses([FromRoute] int userId) 
            => Ok(await commonIterator.GetCoursesByTeacher(userId, false));

        [HttpGet("shedules")]
        public async Task<IActionResult> GetShedules([FromRoute] int userId) 
            => Ok(await commonIterator.GetTeacherShedules(userId, false));

        [HttpGet("abonements")]
        public async Task<IActionResult> GetAbonements([FromRoute] int userId)
            => Ok(await teacherIterator.GetAbonements(userId, userId));

        [HttpPut("course/edit")]
        public async Task<IActionResult> EditCourse([FromRoute] int userId, [FromBody] CourseDto courseDto) 
            => Ok(await teacherIterator.EditCourse(userId, courseDto));

        [HttpPost("course/create")]
        public async Task<IActionResult> CreateCourse([FromRoute] int userId, [FromBody] CourseDto courseDto) 
            => Ok(await teacherIterator.CreateCourse(userId, courseDto));

        [HttpPatch("course/activate")]
        public async Task<IActionResult> ActivateCourse([FromRoute] int userId, [FromQuery] int courseId) 
            => Ok(await teacherIterator.ActivateCourse(userId, courseId));

        [HttpPatch("course/deactivate")]
        public async Task<IActionResult> DeactivateCourse([FromRoute] int userId, [FromQuery] int courseId) 
            => Ok(await teacherIterator.DeactivateCourse(userId, courseId));

        [HttpPost("shedule/create")]
        public async Task<IActionResult> CreateShedule([FromRoute] int userId, [FromBody] TeacherSheduleDto sheduleDto) 
            => Ok(await teacherIterator.CreateShedule(userId, sheduleDto));

        [HttpDelete("shedule/delete")]
        public async Task<IActionResult> DeleteShedule([FromRoute] int userId, [FromQuery] int sheduleId) 
            => Ok(await teacherIterator.DeleteShedule(userId, sheduleId));
    }
}
