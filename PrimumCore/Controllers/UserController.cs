using CoreConnection.DTOs;
using Microsoft.AspNetCore.Mvc;
using PrimumCore.Services;

namespace PrimumCore.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController(UserIterator iterator) : PrimumController
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser([FromRoute] int id) => Ok(await iterator.GetUser(id));

        [HttpGet("login")]
        public async Task<IActionResult> Login([FromQuery] string login, [FromQuery] string password) => Ok(await iterator.Login(login, password));

        [HttpPost("register")]
        public async Task<IActionResult> RegUser([FromBody] RegistrationDto dto)
            => Ok(await iterator.RegUser(dto));

        [HttpPost("create-teacher-profile/{userId}")]
        public async Task<IActionResult> CreateTeacherProfile([FromRoute] int userId, [FromBody] string about)
            => Ok(await iterator.CreateTeacherProfile(userId, about));

        [HttpPost("create-student-profile/{userId}")]
        public async Task<IActionResult> CreateStudentProfile([FromRoute] int userId)
            => Ok(await iterator.CreateStudentProfile(userId));
    }
}
