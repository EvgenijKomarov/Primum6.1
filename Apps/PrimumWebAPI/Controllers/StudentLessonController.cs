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
        public async Task<ActionResult<IEnumerable<LessonDto>>> GetLessons()
            => Ok(await client.LessonsAsync(User.GetUserId()));

        /// <summary>
        /// Только будущие занятия
        /// </summary>
        /// <returns></returns>
        [HttpGet("future")]
        public async Task<ActionResult<IEnumerable<LessonDto>>> GetFututreLessons()
            => Ok(await client.FutureLessonsAsync(User.GetUserId()));

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
