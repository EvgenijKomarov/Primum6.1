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
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers() => Ok(await client.GetUsersAsync(User.GetUserId()));

        /// <summary>
        /// Информация о конкретном пользователе
        /// </summary>
        /// <param name="objectUserId"></param>
        /// <returns></returns>
        [HttpGet("{objectUserId}")]
        public async Task<ActionResult<UserDto>> GetUser([FromRoute] int objectUserId)
            => Ok(await client.GetUserAsync(User.GetUserId(), objectUserId));

        /// <summary>
        /// Добавить (отнять при отрицательном значении cash) деньги у пользователя. Только для админов с правом AddCash
        /// </summary>
        /// <param name="objectUserId"></param>
        /// <param name="cash"></param>
        /// <returns></returns>
        [HttpPatch("{objectUserId}/add-cash/{cash}")]
        public async Task<ActionResult<int>> AddCash([FromRoute] int objectUserId, [FromRoute] int cash = 0)
            => Ok(await client.AddCashAsync(User.GetUserId(), objectUserId, cash));

        /// <summary>
        /// Забанить пользователя. Только для админов с правом ChangeBanStatus
        /// </summary>
        /// <param name="objectUserId"></param>
        /// <returns></returns>
        [HttpPatch("{objectUserId}/ban")]
        public async Task<ActionResult<int>> BanUser([FromRoute] int objectUserId)
            => Ok(await client.BanAsync(User.GetUserId(), objectUserId));

        /// <summary>
        /// Разбанить пользователя. Только для админов с правом ChangeBanStatus
        /// </summary>
        /// <param name="objectUserId"></param>
        /// <returns></returns>
        [HttpPatch("{objectUserId}/unban")]
        public async Task<ActionResult<int>> UnbanUser([FromRoute] int objectUserId)
            => Ok(await client.UnbanAsync(User.GetUserId(), objectUserId));

        /// <summary>
        /// Создать профиль админа пользователю. Только для админов с правом CreateAdminProfiles
        /// </summary>
        /// <param name="objectUserId"></param>
        /// <param name="status">Статус (просто приписка, ни на что не влияющая)</param>
        /// <returns></returns>
        [HttpPut("{objectUserId}/create-admin-profile")]
        public async Task<ActionResult<int>> CreateAdminProfile([FromRoute] int objectUserId, [FromQuery] string status)
            => Ok(await client.CreateAdminProfileAsync(User.GetUserId(), objectUserId, status));
    }
}
