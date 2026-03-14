using CoreConnection;
using CoreConnection.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrimumWebAPI.Extensions;

namespace PrimumWebAPI.Controllers
{
    [Route("teacher/abonements")]
    [Authorize]
    public class TeacherAbonementController(TeacherClient client) : DefaultController
    {
        /// <summary>
        /// Все абонементы учеников, подписанные на курсы преподавателя
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<AbonementDtoPageResult>> GetAbonements(
            [FromQuery] int page = 0,
            [FromQuery] int pageSize = 10)
            => Ok(await client.AbonementsAsync(User.GetUserId(), page, pageSize));

        /// <summary>
        /// Конкретный абонемент ученика, подписанного на курс преподавателя
        /// </summary>
        /// <param name="abonementId"></param>
        /// <returns></returns>
        [HttpGet("{abonementId}")]
        public async Task<ActionResult<AbonementDto>> GetAbonement([FromRoute] int abonementId)
            => Ok(await client.AbonementAsync(User.GetUserId(), abonementId));

        /// <summary>
        /// Расписания абонемента ученика, подписанного на курс преподавателя
        /// </summary>
        /// <param name="abonementId"></param>
        /// <returns></returns>
        [HttpGet("{abonementId}/shedules")]
        public async Task<ActionResult<AbonementSheduleDtoPageResult>> GetAbonementShedules(
            [FromRoute] int abonementId,
            [FromQuery] int page = 0,
            [FromQuery] int pageSize = 10)
            => Ok(await client.AbonementShedulesAsync(User.GetUserId(), abonementId, page, pageSize));

        /// <summary>
        /// Все занятия абонемента ученика, подписанного на курс преподавателя, и прошедшие и будущие
        /// </summary>
        /// <param name="abonementId"></param>
        /// <returns></returns>
        [HttpGet("{abonementId}/lessons")]
        public async Task<ActionResult<LessonDtoPageResult>> GetAbonementLessons(
            [FromRoute] int abonementId,
            [FromQuery] int page = 0,
            [FromQuery] int pageSize = 10)
            => Ok(await client.AbonementLessonsAsync(User.GetUserId(), abonementId, page, pageSize));
    }
}
