using CoreConnection;
using CoreConnection.DTOs;
using CoreConnection.DTOs.Inputs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrimumWebAPI.Extensions;

namespace PrimumWebAPI.Controllers
{
    [Route("teacher/courses")]
    [Authorize]
    public class TeacherCourseController(TeacherClient client) : DefaultController
    {
        /// <summary>
        /// Все курсы преподавателя
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<CourseDtoPageResult>> GetCourses(
            [FromQuery] int page = 0,
            [FromQuery] int pageSize = 10)
            => Ok(await client.CoursesAsync(User.GetUserId(), page, pageSize));

        /// <summary>
        /// Конкретный курс преподавателя
        /// </summary>
        /// <returns></returns>
        [HttpGet("{courseId}")]
        public async Task<ActionResult<CourseDto>> GetCourses([FromRoute] int courseId)
            => Ok(await client.CourseAsync(courseId, User.GetUserId()));

        /// <summary>
        /// Создать курс
        /// </summary>
        /// <param name="courseDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<int>> CreateCourse([FromBody] CourseInputDto courseDto)
            => Ok(await client.CourseCreateAsync(User.GetUserId(), courseDto));

        /// <summary>
        /// Активировать/деактивировать курс, чтобы он отображался в общем списке и на него могли подписываться ученики, либо скрыть его от учеников и не дать им на него подписаться
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="activityStatus">Статус активности (active/non-active)</param>
        /// <returns></returns>
        [HttpPatch("{courseId}/activity")]
        public async Task<ActionResult<int>> ActivateCourse([FromRoute] int courseId, [FromBody] bool activityStatus)
        {
            if (activityStatus)
            {
                return Ok(await client.CourseActivateAsync(User.GetUserId(), courseId));
            }
            else
            {
                return Ok(await client.CourseDeactivateAsync(User.GetUserId(), courseId));
            }
        }

        /// <summary>
        /// Реадктирование курса. При изменении названия и описания, курс отправляется заново на процедуру утверждения и пропадает из видимости у остальных пользователей
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="courseDto"></param>
        /// <returns></returns>
        [HttpPut("{courseId}")]
        public async Task<ActionResult<int>> EditCourse([FromRoute] int courseId, [FromBody] CourseInputDto courseDto = null!)
            => Ok(await client.CourseEditAsync(User.GetUserId(), courseId, courseDto));
    }
}
