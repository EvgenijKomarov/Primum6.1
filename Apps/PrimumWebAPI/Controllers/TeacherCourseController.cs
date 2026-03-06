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
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetCourses()
            => Ok(await client.CoursesAsync(User.GetUserId()));

        /// <summary>
        /// Все курсы преподавателя
        /// </summary>
        /// <returns></returns>
        [HttpGet("{courseId}")]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetCourses([FromRoute] int courseId)
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
        /// Активировать курс, чтобы он отображался в общем списке и на него могли подписываться ученики.
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        [HttpPatch("{courseId}/activate")]
        public async Task<ActionResult<int>> ActivateCourse([FromRoute] int courseId)
            => Ok(await client.CourseActivateAsync(User.GetUserId(), courseId));

        /// <summary>
        /// Деактивировать курс, чтобы скрыть его от учеников и не дать им на него подписаться
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        [HttpPatch("{courseId}/deactivate")]
        public async Task<ActionResult<int>> DeactivateCourse([FromRoute] int courseId)
            => Ok(await client.CourseDeactivateAsync(User.GetUserId(), courseId));

        /// <summary>
        /// Реадктирование курса. Применится только Price, FreeLessons, и MaxLessonsMaxLessons. Остальные поля не редактируются, так как после редактирования модерация не предполагается
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="courseDto"></param>
        /// <returns></returns>
        [HttpPut("{courseId}/edit")]
        public async Task<ActionResult<int>> EditCourse([FromRoute] int courseId, [FromBody] CourseInputDto courseDto = null!)
            => Ok(await client.CourseEditAsync(User.GetUserId(), courseId, courseDto));
    }
}
