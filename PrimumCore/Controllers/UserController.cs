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

        [HttpPost("reg-teacher")]
        public async Task<IActionResult> RegTeacher([FromBody] RegistrationDto dto)
            => Ok(await iterator.RegTeacher(dto));

        [HttpPost("reg-student")]
        public async Task<IActionResult> RegStudent([FromBody] RegistrationDto dto)
            => Ok(await iterator.RegStudent(dto));
    }
}
