using CoreConnection.DTOs;
using Microsoft.AspNetCore.Mvc;
using PrimumCore.Services.Iterators;
using Serilog;
using Swashbuckle.AspNetCore.Annotations;

namespace PrimumCore.Controllers
{
    [ApiController]
    [Route("api/available-user/{userId}")]
    [Tags("User")]
    public class AvailableUserController(UserIterator iterator) : PrimumController
    {
        [HttpPatch("deposit")]
        public async Task<ActionResult<long>> DepositMoney([FromRoute] int userId, [FromQuery] long cash = 0)
            => Ok(await iterator.AddMoney(userId, cash));

        [HttpPatch("withdrawn")]
        public async Task<ActionResult<long>> WithdrawnMoney([FromRoute] int userId, [FromQuery] long cash = 0)
            => Ok(await iterator.GetMoney(userId, cash));

        [HttpPost("create-teacher-profile")]
        public async Task<ActionResult<int>> CreateTeacherProfile([FromRoute] int userId, [FromBody] string about)
            => Ok(await iterator.CreateTeacherProfile(userId, about));

        [HttpPost("create-student-profile")]
        public async Task<ActionResult<int>> CreateStudentProfile([FromRoute] int userId)
            => Ok(await iterator.CreateStudentProfile(userId));
    }
}
