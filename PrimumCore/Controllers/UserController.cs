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
        public async Task<IActionResult> GetUser(int id) => Ok(iterator.GetUser(id));

        [HttpPost("reg-teacher")]
        public async Task<IActionResult> RegTeacher([FromBody] UserDTO dto, [FromBody] string about) => Ok(iterator.RegTeacher(dto, about));


        [HttpPost("reg-user")]
        public async Task<IActionResult> RegStudent([FromBody] UserDTO dto) => Ok(iterator.RegStudent(dto));
    }
}
