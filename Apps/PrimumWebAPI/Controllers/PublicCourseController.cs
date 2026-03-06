using CoreConnection;
using CoreConnection.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PrimumWebAPI.Controllers
{
    [Route("public/courses")]
    [AllowAnonymous]
    public class PublicCourseController(PublicClient client) : DefaultController
    {
        /// <summary>
        /// Все курсы
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetCourses()
            => Ok(await client.CoursesAsync());

        /// <summary>
        /// Конкретный курс
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        [HttpGet("{courseId}")]
        public async Task<ActionResult<CourseDto>> GetCourse([FromRoute] int courseId)
            => Ok(await client.CourseAsync(courseId));

        /// <summary>
        /// Курсы по теме
        /// </summary>
        /// <param name="themeId">Id темы</param>
        /// <returns></returns>
        [HttpGet("by-theme/{themeId}")]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetCoursesByTheme([FromRoute] int themeId)
            => Ok(await client.CoursesByThemeAsync(themeId));
    }
}
