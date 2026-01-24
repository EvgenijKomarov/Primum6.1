using CoreConnection.DTOs.Inputs;
using Microsoft.AspNetCore.Mvc;
using PrimumCore.Services.Iterators;

namespace PrimumCore.Controllers
{
    [ApiController]
    [Route("api/admin/{userId}")]
    public class AdminController(AdminIterator iterator): PrimumController
    {
        [HttpGet("incendents")]
        public async Task<IActionResult> GetIncendents([FromRoute] int userId) => Ok(await iterator.GetIncedents(userId));

        [HttpGet("admins")]
        public async Task<IActionResult> GetAdmins() => Ok(await iterator.GetAdmins());

        [HttpGet("admin")]
        public async Task<IActionResult> GetAdmin([FromRoute] int userId, [FromQuery] int objectUserId) 
            => Ok(await iterator.GetAdmin(objectUserId));

        [HttpGet("incendent-logs")]
        public async Task<IActionResult> GetIncendentLogs([FromRoute] int userId) => Ok(await iterator.GetIncendentLogs(userId));

        [HttpGet("incendent-log/{logId}")]
        public async Task<IActionResult> GetIncendentLog([FromRoute] int userId, [FromQuery] int logId)
            => Ok(await iterator.GetIncendentLog(userId, logId));

        [HttpGet("revise-incendent-log/{logId}")]
        public async Task<IActionResult> RevisionIncendentLog([FromRoute] int userId, [FromQuery] int logId)
            => Ok(await iterator.RevisionIncendentLog(userId, logId));

        [HttpPut("solve")]
        public async Task<IActionResult> SolveIncedent([FromRoute] int userId, [FromBody] IncendentDecisionInputDto dto) 
            => Ok(await iterator.SolveIncedent(userId, dto));

        [HttpPatch("add-cash")]
        public async Task<IActionResult> AddCash([FromRoute] int userId, [FromQuery] int cash, [FromQuery] int objectUserId) 
            => Ok(await iterator.AddCash(userId, objectUserId, cash));

        [HttpPatch("edit-permissions")]
        public async Task<IActionResult> EditPermissions([FromRoute] int userId, [FromQuery] int objectUserId, [FromBody] Dictionary<string, bool> permissions)
            => Ok(await iterator.EditPermissions(userId, objectUserId, permissions));

        [HttpPut("create-admin-profile")]
        public async Task<IActionResult> CreateAdminProfile([FromRoute] int userId, [FromQuery] int objectUserId, [FromQuery] string status)
            => Ok(await iterator.CreateAdminProfile(userId, objectUserId, status));

        [HttpPut("delete-admin-profile")]
        public async Task<IActionResult> DeleteAdminProfile([FromRoute] int userId, [FromQuery] int objectUserId)
            => Ok(await iterator.DeleteAdminProfile(userId, objectUserId));
    }
}
