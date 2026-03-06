using CoreConnection;
using CoreConnection.DTOs;
using CoreConnection.DTOs.Inputs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrimumWebAPI.Extensions;

namespace PrimumWebAPI.Controllers
{
    [Route("teacher")]
    [Authorize]
    public class TeacherController(TeacherClient client): DefaultController
    {
        /// <summary>
        /// Профиль преподавателя
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<TeacherProfileDto>> GetTeacherProfile()
            => Ok(await client.ProfileAsync(User.GetUserId()));
    }
}
