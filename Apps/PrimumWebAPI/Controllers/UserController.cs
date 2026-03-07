using CoreConnection;
using CoreConnection.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrimumWebAPI.Extensions;
using System.Security.Claims;

namespace PrimumWebAPI.Controllers
{
    [Authorize]
    [Route("user")]
    public class UserController(UserClient client): DefaultController
    {
        /// <summary>
        /// Полный профиль пользователя, включая информацию о том, является ли он учеником или преподавателем, подтверждена ли почта и т.д.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetProfile() => Ok(await client.ProfileAsync(User.GetUserId()));

        /// <summary>
        /// Отправить письмо с подтверждением почты (не сработает если почта уже подтверждена)
        /// </summary>
        /// <param name="correctiveMail">Если нужно внести изменения в адрес почты, можно передать correctiveMail</param>
        /// <returns></returns>
        [HttpPut("send-email-verification")]
        public async Task<ActionResult<int>> SendEmailVerification([FromQuery] string? correctiveMail)
            => Ok(await client.SendEmailVerificationAsync(User.GetUserId(), correctiveMail));

        /// <summary>
        /// Подтвердить почту, отправив пришедший в письме токен 
        /// </summary>
        /// <param name="token">Токен из письма</param>
        /// <returns></returns>
        [HttpPost("confirm-email")]
        public async Task<ActionResult<int>> ConfirmEmail([FromBody] string token)
            => Ok(await client.ConfirmEmailAsync(User.GetUserId(), token));

        /// <summary>
        /// Подтвердить чат, отправив токен из него
        /// </summary>
        /// <param name="token">Токен из чата</param>
        /// <returns></returns>
        [HttpPost("confirm-chat")]
        public async Task<ActionResult<int>> ConfirmChat([FromBody] string token)
            => Ok(await client.ConfirmChatAsync(User.GetUserId(), token));

        /*[HttpPatch("deposit")]
        public async Task<ActionResult<long>> DepositMoney([FromQuery] long cash)
            => Ok(await client.DepositAsync(User.GetUserId(), cash));

        [HttpPatch("withdrawn")]
        public async Task<ActionResult<long>> WithdrawnMoney([FromQuery] long cash)
            => Ok(await client.WithdrawnAsync(User.GetUserId(), cash));*/

        /// <summary>
        /// Создать профиль преподавателя
        /// </summary>
        /// <param name="about">О преподавателе (обязательно)</param>
        /// <returns></returns>
        [HttpPost("create-teacher-profile")]
        public async Task<ActionResult<int>> CreateTeacherProfile([FromBody] string about)
            => Ok(await client.CreateTeacherProfileAsync(User.GetUserId(), about));

        /// <summary>
        /// Создать профиль ученика
        /// </summary>
        /// <returns></returns>
        [HttpPost("create-student-profile")]
        public async Task<ActionResult<int>> CreateStudentProfile()
            => Ok(await client.CreateStudentProfileAsync(User.GetUserId()));
    }
}
