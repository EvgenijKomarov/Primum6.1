using CoreConnection.DTOs;
using Microsoft.AspNetCore.Mvc;
using PrimumCore.Services;

namespace PrimumCore.Controllers
{
    [ApiController]
    [Route("api/admin/{userId}")]
    public class AdminController(AdminIterator iterator): PrimumController
    {
        [HttpGet("incendents")]
        public async Task<IActionResult> GetIncendents([FromRoute] int userId) => Ok(await iterator.GetIncedents(userId));

        [HttpPut("solve")]
        public async Task<IActionResult> SolveIncedent([FromRoute] int userId, [FromBody] IncendentDecisionInputDto dto) 
            => Ok(await iterator.SolveIncedent(userId, dto));

        [HttpPatch("add-cash")]
        public async Task<IActionResult> AddCash([FromRoute] int userId, [FromQuery] int cash, [FromQuery] int objectUserId) 
            => Ok(await iterator.AddCash(userId, objectUserId, cash));

        [HttpPatch("give-permission")]
        public async Task<IActionResult> GivePermission([FromRoute] int userId, [FromQuery] int objectUserId, [FromQuery] string permissionIndex)
            => Ok(await iterator.GivePermission(userId, objectUserId, permissionIndex));

        [HttpPatch("take-back-permission")]
        public async Task<IActionResult> TakeBackPermission([FromRoute] int userId, [FromQuery] int objectUserId, [FromQuery] string permissionIndex)
            => Ok(await iterator.TakeBackPermission(userId, objectUserId, permissionIndex));

        [HttpPut("create-admin-profile")]
        public async Task<IActionResult> CreateAdminProfile([FromRoute] int userId, [FromQuery] int objectUserId, [FromQuery] string status)
            => Ok(await iterator.CreateAdminProfile(userId, objectUserId, status));

        [HttpPut("delete-admin-profile")]
        public async Task<IActionResult> DeleteAdminProfile([FromRoute] int userId, [FromQuery] int objectUserId)
            => Ok(await iterator.DeleteAdminProfile(userId, objectUserId));
    }
}
