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
        public async Task<IActionResult> Login([FromQuery] string login, [FromQuery] string password)
        {
            var result = await iterator.Login(login, password);
            if (result.Item1 is null) { return Unauthorized(result.Item2); }
            return Ok(result.Item1);
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegUser([FromBody] RegistrationInputDto dto)
            => Ok(await iterator.RegUser(dto));

        [HttpPost("create-teacher-profile/{userId}")]
        public async Task<IActionResult> CreateTeacherProfile([FromRoute] int userId, [FromBody] string about)
            => Ok(await iterator.CreateTeacherProfile(userId, about));

        [HttpPost("create-student-profile/{userId}")]
        public async Task<IActionResult> CreateStudentProfile([FromRoute] int userId)
            => Ok(await iterator.CreateStudentProfile(userId));
    }
}
