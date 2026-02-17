using CoreConnection;
using CoreConnection.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrimumWebAPI.Extensions;
using System.Security.Claims;

namespace PrimumWebAPI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/user")]
    [Tags("User")]
    public class UserController(UserClient client): DefaultController
    {
        [HttpGet("profile")]
        public async Task<ActionResult<UserDto>> GetProfile() => Ok(await client.ProfileAsync(User.GetUserId()));

        [HttpPost("send-email-verification")]
        public async Task<ActionResult<int>> SendEmailVerification([FromQuery] string? correctiveMail)
            => Ok(await client.SendEmailVerificationAsync(User.GetUserId(), correctiveMail));

        [HttpPost("confirm-email")]
        public async Task<ActionResult<int>> ConfirmEmail([FromQuery] string token)
            => Ok(await client.ConfirmEmailAsync(User.GetUserId(), token));

        /*[HttpPatch("deposit")]
        public async Task<ActionResult<long>> DepositMoney([FromQuery] long cash)
            => Ok(await client.DepositAsync(User.GetUserId(), cash));

        [HttpPatch("withdrawn")]
        public async Task<ActionResult<long>> WithdrawnMoney([FromQuery] long cash)
            => Ok(await client.WithdrawnAsync(User.GetUserId(), cash));*/

        [HttpPost("create-teacher-profile")]
        public async Task<ActionResult<int>> CreateTeacherProfile([FromBody] string about)
            => Ok(await client.CreateTeacherProfileAsync(User.GetUserId(), about));

        [HttpPost("create-student-profile")]
        public async Task<ActionResult<int>> CreateStudentProfile()
            => Ok(await client.CreateStudentProfileAsync(User.GetUserId()));
    }
}
