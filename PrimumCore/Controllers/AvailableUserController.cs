using CoreConnection.DTOs;
using Microsoft.AspNetCore.Mvc;
using PrimumCore.Services.Iterators;
using Serilog;

namespace PrimumCore.Controllers
{
    [ApiController]
    [Route("api/available-user/{userId}")]
    public class AvailableUserController(UserIterator iterator) : PrimumController
    {
        [HttpPatch("deposit")]
        public async Task<IActionResult> DepositMoney([FromRoute] int userId, [FromQuery] long cash)
            => Ok(await iterator.AddMoney(userId, cash));

        [HttpPatch("withdrawn")]
        public async Task<IActionResult> WithdrawnMoney([FromRoute] int userId, [FromQuery] long cash)
            => Ok(await iterator.GetMoney(userId, cash));

        [HttpPost("create-teacher-profile")]
        public async Task<IActionResult> CreateTeacherProfile([FromRoute] int userId, [FromBody] string about)
            => Ok(await iterator.CreateTeacherProfile(userId, about));

        [HttpPost("create-student-profile")]
        public async Task<IActionResult> CreateStudentProfile([FromRoute] int userId)
            => Ok(await iterator.CreateStudentProfile(userId));
    }
}
