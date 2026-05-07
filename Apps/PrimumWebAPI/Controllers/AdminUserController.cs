using CoreConnection;
using CoreConnection.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrimumWebAPI.Extensions;

namespace PrimumWebAPI.Controllers
{
    [Route("admin/users")]
    [Authorize]
    public class AdminUserController(AdminClient client) : DefaultController
    {
        /// <summary>
        /// Список всех пользователей
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<UserDtoPageResult>> GetUsers(
            [FromQuery] int page = 0,
            [FromQuery] int pageSize = 10) 
            => Ok(await client.GetUsersAsync(User.GetUserId(), page, pageSize));

        /// <summary>
        /// Информация о конкретном пользователе
        /// </summary>
        /// <param name="objectUserId"></param>
        /// <returns></returns>
        [HttpGet("{objectUserId}")]
        public async Task<ActionResult<UserDto>> GetUser([FromRoute] int objectUserId)
            => Ok(await client.GetUserAsync(User.GetUserId(), objectUserId));

        /// <summary>
        /// Забанить/разбанить пользователя. Только для админов с правом ChangeBanStatus
        /// </summary>
        /// <param name="objectUserId"></param>
        /// <param name="banStatus">Статус бана</param>
        /// <returns></returns>
        [HttpPatch("{objectUserId}/ban-status")]
        public async Task<ActionResult<int>> BanUser([FromRoute] int objectUserId, [FromBody] bool banStatus)
        {
            if (banStatus) 
            { 
                return Ok(await client.BanAsync(User.GetUserId(), objectUserId));
            }
            else
            {
                return Ok(await client.UnbanAsync(User.GetUserId(), objectUserId));
            }
        }

        /// <summary>
        /// Создать профиль админа пользователю. Только для админов с правом CreateAdminProfiles
        /// </summary>
        /// <param name="objectUserId"></param>
        /// <param name="status">Статус (просто приписка, ни на что не влияющая)</param>
        /// <returns></returns>
        [HttpPost("{objectUserId}/admin-profile")]
        public async Task<ActionResult<int>> CreateAdminProfile([FromRoute] int objectUserId, [FromQuery] string status)
            => Ok(await client.CreateAdminProfileAsync(User.GetUserId(), objectUserId, status));
    }
}
