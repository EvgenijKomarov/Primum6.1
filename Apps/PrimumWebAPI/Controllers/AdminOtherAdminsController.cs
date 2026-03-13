using CoreConnection;
using CoreConnection.DTOs;
using CoreDBModel.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrimumWebAPI.Extensions;

namespace PrimumWebAPI.Controllers
{
    [Route("admin/other-admins")]
    [Authorize]
    public class AdminOtherAdminsController(AdminClient client) : DefaultController
    {
        /// <summary>
        /// Список всех админов
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<AdminProfileDtoPageResult>> GetAdmins(
            [FromQuery] int page = 0,
            [FromQuery] int pageSize = 10) 
            => Ok(await client.AdminsAsync(User.GetUserId(), page, pageSize));

        /// <summary>
        /// Конкретный админ
        /// </summary>
        /// <param name="objectUserId"></param>
        /// <returns></returns>
        [HttpGet("{objectUserId}")]
        public async Task<ActionResult<AdminProfileDto>> GetAdmin([FromRoute] int objectUserId)
            => Ok(await client.AdminAsync(User.GetUserId(), objectUserId));

        /// <summary>
        /// Редактирование прав админа. Только для админов с правом EditPermissions
        /// </summary>
        /// <param name="objectUserId"></param>
        /// <param name="permissions"></param>
        /// <returns></returns>
        [HttpPatch("{objectUserId}/permissions")]
        public async Task<ActionResult<int>> EditPermissions([FromRoute] int objectUserId, [FromBody] Dictionary<string, bool> permissions = null!)
            => Ok(await client.EditPermissionsAsync(User.GetUserId(), objectUserId, permissions));

        /// <summary>
        /// Удалить профиль админа у пользователя. Только для админов с правом CreateAdminProfiles
        /// </summary>
        /// <param name="objectUserId">Id пользователя</param>
        /// <returns></returns>
        [HttpDelete("{objectUserId}")]
        public async Task<ActionResult<int>> DeleteAdminProfile([FromRoute] int objectUserId)
            => Ok(await client.DeleteAdminProfileAsync(User.GetUserId(), objectUserId));
    }
}
