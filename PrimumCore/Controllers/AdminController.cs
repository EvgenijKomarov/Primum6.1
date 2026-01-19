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
        public async Task<IActionResult> GetIncendents(int userId) => Ok(await iterator.GetIncedents(userId));

        [HttpPut("solve")]
        public async Task<IActionResult> SolveIncedent(int userId, [FromBody] IncendentDecisionInputDto dto) 
            => Ok(await iterator.SolveIncedent(userId, dto));

        [HttpPatch("add-cash")]
        public async Task<IActionResult> AddCash(int userId, [FromBody] int objUserId, [FromBody] int cash) 
            => Ok(await iterator.AddCash(userId, objUserId, cash));

        [HttpPatch("give-permission")]
        public async Task<IActionResult> GivePermission(int userId, [FromBody] int objUserId, [FromBody] string permissionIndex)
            => Ok(await iterator.GivePermission(userId, objUserId, permissionIndex));

        [HttpPatch("take-back-permission")]
        public async Task<IActionResult> TakeBackPermission(int userId, [FromBody] int objUserId, [FromBody] string permissionIndex)
            => Ok(await iterator.TakeBackPermission(userId, objUserId, permissionIndex));
    }
}
