using DTO;
using DTO.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrimumCore.Models;
using PrimumCore.Services;
using PrimumPlatformModel.Models.Enums;

namespace PrimumCore.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController(UserIterator iterator) : PrimumController
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id) => Ok(await iterator.GetUser(id));

        [HttpGet("login")]
        public async Task<IActionResult> Login([FromBody] string login, [FromBody] string password) => Ok(await iterator.Login(login, password));

        [HttpPost("reg-teacher")]
        public async Task<IActionResult> RegTeacher([FromBody] UserDTO dto, [FromBody] string about, [FromBody] string login, [FromBody] string password)
            => Ok(await iterator.RegTeacher(login, password, dto, about));

        [HttpPost("reg-student")]
        public async Task<IActionResult> RegStudent([FromBody] UserDTO dto, [FromBody] string login, [FromBody] string password)
            => Ok(await iterator.RegStudent(login, password, dto));
    }
}
