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
        public async Task<ActionResult<IEnumerable<AbonementDto>>> GetAbonements()
            => Ok(await client.AbonementsAsync(User.GetUserId()));

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
        public async Task<ActionResult<IEnumerable<StudentSheduleDto>>> GetAbonementShedules([FromRoute] int abonementId)
            => Ok(await client.AbonementShedulesAsync(User.GetUserId(), abonementId));

        /// <summary>
        /// Все занятия абонемента ученика, подписанного на курс преподавателя, и прошедшие и будущие
        /// </summary>
        /// <param name="abonementId"></param>
        /// <returns></returns>
        [HttpGet("{abonementId}/lessons")]
        public async Task<ActionResult<IEnumerable<LessonDto>>> GetAbonementLessons([FromRoute] int abonementId)
            => Ok(await client.AbonementLessonsAsync(User.GetUserId(), abonementId));
    }
}
