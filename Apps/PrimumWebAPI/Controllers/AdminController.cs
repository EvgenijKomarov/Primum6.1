using CoreConnection;
using CoreConnection.DTOs;
using CoreConnection.DTOs.Inputs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrimumWebAPI.Extensions;

namespace PrimumWebAPI.Controllers
{
    [Route("admin")]
    [Authorize]
    public class AdminController(AdminClient client): DefaultController
    {
        /// <summary>
        /// Профиль админа
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<AdminProfileDto>> GetProfile()
            => Ok(await client.ProfileAsync(User.GetUserId()));
    }
}
