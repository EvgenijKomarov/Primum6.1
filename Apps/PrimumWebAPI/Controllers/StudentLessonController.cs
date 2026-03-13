using CoreConnection;
using CoreConnection.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrimumWebAPI.Extensions;

namespace PrimumWebAPI.Controllers
{
    [Route("student/lessons")]
    [Authorize]
    public class StudentLessonController(StudentClient client) : DefaultController
    {
        /// <summary>
        /// Все занятия ученика, включая прошедшие и будущие
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<LessonDtoPageResult>> GetLessons(
            [FromQuery] int page = 0,
            [FromQuery] int pageSize = 10)
            => Ok(await client.LessonsAsync(User.GetUserId(), page, pageSize));

        /// <summary>
        /// Только будущие занятия
        /// </summary>
        /// <returns></returns>
        [HttpGet("future")]
        public async Task<ActionResult<LessonDtoPageResult>> GetFututreLessons(
            [FromQuery] int page = 0,
            [FromQuery] int pageSize = 10)
            => Ok(await client.FutureLessonsAsync(User.GetUserId(), page, pageSize));

        /// <summary>
        /// Конкретное занятие
        /// </summary>
        /// <param name="lessonId"></param>
        /// <returns></returns>
        [HttpGet("{lessonId}")]
        public async Task<ActionResult<LessonDto>> GetLesson([FromRoute] int lessonId)
            => Ok(await client.LessonAsync(User.GetUserId(), lessonId));
    }
}
