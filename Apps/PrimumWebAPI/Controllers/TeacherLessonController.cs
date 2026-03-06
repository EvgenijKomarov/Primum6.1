using CoreConnection;
using CoreConnection.DTOs;
using CoreConnection.DTOs.Inputs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrimumWebAPI.Extensions;

namespace PrimumWebAPI.Controllers
{
    [Route("teacher/lessons")]
    [Authorize]
    public class TeacherLessonController(TeacherClient client) : DefaultController
    {
        /// <summary>
        /// Все занятия, включая прошедшие и будущие
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

        /// <summary>
        /// Выставить оценку занятию
        /// </summary>
        /// <param name="lessonId">Id занятия</param>
        /// <param name="gradingDto">Дто с оценками</param>
        /// <returns></returns>
        [HttpPost("{lessonId}/grade")]
        public async Task<ActionResult<int>> GradeLesson([FromRoute] int lessonId, [FromBody] GradingInputDto gradingDto = null!)
            => Ok(await client.LessonGradeAsync(User.GetUserId(), lessonId, gradingDto));
    }
}
