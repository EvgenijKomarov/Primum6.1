using CoreConnection;
using CoreConnection.DTOs;
using CoreConnection.DTOs.Inputs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrimumWebAPI.Extensions;

namespace PrimumWebAPI.Controllers
{
    [Route("teacher/shedules")]
    [Authorize]
    public class TeacherSheduleController(TeacherClient client) : DefaultController
    {
        /// <summary>
        /// Все расписания преподавателя
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeacherSheduleDto>>> GetShedules()
            => Ok(await client.ShedulesAsync(User.GetUserId()));

        /// <summary>
        /// Конкретное расписание
        /// </summary>
        /// <param name="sheduleId"></param>
        /// <returns></returns>
        [HttpGet("{sheduleId}")]
        public async Task<ActionResult<TeacherSheduleDto>> GetShedule([FromRoute] int sheduleId)
            => Ok(await client.SheduleAsync(User.GetUserId(), sheduleId));

        /// <summary>
        /// Создать расписание преподавателя
        /// </summary>
        /// <param name="sheduleDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<int>> CreateShedule([FromBody] TeacherSheduleInputDto sheduleDto = null!)
            => Ok(await client.SheduleCreateAsync(User.GetUserId(), sheduleDto));

        /// <summary>
        /// Удалить расписание. Оно не удалится, если к расписанию преподавателя привязано расписание ученика
        /// </summary>
        /// <param name="sheduleId"></param>
        /// <returns></returns>
        [HttpDelete("{sheduleId}")]
        public async Task<ActionResult<int>> DeleteShedule([FromRoute] int sheduleId)
            => Ok(await client.SheduleDeleteAsync(User.GetUserId(), sheduleId));
    }
}
