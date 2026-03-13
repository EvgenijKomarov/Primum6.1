using CoreConnection;
using CoreConnection.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrimumWebAPI.Extensions;

namespace PrimumWebAPI.Controllers
{
    [Route("student/shedules")]
    [Authorize]
    public class StudentSheduleController(StudentClient client) : DefaultController
    {

        /// <summary>
        /// Все расписания ученика, на которые генерируются занятия
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<StudentSheduleDtoPageResult>> GetShedules(
            [FromQuery] int page = 0,
            [FromQuery] int pageSize = 10)
            => Ok(await client.ShedulesAsync(User.GetUserId(), page, pageSize));

        /// <summary>
        /// Конкретное расписание ученика
        /// </summary>
        /// <param name="sheduleId"></param>
        /// <returns></returns>
        [HttpGet("{sheduleId}")]
        public async Task<ActionResult<StudentSheduleDto>> GetShedule([FromRoute] int sheduleId)
            => Ok(await client.SheduleGetAsync(User.GetUserId(), sheduleId));

        /// <summary>
        /// Удалить расписание 
        /// </summary>
        /// <param name="abonementSheduleId"></param>
        /// <returns></returns>
        [HttpDelete("{sheduleId}")]
        public async Task<ActionResult<int>> DeleteShedule([FromRoute] int abonementSheduleId)
            => Ok(await client.SheduleDeleteAsync(User.GetUserId(), abonementSheduleId));
    }
}
