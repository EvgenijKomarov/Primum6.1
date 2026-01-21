using Microsoft.AspNetCore.Mvc;
using PrimumCore.Services.Iterators;

namespace PrimumCore.Controllers
{
    [ApiController]
    [Route("api/common")]
    public class PublicController(CommonIterator iterator) : PrimumController
    {

        [HttpGet("user/{id}")]
        public async Task<IActionResult> GetUser([FromRoute] int userId) => Ok(await iterator.GetUser(userId));

        [HttpGet("teacher/{teacherid}")]
        public async Task<IActionResult> GetTeacher([FromRoute] int teacherId) => Ok(await iterator.GetTeacher(teacherId));

        [HttpGet("teachers")]
        public async Task<IActionResult> GetTeachers() => Ok(await iterator.GetTeachers(true));

        [HttpGet("courses-by-teacher/{teacherId}")]
        public async Task<IActionResult> GetCoursesByTeacher([FromRoute] int teacherId) 
            => Ok(await iterator.GetCoursesByTeacher(teacherId, true));

        [HttpGet("teacher-shedules/{teacherId}")]
        public async Task<IActionResult> GetTeacherShedules([FromRoute] int teacherId)
            => Ok(await iterator.GetTeacherShedules(teacherId, true));

        [HttpGet("themes")]
        public async Task<IActionResult> GetThemes()
            => Ok(await iterator.GetThemes(true));

        [HttpGet("theme/{themeId}")]
        public async Task<IActionResult> GetTheme([FromRoute] int themeId)
            => Ok(await iterator.GetTheme(themeId));

        [HttpGet("course/{courseId}")]
        public async Task<IActionResult> GetCourse([FromRoute] int courseId)
            => Ok(await iterator.GetCourse(courseId));

        [HttpGet("courses")]
        public async Task<IActionResult> GetCourses()
            => Ok(await iterator.GetCourses(true));

        [HttpGet("courses-by-theme/{themeId}")]
        public async Task<IActionResult> GetCoursesByTheme([FromRoute] int themeId)
            => Ok(await iterator.GetCoursesByTheme(themeId, true));
    }
}
