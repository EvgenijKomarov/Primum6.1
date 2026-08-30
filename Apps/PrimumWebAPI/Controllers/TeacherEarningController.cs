using CoreConnection;
using CoreConnection.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrimumWebAPI.Extensions;

namespace PrimumWebAPI.Controllers
{
    [Route("teacher/earnings")]
    [Authorize]
    public class TeacherEarningController(TeacherClient client): DefaultController
    {
        /// <summary>
        /// Заработок преподавателя
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<TeacherProfileDto>> GetTeacherEarnings()
            => Ok(await client.EarningsAsync(User.GetUserId()));
    }
}
