using CoreConnection.DTOs.Inputs;
using Microsoft.AspNetCore.Mvc;
using PrimumCore.Services.Iterators;

namespace PrimumCore.Controllers
{
    [ApiController]
    [Route("api/admin/{userId}")]
    public class AdminController(
        AdminIterator iterator, 
        IncendentIterator incendentIterator,
        PromocodeIterator promocodeIterator
        ) : PrimumController
    {
        [HttpGet("incendents")]
        public async Task<IActionResult> GetIncendents([FromRoute] int userId) => Ok(await incendentIterator.GetIncedents(userId));

        [HttpGet("admins")]
        public async Task<IActionResult> GetAdmins() => Ok(await iterator.GetAdmins());

        [HttpGet("admin")]
        public async Task<IActionResult> GetAdmin([FromRoute] int userId, [FromQuery] int objectUserId) 
            => Ok(await iterator.GetAdmin(objectUserId));

        [HttpGet("incendent-logs")]
        public async Task<IActionResult> GetIncendentLogs([FromRoute] int userId, [FromQuery] bool OnlyUnrevisioned) 
            => Ok(await incendentIterator.GetIncendentLogs(userId, OnlyUnrevisioned));

        [HttpGet("incendent-log/{logId}")]
        public async Task<IActionResult> GetIncendentLog([FromRoute] int userId, [FromQuery] int logId)
            => Ok(await incendentIterator.GetIncendentLog(userId, logId));

        [HttpGet("revise-incendent-log/{logId}")]
        public async Task<IActionResult> RevisionIncendentLog([FromRoute] int userId, [FromQuery] int logId)
            => Ok(await incendentIterator.RevisionIncendentLog(userId, logId));

        [HttpGet("available-promocodes")]
        public async Task<IActionResult> GetPromocodes()
            => Ok(await promocodeIterator.GetPromocodes(false));

        [HttpGet("promocode/{promocodeId}")]
        public async Task<IActionResult> GetPromocode([FromRoute] int promocodeId)
            => Ok(await promocodeIterator.GetPromocode(promocodeId, false));

        [HttpPut("solve")]
        public async Task<IActionResult> SolveIncedent([FromRoute] int userId, [FromBody] IncendentDecisionInputDto dto) 
            => Ok(await incendentIterator.SolveIncedent(userId, dto));

        [HttpPatch("add-cash")]
        public async Task<IActionResult> AddCash([FromRoute] int userId, [FromQuery] int cash, [FromQuery] int objectUserId) 
            => Ok(await iterator.AddCash(userId, objectUserId, cash));

        [HttpPatch("edit-permissions")]
        public async Task<IActionResult> EditPermissions([FromRoute] int userId, [FromQuery] int objectUserId, [FromBody] Dictionary<string, bool> permissions)
            => Ok(await iterator.EditPermissions(userId, objectUserId, permissions));

        [HttpPut("create-admin-profile")]
        public async Task<IActionResult> CreateAdminProfile([FromRoute] int userId, [FromQuery] int objectUserId, [FromQuery] string status)
            => Ok(await iterator.CreateAdminProfile(userId, objectUserId, status));

        [HttpPut("add-promocode")]
        public async Task<IActionResult> AddPromocode([FromRoute] int userId, [FromBody] PromocodeInputDto dto)
            => Ok(await promocodeIterator.AddPromocode(userId, dto));

        [HttpPut("delete-admin-profile")]
        public async Task<IActionResult> DeleteAdminProfile([FromRoute] int userId, [FromQuery] int objectUserId)
            => Ok(await iterator.DeleteAdminProfile(userId, objectUserId));

        [HttpDelete("delete-promocode")]
        public async Task<IActionResult> DeletePromocode([FromRoute] int userId, [FromQuery] int promocodeId)
            => Ok(await promocodeIterator.DeletePromocode(userId, promocodeId));
    }
}
