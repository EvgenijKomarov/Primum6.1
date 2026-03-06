using CoreConnection;
using CoreConnection.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PrimumWebAPI.Controllers
{
    [Route("public/user")]
    [AllowAnonymous]
    public class PublicUserController(PublicClient client) : DefaultController
    {
        /// <summary>
        /// Информация о ЛЮБОМ пользователе
        /// </summary>
        /// <param name="userId">Id пользователя</param>
        /// <returns></returns>
        [HttpGet("{userId}")]
        public async Task<ActionResult<UserDtoLite>> GetUser([FromRoute] int userId) => Ok(await client.UserAsync(userId));
    }
}
