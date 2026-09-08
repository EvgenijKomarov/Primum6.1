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
        [HttpGet("last")]
        public async Task<ActionResult<LessonDtoPageResult>> GetLastLessons(
            [FromQuery] int page = 0,
            [FromQuery] int pageSize = 10)
            => Ok(await client.LastLessonsAsync(User.GetUserId(), page, pageSize));

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

        /// <summary>
        /// Отменить занятие
        /// </summary>
        /// <param name="lessonId"></param>
        /// <returns></returns>
        [HttpPatch("{lessonId}/cancel")]
        public async Task<ActionResult<int>> CancelLesson([FromRoute] int lessonId)
            => Ok(await client.LessonCancelAsync(User.GetUserId(), lessonId));

        /// <summary>
        /// Пожаловаться на занятие
        /// </summary>
        /// <param name="lessonId"></param>
        /// <param name="reportStatus">тип жалобы</param>
        /// <returns></returns>
        [HttpPatch("{lessonId}/report")]
        public async Task<ActionResult<int>> ReportLesson([FromRoute] int lessonId, [FromBody] LessonReportStatus reportStatus)
            => Ok(await client.LessonReportAsync(User.GetUserId(), lessonId, reportStatus));
    }
}
