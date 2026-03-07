using CoreConnection;
using CoreConnection.DTOs;
using CoreConnection.DTOs.Inputs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrimumWebAPI.Extensions;

namespace PrimumWebAPI.Controllers
{
    [Route("admin/incidents")]
    [Authorize]
    public class AdminIncidentController(AdminClient client) : DefaultController
    {
        /// <summary>
        /// Список всех инцидентов. Доступно всем
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IncidentDto>>> GetIncidents() => Ok(await client.IncidentsAsync(User.GetUserId()));

        /// <summary>
        /// Решение инцидента. Доступно всем
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPatch]
        public async Task<ActionResult<int>> SolveIncedent([FromBody] IncidentDecisionInputDto dto = null!)
            => Ok(await client.SolveIncidentAsync(User.GetUserId(), dto));

        /// <summary>
        /// Посмотреть логи действий всех админов. Только для админов с правом InspectIncidentLogs
        /// </summary>
        /// <param name="OnlyUnrevisioned"></param>
        /// <returns></returns>
        [HttpGet("logs")]
        public async Task<ActionResult<IEnumerable<IncidentLogDto>>> GetIncidentLogs([FromQuery] bool OnlyUnrevisioned = true)
            => Ok(await client.IncidentLogsAsync(User.GetUserId(), OnlyUnrevisioned));

        /// <summary>
        /// Конкретный лог действия админа. Только для админов с правом InspectIncidentLogs
        /// </summary>
        /// <param name="logId"></param>
        /// <returns></returns>
        [HttpGet("logs/{logId}")]
        public async Task<ActionResult<IncidentLogDto>> GetIncidentLog([FromRoute] int logId)
            => Ok(await client.IncidentLogAsync(User.GetUserId(), logId));

        /// <summary>
        /// Отметить лог просмотренным (предполагается, что это делается кнопкой под карточкой лога)
        /// </summary>
        /// <param name="logId"></param>
        /// <returns></returns>
        [HttpPatch("logs/{logId}")]
        public async Task<ActionResult<int>> RevisionIncidentLog([FromRoute] int logId)
            => Ok(await client.ReviseIncidentLogAsync(User.GetUserId(), logId));
    }
}
