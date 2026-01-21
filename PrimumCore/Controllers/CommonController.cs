using Microsoft.AspNetCore.Mvc;
using PrimumCore.Services.Iterators;

namespace PrimumCore.Controllers
{
    [ApiController]
    [Route("api/common")]
    public class CommonController(CommonIterator iterator) : PrimumController
    {
        [HttpGet("teacher/{teacherid}")]
        public async Task<IActionResult> GetTeacher([FromRoute] int teacherId) => Ok(await iterator.GetTeacher(teacherId));

        [HttpGet("teachers")]
        public async Task<IActionResult> GetTeachers() => Ok(await iterator.GetTeachers());

        [HttpGet("courses-by-teacher/{teacherId}")]
        public async Task<IActionResult> GetCoursesByTeacher([FromRoute] int teacherId) 
            => Ok(await iterator.GetCoursesByTeacher(teacherId));

        [HttpGet("teacher-shedules/{teacherId}")]
        public async Task<IActionResult> GetTeacherShedules([FromRoute] int teacherId)
            => Ok(await iterator.GetTeacherShedules(teacherId));

        [HttpGet("themes")]
        public async Task<IActionResult> GetThemes()
            => Ok(await iterator.GetThemes());

        [HttpGet("theme/{themeId}")]
        public async Task<IActionResult> GetTheme([FromRoute] int themeId)
            => Ok(await iterator.GetTheme(themeId));

        [HttpGet("course/{courseId}")]
        public async Task<IActionResult> GetCourse([FromRoute] int courseId)
            => Ok(await iterator.GetCourse(courseId));

        [HttpGet("courses")]
        public async Task<IActionResult> GetCourses()
            => Ok(await iterator.GetCourses());

        [HttpGet("courses-by-theme/{themeId}")]
        public async Task<IActionResult> GetCoursesByTheme([FromRoute] int themeId)
            => Ok(await iterator.GetCoursesByTheme(themeId));
    }
}
