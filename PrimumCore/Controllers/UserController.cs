using CoreConnection.DTOs;
using Microsoft.AspNetCore.Mvc;
using PrimumCore.Services.Iterators;
using Serilog;

namespace PrimumCore.Controllers
{
    [ApiController]
    [Route("api/user/{userId}")]
    public class UserController(UserIterator iterator, TokenIterator tokenIterator) : PrimumController
    {
        [HttpGet("profile")]
        public async Task<IActionResult> GetUser([FromRoute] int userId) => Ok(await iterator.GetUser(userId));

        [HttpPatch("deposit")]
        public async Task<IActionResult> DepositMoney([FromRoute] int userId, [FromQuery] long cash)
            => Ok(await iterator.AddMoney(userId, cash));

        [HttpPatch("withdrawn")]
        public async Task<IActionResult> WithdrawnMoney([FromRoute] int userId, [FromQuery] long cash)
            => Ok(await iterator.GetMoney(userId, cash));

        [HttpPost("send-email-verification")]
        public async Task<IActionResult> SendEmailVerification([FromRoute] int userId, [FromQuery] string? correctiveMail)
            => Ok(await tokenIterator.SendEmailVerification(userId, correctiveMail));

        [HttpPost("confirm-email")]
        public async Task<IActionResult> ConfirmEmail([FromRoute] int userId, [FromQuery] string token)
            => Ok(await tokenIterator.ConfirmToken(userId, token));

        [HttpPost("create-teacher-profile")]
        public async Task<IActionResult> CreateTeacherProfile([FromRoute] int userId, [FromBody] string about)
            => Ok(await iterator.CreateTeacherProfile(userId, about));

        [HttpPost("create-student-profile")]
        public async Task<IActionResult> CreateStudentProfile([FromRoute] int userId)
            => Ok(await iterator.CreateStudentProfile(userId));
    }
}
