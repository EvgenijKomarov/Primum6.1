using CoreConnection.DTOs;
using CoreConnection.DTOs.Inputs;
using Microsoft.AspNetCore.Mvc;
using PrimumCore.Services.Iterators;
using Swashbuckle.AspNetCore.Annotations;

namespace PrimumCore.Controllers
{
    [ApiController]
    [Route("api/admin/{userId}")]
    [Tags("Admin")]
    public class AdminController(
        AdminIterator iterator, 
        IncidentIterator IncidentIterator,
        PromocodeIterator promocodeIterator,
        UserIterator userIterator
        ) : PrimumController
    {
        [HttpGet("get-user/{objectUserId}")]
        public async Task<ActionResult<UserDto>> GetUser([FromRoute] int userId, [FromRoute] int objectUserId) 
            => Ok(await userIterator.GetUser(objectUserId, false));

        [HttpGet("get-users")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers([FromRoute] int userId) => Ok(await userIterator.GetUsers(false));

        [HttpGet("incidents")]
        public async Task<ActionResult<IEnumerable<IncidentDto>>> GetIncidents([FromRoute] int userId) => Ok(await IncidentIterator.GetIncedents(userId));

        [HttpGet("admins")]
        public async Task<ActionResult<IEnumerable<AdminProfileDto>>> GetAdmins([FromRoute] int userId) => Ok(await iterator.GetAdmins());

        [HttpGet("admin/{objectUserId}")]
        public async Task<ActionResult<AdminProfileDto>> GetAdmin([FromRoute] int userId, [FromRoute] int objectUserId) 
            => Ok(await iterator.GetAdmin(objectUserId));

        [HttpGet("incident-logs")]
        public async Task<ActionResult<IEnumerable<IncidentLogDto>>> GetIncidentLogs([FromRoute] int userId, [FromQuery] bool OnlyUnrevisioned = true) 
            => Ok(await IncidentIterator.GetIncidentLogs(userId, OnlyUnrevisioned));

        [HttpGet("incident-log/{logId}")]
        public async Task<ActionResult<IncidentLogDto>> GetIncidentLog([FromRoute] int userId, [FromRoute] int logId)
            => Ok(await IncidentIterator.GetIncidentLog(userId, logId));

        [HttpGet("revise-incident-log/{logId}")]
        public async Task<ActionResult<int>> RevisionIncidentLog([FromRoute] int userId, [FromRoute] int logId)
            => Ok(await IncidentIterator.RevisionIncidentLog(userId, logId));

        [HttpGet("all-promocodes")]
        public async Task<ActionResult<IEnumerable<PromocodeDto>>> GetPromocodes([FromRoute] int userId)
            => Ok(await promocodeIterator.GetPromocodes(false));

        [HttpGet("promocode/{promocodeId}")]
        public async Task<ActionResult<PromocodeDto>> GetPromocode([FromRoute] int userId, [FromRoute] int promocodeId)
            => Ok(await promocodeIterator.GetPromocode(promocodeId, false));

        [HttpPut("solve")]
        public async Task<ActionResult<int>> SolveIncedent([FromRoute] int userId, [FromBody] IncidentDecisionInputDto dto = null!) 
            => Ok(await IncidentIterator.SolveIncedent(userId, dto));

        [HttpPatch("add-cash/{objectUserId}")]
        public async Task<ActionResult<int>> AddCash([FromRoute] int userId, [FromRoute] int objectUserId, [FromQuery] int cash = 0) 
            => Ok(await iterator.AddCash(userId, objectUserId, cash));

        [HttpPatch("ban/{objectUserId}")]
        public async Task<ActionResult<int>> BanUser([FromRoute] int userId, [FromRoute] int objectUserId)
            => Ok(await iterator.BanUser(userId, objectUserId, true));

        [HttpPatch("unban/{objectUserId}")]
        public async Task<ActionResult<int>> UnbanUser([FromRoute] int userId, [FromRoute] int objectUserId)
            => Ok(await iterator.BanUser(userId, objectUserId, false));

        [HttpPatch("edit-permissions/{objectUserId}")]
        public async Task<ActionResult<int>> EditPermissions([FromRoute] int userId, [FromRoute] int objectUserId, [FromBody] Dictionary<string, bool> permissions = null!)
            => Ok(await iterator.EditPermissions(userId, objectUserId, permissions));

        [HttpPut("create-admin-profile/{objectUserId}")]
        public async Task<ActionResult<int>> CreateAdminProfile([FromRoute] int userId, [FromRoute] int objectUserId, [FromQuery] string status)
            => Ok(await iterator.CreateAdminProfile(userId, objectUserId, status));

        [HttpPut("add-promocode")]
        public async Task<ActionResult<int>> AddPromocode([FromRoute] int userId, [FromBody] PromocodeInputDto dto = null!)
            => Ok(await promocodeIterator.AddPromocode(userId, dto));

        [HttpPut("delete-admin-profile/{objectUserId}")]
        public async Task<ActionResult<int>> DeleteAdminProfile([FromRoute] int userId, [FromRoute] int objectUserId)
            => Ok(await iterator.DeleteAdminProfile(userId, objectUserId));

        [HttpDelete("delete-promocode/{promocodeId}")]
        public async Task<ActionResult<int>> DeletePromocode([FromRoute] int userId, [FromRoute] int promocodeId)
            => Ok(await promocodeIterator.DeletePromocode(userId, promocodeId));
    }
}
