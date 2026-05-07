using CoreConnection.DTOs;
using CoreConnection.DTOs.Inputs;
using PrimumCore.Entities;
using Microsoft.AspNetCore.Mvc;
using PrimumCore.Services.Iterators;

namespace PrimumCore.Controllers
{
    [ApiController]
    [Route("api/admin/{userId}")]
    [Tags("Admin")]
    public class AdminController(
        AdminIterator iterator, 
        IncidentIterator IncidentIterator,
        PromocodeIterator promocodeIterator,
        UserIterator userIterator,
        ThemeIterator themeIterator
        ) : PrimumController
    {
        [HttpGet("profile")]
        public async Task<ActionResult<AdminProfileDto>> GetProfile([FromRoute] int userId)
            => Ok(await iterator.GetAdmin(userId));

        [HttpGet("get-user/{objectUserId}")]
        public async Task<ActionResult<UserDto>> GetUser([FromRoute] int userId, [FromRoute] int objectUserId) 
            => Ok(await userIterator.GetUser(objectUserId, false));

        [HttpGet("get-users")]
        public async Task<ActionResult<PageResult<UserDto>>> GetUsers([FromRoute] int userId, [FromQuery] int page = 0, [FromQuery] int pageSize = 10) 
            => Ok(await userIterator.GetUsers(false, page, pageSize));

        [HttpGet("incidents")]
        public async Task<ActionResult<PageResult<IncidentDto>>> GetIncidents([FromRoute] int userId, [FromQuery] int page = 0, [FromQuery] int pageSize = 10) 
            => Ok(await IncidentIterator.GetIncedents(userId, page, pageSize));

        [HttpGet("admins")]
        public async Task<ActionResult<PageResult<AdminProfileDto>>> GetAdmins([FromRoute] int userId, [FromQuery] int page = 0, [FromQuery] int pageSize = 10) 
            => Ok(await iterator.GetAdmins(page, pageSize));

        [HttpGet("admin/{objectUserId}")]
        public async Task<ActionResult<AdminProfileDto>> GetAdmin([FromRoute] int userId, [FromRoute] int objectUserId) 
            => Ok(await iterator.GetAdmin(objectUserId));

        [HttpGet("incident-logs")]
        public async Task<ActionResult<PageResult<IncidentLogDto>>> GetIncidentLogs(
            [FromRoute] int userId, 
            [FromQuery] bool OnlyUnrevisioned = true, 
            [FromQuery] int page = 0, 
            [FromQuery] int pageSize = 10) 
            => Ok(await IncidentIterator.GetIncidentLogs(userId, OnlyUnrevisioned, page, pageSize));

        [HttpGet("incident-log/{logId}")]
        public async Task<ActionResult<IncidentLogDto>> GetIncidentLog([FromRoute] int userId, [FromRoute] int logId)
            => Ok(await IncidentIterator.GetIncidentLog(userId, logId));

        [HttpGet("revise-incident-log/{logId}")]
        public async Task<ActionResult<int>> RevisionIncidentLog([FromRoute] int userId, [FromRoute] int logId)
            => Ok(await IncidentIterator.RevisionIncidentLog(userId, logId));

        [HttpGet("all-promocodes")]
        public async Task<ActionResult<PageResult<PromocodeDto>>> GetPromocodes([FromRoute] int userId, [FromQuery] int page = 0, [FromQuery] int pageSize = 10)
            => Ok(await promocodeIterator.GetPromocodes(false, page, pageSize));

        [HttpGet("promocode/{promocodeId}")]
        public async Task<ActionResult<PromocodeDto>> GetPromocode([FromRoute] int userId, [FromRoute] int promocodeId)
            => Ok(await promocodeIterator.GetPromocode(promocodeId, false));

        [HttpPatch("ban/{objectUserId}")]
        public async Task<ActionResult<int>> BanUser([FromRoute] int userId, [FromRoute] int objectUserId)
            => Ok(await iterator.ChangeBanStatus(userId, objectUserId, true));

        [HttpPatch("unban/{objectUserId}")]
        public async Task<ActionResult<int>> UnbanUser([FromRoute] int userId, [FromRoute] int objectUserId)
            => Ok(await iterator.ChangeBanStatus(userId, objectUserId, false));

        [HttpPatch("edit-permissions/{objectUserId}")]
        public async Task<ActionResult<int>> EditPermissions([FromRoute] int userId, [FromRoute] int objectUserId, [FromBody] Dictionary<string, bool> permissions = null!)
            => Ok(await iterator.EditPermissions(userId, objectUserId, permissions));

        [HttpPatch("edit-course-theme/{themeId}")]
        public async Task<ActionResult<int>> EditTheme([FromRoute] int userId, [FromRoute] int themeId, [FromBody] CourseThemeInputDto dto = null!)
            => Ok(await themeIterator.EditTheme(userId, themeId, dto));

        [HttpPut("create-admin-profile/{objectUserId}")]
        public async Task<ActionResult<int>> CreateAdminProfile([FromRoute] int userId, [FromRoute] int objectUserId, [FromQuery] string status)
            => Ok(await iterator.CreateAdminProfile(userId, objectUserId, status));

        [HttpPut("add-promocode")]
        public async Task<ActionResult<int>> AddPromocode([FromRoute] int userId, [FromBody] PromocodeInputDto dto = null!)
            => Ok(await promocodeIterator.AddPromocode(userId, dto));

        [HttpPut("solve-incident")]
        public async Task<ActionResult<int>> SolveIncedent([FromRoute] int userId, [FromBody] IncidentDecisionInputDto dto = null!)
            => Ok(await IncidentIterator.SolveIncedent(userId, dto));

        [HttpPut("create-course-theme")]
        public async Task<ActionResult<int>> CreateTheme([FromRoute] int userId, [FromBody] CourseThemeInputDto dto = null!)
            => Ok(await themeIterator.CreateTheme(userId, dto));

        [HttpDelete("delete-admin-profile/{objectUserId}")]
        public async Task<ActionResult<int>> DeleteAdminProfile([FromRoute] int userId, [FromRoute] int objectUserId)
            => Ok(await iterator.DeleteAdminProfile(userId, objectUserId));

        [HttpDelete("delete-promocode/{promocodeId}")]
        public async Task<ActionResult<int>> DeletePromocode([FromRoute] int userId, [FromRoute] int promocodeId)
            => Ok(await promocodeIterator.DeletePromocode(userId, promocodeId));
    }
}
