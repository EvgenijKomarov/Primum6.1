using CoreConnection;
using CoreConnection.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrimumWebAPI.Entities.Enums;
using PrimumWebAPI.Extensions;

namespace PrimumWebAPI.Controllers
{
    [Route("student/abonements")]
    [Authorize]
    public class StudentAbonementController(StudentClient client) : DefaultController
    {
        /// <summary>
        /// Все абонементы ученика
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AbonementDto>>> GetAbonements()
            => Ok(await client.AbonementsAsync(User.GetUserId()));

        /// <summary>
        /// Конкретный абонемент ученика
        /// </summary>
        /// <param name="abonementId"></param>
        /// <returns></returns>
        [HttpGet("{abonementId}")]
        public async Task<ActionResult<AbonementDto>> GetAbonement([FromRoute] int abonementId)
            => Ok(await client.AbonementAsync(User.GetUserId(), abonementId));

        /// <summary>
        /// Расписания ученика, привязанного к конкретному абонементу
        /// </summary>
        /// <param name="abonementId"></param>
        /// <returns></returns>
        [HttpGet("{abonementId}/shedules")]
        public async Task<ActionResult<IEnumerable<StudentSheduleDto>>> GetAbonementShedules([FromRoute] int abonementId)
            => Ok(await client.AbonementShedulesAsync(User.GetUserId(), abonementId));

        /// <summary>
        /// Все занятия по абонементу, включая прошедшие и будущие
        /// </summary>
        /// <param name="abonementId"></param>
        /// <returns></returns>
        [HttpGet("{abonementId}/lessons")]
        public async Task<ActionResult<IEnumerable<LessonDto>>> GetAbonementLessons([FromRoute] int abonementId)
            => Ok(await client.AbonementLessonsAsync(User.GetUserId(), abonementId));

        /// <summary>
        /// Активировать абонемент или заморозить, чтобы занятия по нему генерировались, но не происходили
        /// </summary>
        /// <param name="abonementId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        [HttpPatch("{abonementId}/status")]
        public async Task<ActionResult<int>> ChangeStatusAbonement([FromRoute] int abonementId, [FromBody] AbonementInputStatus status)
        {
            if (status == AbonementInputStatus.Activate)
            {
                return Ok(await client.AbonementActivateAsync(User.GetUserId(), abonementId));
            }
            else if (status == AbonementInputStatus.Freeze)
            {
                return Ok(await client.AbonementFreezeAsync(User.GetUserId(), abonementId));
            }
            return BadRequest();
        }

        /// <summary>
        /// Удалить абонемент
        /// </summary>
        /// <param name="abonementId"></param>
        /// <returns></returns>
        [HttpDelete("{abonementId}")]
        public async Task<ActionResult<int>> DeleteAbonement([FromRoute] int abonementId)
            => Ok(await client.AbonementDeleteAsync(User.GetUserId(), abonementId));
    }
}
