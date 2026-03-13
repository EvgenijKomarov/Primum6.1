using CoreConnection;
using CoreConnection.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PrimumWebAPI.Controllers
{
    [Route("public/teachers")]
    [AllowAnonymous]
    public class PublicTeacherController(PublicClient client) : DefaultController
    {

        /// <summary>
        /// Все доступные преподаватели
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<TeacherProfileDtoPageResult>> GetTeachers(
            [FromQuery] int page = 0,
            [FromQuery] int pageSize = 10)
            => Ok(await client.TeachersAsync(page, pageSize));

        /// <summary>
        /// Информация о преподавателе
        /// </summary>
        /// <param name="teacherId">Id преподавателя</param>
        /// <returns></returns>
        [HttpGet("{teacherId}")]
        public async Task<ActionResult<TeacherProfileDto>> GetTeacher([FromRoute] int teacherId) => Ok(await client.TeacherAsync(teacherId));

        /// <summary>
        /// Все курсы преподавателя
        /// </summary>
        /// <param name="teacherId">Id преподавателя</param>
        /// <returns></returns>
        [HttpGet("{teacherId}/courses")]
        public async Task<ActionResult<CourseDtoPageResult>> GetCoursesByTeacher(
            [FromRoute] int teacherId,
            [FromQuery] int page = 0,
            [FromQuery] int pageSize = 10)
            => Ok(await client.CoursesByTeacherAsync(teacherId, page, pageSize));

        /// <summary>
        /// Все расписания преподавателя
        /// </summary>
        /// <param name="teacherId">Id преподавателя</param>
        /// <returns></returns>
        [HttpGet("{teacherId}/shedules")]
        public async Task<ActionResult<TeacherSheduleDtoPageResult>> GetTeacherShedules(
            [FromRoute] int teacherId,
            [FromQuery] int page = 0,
            [FromQuery] int pageSize = 10)
            => Ok(await client.TeacherShedulesAsync(teacherId, page, pageSize));
    }
}
