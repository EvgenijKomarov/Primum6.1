using CoreConnection.DTOs;
using Microsoft.AspNetCore.Mvc;
using PrimumCore.Services.Iterators;
using Serilog;

namespace PrimumCore.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController(UserIterator iterator) : PrimumController
    {
        [HttpGet("login")]
        public async Task<IActionResult> Login([FromQuery] string mailAdress, [FromQuery] string password)
        {
            var result = await iterator.Login(mailAdress, password);
            if (result.Item1 is null) { return Unauthorized(result.Item2); }
            return Ok(result.Item1);
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegUser([FromBody] RegistrationInputDto dto)
            => Ok(await iterator.RegUser(dto));

        [HttpPost("send-email-verification/{userId}")]
        public async Task<IActionResult> SendEmailVerification([FromRoute] int userId, [FromQuery] string? correctiveMail)
            => Ok(await iterator.SendEmailVerification(userId, correctiveMail));

        [HttpPost("confirm-email/{userId}")]
        public async Task<IActionResult> ConfirmEmail([FromRoute] int userId, [FromQuery] string token)
            => Ok(await iterator.ConfirmToken(userId, token));

        [HttpPost("create-teacher-profile/{userId}")]
        public async Task<IActionResult> CreateTeacherProfile([FromRoute] int userId, [FromBody] string about)
            => Ok(await iterator.CreateTeacherProfile(userId, about));

        [HttpPost("create-student-profile/{userId}")]
        public async Task<IActionResult> CreateStudentProfile([FromRoute] int userId)
            => Ok(await iterator.CreateStudentProfile(userId));
    }
}
