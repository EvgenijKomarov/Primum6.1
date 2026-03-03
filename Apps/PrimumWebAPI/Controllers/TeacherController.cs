using CoreConnection;
using CoreConnection.DTOs;
using CoreConnection.DTOs.Inputs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrimumWebAPI.Extensions;

namespace PrimumWebAPI.Controllers
{
    [ApiController]
    [Route("api/teacher")]
    [Tags("Teacher")]
    [Authorize]
    public class TeacherController(TeacherClient client): DefaultController
    {
        /// <summary>
        /// Профиль преподавателя
        /// </summary>
        /// <returns></returns>
        [HttpGet("profile")]
        public async Task<ActionResult<TeacherProfileDto>> GetTeacherProfile()
            => Ok(await client.ProfileAsync(User.GetUserId()));

        /// <summary>
        /// Все занятия, включая прошедшие и будущие
        /// </summary>
        /// <returns></returns>
        [HttpGet("lessons")]
        public async Task<ActionResult<IEnumerable<LessonDto>>> GetLessons()
            => Ok(await client.LessonsAsync(User.GetUserId()));

        /// <summary>
        /// Только будущие занятия
        /// </summary>
        /// <returns></returns>
        [HttpGet("future-lessons")]
        public async Task<ActionResult<IEnumerable<LessonDto>>> GetFututreLessons()
            => Ok(await client.FutureLessonsAsync(User.GetUserId()));

        /// <summary>
        /// Конкретное занятие
        /// </summary>
        /// <param name="lessonId"></param>
        /// <returns></returns>
        [HttpGet("lesson/{lessonId}")]
        public async Task<ActionResult<LessonDto>> GetLesson([FromRoute] int lessonId)
            => Ok(await client.LessonAsync(User.GetUserId(), lessonId));

        /// <summary>
        /// Все курсы преподавателя
        /// </summary>
        /// <returns></returns>
        [HttpGet("courses")]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetCourses()
            => Ok(await client.CoursesAsync(User.GetUserId()));

        /// <summary>
        /// Все расписания преподавателя
        /// </summary>
        /// <returns></returns>
        [HttpGet("shedules")]
        public async Task<ActionResult<IEnumerable<TeacherSheduleDto>>> GetShedules()
            => Ok(await client.ShedulesAsync(User.GetUserId()));

        /// <summary>
        /// Конкретное расписание
        /// </summary>
        /// <param name="sheduleId"></param>
        /// <returns></returns>
        [HttpGet("shedule/{sheduleId}")]
        public async Task<ActionResult<TeacherSheduleDto>> GetShedule([FromRoute] int sheduleId)
            => Ok(await client.SheduleAsync(User.GetUserId(), sheduleId));

        /// <summary>
        /// Все абонементы учеников, подписанные на курсы преподавателя
        /// </summary>
        /// <returns></returns>
        [HttpGet("abonements")]
        public async Task<ActionResult<IEnumerable<AbonementDto>>> GetAbonements()
            => Ok(await client.AbonementsAsync(User.GetUserId()));

        /// <summary>
        /// Конкретный абонемент ученика, подписанного на курс преподавателя
        /// </summary>
        /// <param name="abonementId"></param>
        /// <returns></returns>
        [HttpGet("abonement/{abonementId}")]
        public async Task<ActionResult<AbonementDto>> GetAbonement([FromRoute] int abonementId)
            => Ok(await client.AbonementAsync(User.GetUserId(), abonementId));

        /// <summary>
        /// Расписания абонемента ученика, подписанного на курс преподавателя
        /// </summary>
        /// <param name="abonementId"></param>
        /// <returns></returns>
        [HttpGet("abonement-shedules/{abonementId}")]
        public async Task<ActionResult<IEnumerable<StudentSheduleDto>>> GetAbonementShedules([FromRoute] int abonementId)
            => Ok(await client.AbonementShedulesAsync(User.GetUserId(), abonementId));

        /// <summary>
        /// Все занятия абонемента ученика, подписанного на курс преподавателя, и прошедшие и будущие
        /// </summary>
        /// <param name="abonementId"></param>
        /// <returns></returns>
        [HttpGet("abonement-lessons/{abonementId}")]
        public async Task<ActionResult<IEnumerable<LessonDto>>> GetAbonementLessons([FromRoute] int abonementId)
            => Ok(await client.AbonementLessonsAsync(User.GetUserId(), abonementId));

        /// <summary>
        /// Реадктирование курса. Применится только Price, FreeLessons, и MaxLessonsMaxLessons. Остальные поля не редактируются, так как после редактирования модерация не предполагается
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="courseDto"></param>
        /// <returns></returns>
        [HttpPut("course-edit/{courseId}")]
        public async Task<ActionResult<int>> EditCourse([FromRoute] int courseId, [FromBody] CourseInputDto courseDto = null!)
            => Ok(await client.CourseEditAsync(User.GetUserId(), courseId, courseDto));

        /// <summary>
        /// Создать курс
        /// </summary>
        /// <param name="courseDto"></param>
        /// <returns></returns>
        [HttpPost("course-create")]
        public async Task<ActionResult<int>> CreateCourse([FromBody] CourseInputDto courseDto)
            => Ok(await client.CourseCreateAsync(User.GetUserId(), courseDto));

        /// <summary>
        /// Активировать курс, чтобы он отображался в общем списке и на него могли подписываться ученики.
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        [HttpPatch("course-activate/{courseId}")]
        public async Task<ActionResult<int>> ActivateCourse([FromRoute] int courseId)
            => Ok(await client.CourseActivateAsync(User.GetUserId(), courseId));

        /// <summary>
        /// Деактивировать курс, чтобы скрыть его от учеников и не дать им на него подписаться
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        [HttpPatch("course-deactivate/{courseId}")]
        public async Task<ActionResult<int>> DeactivateCourse([FromRoute] int courseId)
            => Ok(await client.CourseDeactivateAsync(User.GetUserId(), courseId));

        /// <summary>
        /// Создать расписание преподавателя
        /// </summary>
        /// <param name="sheduleDto"></param>
        /// <returns></returns>
        [HttpPost("shedule-create")]
        public async Task<ActionResult<int>> CreateShedule([FromBody] TeacherSheduleInputDto sheduleDto = null!)
            => Ok(await client.SheduleCreateAsync(User.GetUserId(), sheduleDto));

        /// <summary>
        /// Выставить оценку занятию
        /// </summary>
        /// <param name="lessonId">Id занятия</param>
        /// <param name="gradingDto">Дто с оценками</param>
        /// <returns></returns>
        [HttpPost("lesson-grade/{lessonId}")]
        public async Task<ActionResult<int>> GradeLesson([FromRoute] int lessonId, [FromBody] GradingInputDto gradingDto = null!)
            => Ok(await client.LessonGradeAsync(User.GetUserId(), lessonId, gradingDto));

        /// <summary>
        /// Удалить расписание. Оно не удалится, если к расписанию преподавателя привязано расписание ученика
        /// </summary>
        /// <param name="sheduleId"></param>
        /// <returns></returns>
        [HttpDelete("shedule-delete/{sheduleId}")]
        public async Task<ActionResult<int>> DeleteShedule([FromRoute] int sheduleId)
            => Ok(await client.SheduleDeleteAsync(User.GetUserId(), sheduleId));
    }
}
