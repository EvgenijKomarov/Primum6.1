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
        public async Task<ActionResult<IEnumerable<TeacherProfileDto>>> GetTeachers() => Ok(await client.TeachersAsync());

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
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetCoursesByTeacher([FromRoute] int teacherId)
            => Ok(await client.CoursesByTeacherAsync(teacherId));

        /// <summary>
        /// Все расписания преподавателя
        /// </summary>
        /// <param name="teacherId">Id преподавателя</param>
        /// <returns></returns>
        [HttpGet("{teacherId}/shedules")]
        public async Task<ActionResult<IEnumerable<TeacherSheduleDto>>> GetTeacherShedules([FromRoute] int teacherId)
            => Ok(await client.TeacherShedulesAsync(teacherId));
    }
}
