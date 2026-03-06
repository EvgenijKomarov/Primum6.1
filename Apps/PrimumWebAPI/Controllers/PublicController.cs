using CoreConnection;
using CoreConnection.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrimumWebAPI.Services;
using System.Net.Mail;

namespace PrimumWebAPI.Controllers
{
    [Route("public")]
    [AllowAnonymous]
    public class PublicController(UserClient userClient, PublicClient client, JwtTokenService tokenService) : DefaultController
    {
        /// <summary>
        /// Контроллер для авторизации. Возвращает JWT токен
        /// </summary>
        /// <param name="mailAdress">Адрес почты</param>
        /// <param name="password">Пароль (голый, без шифрования)</param>
        /// <returns></returns>
        [HttpGet("login")]
        public async Task<ActionResult<string>> Login([FromQuery] string mailAdress, [FromQuery] string password)
        {
            var id = await client.LoginAsync(mailAdress, password);
            var user = await userClient.ProfileAsync(id);

            return Ok(tokenService.GenerateToken(user));
        }

        /// <summary>
        /// Регистрация. Возвращает при успехе JWT токен
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task<ActionResult<string>> RegUser([FromBody] RegistrationInputDto dto)
        {
            var id = await client.RegisterAsync(dto);
            var user = await userClient.ProfileAsync(id);

            return Ok(tokenService.GenerateToken(user));
        }
    }
}
