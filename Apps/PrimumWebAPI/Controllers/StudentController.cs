using CoreConnection;
using CoreConnection.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrimumWebAPI.Extensions;
using System.Runtime.ConstrainedExecution;

namespace PrimumWebAPI.Controllers
{
    [ApiController]
    [Route("api/student")]
    [Tags("Student")]
    [Authorize]
    public class StudentController(StudentClient client): DefaultController
    {
        /// <summary>
        /// Профиль ученика, включая имя, количество монет и id пользователя
        /// </summary>
        /// <returns></returns>
        [HttpGet("profile")]
        public async Task<ActionResult<StudentProfileDto>> GetStudentProfile()
            => Ok(await client.ProfileAsync(User.GetUserId()));

        /// <summary>
        /// Все занятия ученика, включая прошедшие и будущие
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
        /// Все абонементы ученика
        /// </summary>
        /// <returns></returns>
        [HttpGet("abonements")]
        public async Task<ActionResult<IEnumerable<AbonementDto>>> GetAbonements()
            => Ok(await client.AbonementsAsync(User.GetUserId()));

        /// <summary>
        /// Конкретный абонемент ученика
        /// </summary>
        /// <param name="abonementId"></param>
        /// <returns></returns>
        [HttpGet("abonement/{abonementId}")]
        public async Task<ActionResult<AbonementDto>> GetAbonement([FromRoute] int abonementId)
            => Ok(await client.AbonementAsync(User.GetUserId(), abonementId));

        /// <summary>
        /// Все расписания ученика, на которые генерируются занятия
        /// </summary>
        /// <returns></returns>
        [HttpGet("shedules")]
        public async Task<ActionResult<IEnumerable<StudentSheduleDto>>> GetShedules()
            => Ok(await client.ShedulesAsync(User.GetUserId()));

        /// <summary>
        /// Конкретное расписание ученика
        /// </summary>
        /// <param name="sheduleId"></param>
        /// <returns></returns>
        [HttpGet("shedule/{sheduleId}")]
        public async Task<ActionResult<StudentSheduleDto>> GetShedule([FromRoute] int sheduleId)
            => Ok(await client.SheduleGetAsync(User.GetUserId(), sheduleId));

        /// <summary>
        /// Расписания ученика, привязанного к конкретному абонементу
        /// </summary>
        /// <param name="abonementId"></param>
        /// <returns></returns>
        [HttpGet("abonement-shedules/{abonementId}")]
        public async Task<ActionResult<IEnumerable<StudentSheduleDto>>> GetAbonementShedules([FromRoute] int abonementId)
            => Ok(await client.AbonementShedulesAsync(User.GetUserId(), abonementId));

        /// <summary>
        /// Все занятия по абонементу, включая прошедшие и будущие
        /// </summary>
        /// <param name="abonementId"></param>
        /// <returns></returns>
        [HttpGet("abonement-lessons/{abonementId}")]
        public async Task<ActionResult<IEnumerable<LessonDto>>> GetAbonementLessons([FromRoute] int abonementId)
            => Ok(await client.AbonementLessonsAsync(User.GetUserId(), abonementId));

        /// <summary>
        /// Все купленные учеником промокоды
        /// </summary>
        /// <returns></returns>
        [HttpGet("promocodes")]
        public async Task<ActionResult<IEnumerable<PromocodeDto>>> GetStudentPromocodes()
            => Ok(await client.PromocodesAsync(User.GetUserId()));

        /// <summary>
        /// Посмотреть доступные ученику к покупке промокоды
        /// </summary>
        /// <returns></returns>
        [HttpGet("available-promocodes")]
        public async Task<ActionResult<IEnumerable<PromocodeDto>>> GetPromocodes()
            => Ok(await client.AvailablePromocodesAsync(User.GetUserId()));

        /// <summary>
        /// Посмотреть конкретный купленный промокод
        /// </summary>
        /// <returns></returns>
        [HttpGet("promocode/{promocodeId}")]
        public async Task<ActionResult<PromocodeDto>> GetPromocode([FromRoute] int promocodeId)
            => Ok(await client.PromocodeAsync(promocodeId, User.GetUserId()));

        /// <summary>
        /// Активировать абонемент
        /// </summary>
        /// <param name="abonementId"></param>
        /// <returns></returns>
        [HttpPatch("abonement-activate")]
        public async Task<ActionResult<int>> ActivateAbonement([FromQuery] int abonementId)
            => Ok(await client.AbonementActivateAsync(User.GetUserId(), abonementId));

        /// <summary>
        /// Заморозить абонемент, чтобы занятия по нему генерировались, но не происходили
        /// </summary>
        /// <param name="abonementId"></param>
        /// <returns></returns>
        [HttpPatch("abonement-freeze")]
        public async Task<ActionResult<int>> FreezeAbonement([FromQuery] int abonementId)
            => Ok(await client.AbonementFreezeAsync(User.GetUserId(), abonementId));

        /// <summary>
        /// Подписаться на курс
        /// </summary>
        /// <param name="courseId">Id курса</param>
        /// <param name="teacherSheduleId">Id расписания преподавателя</param>
        /// <returns></returns>
        [HttpPost("course-subscribe")]
        public async Task<ActionResult<int>> SubscribeToCourse([FromQuery] int courseId, [FromQuery] int teacherSheduleId)
            => Ok(await client.CourseSubscribeAsync(User.GetUserId(), courseId, teacherSheduleId));

        /// <summary>
        /// Купить промокод
        /// </summary>
        /// <param name="promocodeId"></param>
        /// <returns></returns>
        [HttpPost("buy-promocode")]
        public async Task<ActionResult<PromocodeDto>> BuyPromocode([FromQuery] int promocodeId)
            => Ok(await client.BuyPromocodeAsync(User.GetUserId(), promocodeId));

        /// <summary>
        /// Удалить расписание 
        /// </summary>
        /// <param name="abonementSheduleId"></param>
        /// <returns></returns>
        [HttpDelete("shedule-delete")]
        public async Task<ActionResult<int>> DeleteShedule([FromQuery] int abonementSheduleId)
            => Ok(await client.SheduleDeleteAsync(User.GetUserId(), abonementSheduleId));

        /// <summary>
        /// Удалить абонемент
        /// </summary>
        /// <param name="abonementId"></param>
        /// <returns></returns>
        [HttpDelete("abonement-delete")]
        public async Task<ActionResult<int>> DeleteAbonement([FromQuery] int abonementId)
            => Ok(await client.AbonementDeleteAsync(User.GetUserId(), abonementId));
    }
}
