using CoreConnection;
using CoreConnection.DTOs;
using CoreConnection.DTOs.Inputs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrimumWebAPI.Extensions;

namespace PrimumWebAPI.Controllers
{
    [ApiController]
    [Route("api/admin")]
    [Tags("Admin")]
    [Authorize]
    public class AdminController(AdminClient client): DefaultController
    {
        [HttpGet("profile")]
        public async Task<ActionResult<AdminProfileDto>> GetProfile()
            => Ok(await client.ProfileAsync(User.GetUserId()));

        [HttpGet("get-user/{objectUserId}")]
        public async Task<ActionResult<UserDto>> GetUser([FromRoute] int objectUserId)
            => Ok(await client.GetUserAsync(User.GetUserId(), objectUserId));

        [HttpGet("get-users")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers() => Ok(await client.GetUsersAsync(User.GetUserId()));

        [HttpGet("incidents")]
        public async Task<ActionResult<IEnumerable<IncidentDto>>> GetIncidents() => Ok(await client.IncidentsAsync(User.GetUserId()));

        [HttpGet("other-admins")]
        public async Task<ActionResult<IEnumerable<AdminProfileDto>>> GetAdmins() => Ok(await client.AdminsAsync(User.GetUserId()));

        [HttpGet("other-admin/{objectUserId}")]
        public async Task<ActionResult<AdminProfileDto>> GetAdmin([FromRoute] int objectUserId)
            => Ok(await client.AdminAsync(User.GetUserId(), objectUserId));

        [HttpGet("incident-logs")]
        public async Task<ActionResult<IEnumerable<IncidentLogDto>>> GetIncidentLogs([FromQuery] bool OnlyUnrevisioned = true)
            => Ok(await client.IncidentLogsAsync(User.GetUserId(), OnlyUnrevisioned));

        [HttpGet("incident-log/{logId}")]
        public async Task<ActionResult<IncidentLogDto>> GetIncidentLog([FromRoute] int logId)
            => Ok(await client.IncidentLogAsync(User.GetUserId(), logId));

        [HttpGet("revise-incident-log/{logId}")]
        public async Task<ActionResult<int>> RevisionIncidentLog([FromRoute] int logId)
            => Ok(await client.ReviseIncidentLogAsync(User.GetUserId(), logId));

        [HttpGet("all-promocodes")]
        public async Task<ActionResult<IEnumerable<PromocodeDto>>> GetPromocodes()
            => Ok(await client.AllPromocodesAsync(User.GetUserId()));

        [HttpGet("promocode/{promocodeId}")]
        public async Task<ActionResult<PromocodeDto>> GetPromocode([FromRoute] int promocodeId)
            => Ok(await client.PromocodeAsync(User.GetUserId(), promocodeId));

        [HttpPut("solve-incident")]
        public async Task<ActionResult<int>> SolveIncedent([FromBody] IncidentDecisionInputDto dto = null!)
            => Ok(await client.SolveIncidentAsync(User.GetUserId(), dto));

        [HttpPatch("add-cash/{objectUserId}")]
        public async Task<ActionResult<int>> AddCash([FromRoute] int objectUserId, [FromQuery] int cash = 0)
            => Ok(await client.AddCashAsync(User.GetUserId(), objectUserId, cash));

        [HttpPatch("ban/{objectUserId}")]
        public async Task<ActionResult<int>> BanUser([FromRoute] int objectUserId)
            => Ok(await client.BanAsync(User.GetUserId(), objectUserId));

        [HttpPatch("unban/{objectUserId}")]
        public async Task<ActionResult<int>> UnbanUser([FromRoute] int objectUserId)
            => Ok(await client.UnbanAsync(User.GetUserId(), objectUserId));

        [HttpPatch("edit-permissions/{objectUserId}")]
        public async Task<ActionResult<int>> EditPermissions([FromRoute] int objectUserId, [FromBody] Dictionary<string, bool> permissions = null!)
            => Ok(await client.EditPermissionsAsync(User.GetUserId(), objectUserId, permissions));

        [HttpPatch("edit-course-theme/{themeId}")]
        public async Task<ActionResult<int>> EditTheme([FromRoute] int themeId, [FromBody] CourseThemeInputDto dto = null!)
            => Ok(await client.EditCourseThemeAsync(User.GetUserId(), themeId, dto));

        [HttpPut("create-admin-profile/{objectUserId}")]
        public async Task<ActionResult<int>> CreateAdminProfile([FromRoute] int objectUserId, [FromQuery] string status)
            => Ok(await client.CreateAdminProfileAsync(User.GetUserId(), objectUserId, status));

        [HttpPut("add-promocode")]
        public async Task<ActionResult<int>> AddPromocode([FromBody] PromocodeInputDto dto = null!)
            => Ok(await client.AddPromocodeAsync(User.GetUserId(), dto));

        [HttpPut("delete-admin-profile/{objectUserId}")]
        public async Task<ActionResult<int>> DeleteAdminProfile([FromRoute] int objectUserId)
            => Ok(await client.DeleteAdminProfileAsync(User.GetUserId(), objectUserId));

        [HttpPut("create-course-theme")]
        public async Task<ActionResult<int>> CreateTheme([FromBody] CourseThemeInputDto dto = null!)
            => Ok(await client.CreateCourseThemeAsync(User.GetUserId(), dto));

        [HttpDelete("delete-promocode/{promocodeId}")]
        public async Task<ActionResult<int>> DeletePromocode([FromRoute] int promocodeId)
            => Ok(await client.DeletePromocodeAsync(User.GetUserId(), promocodeId));
    }
}
