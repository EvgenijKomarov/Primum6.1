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
        [HttpPost("create-teacher-profile")]
        public async Task<ActionResult<int>> CreateTeacherProfile([FromRoute] int userId, [FromBody] string about)
            => Ok(await iterator.CreateTeacherProfile(userId, about));

        [HttpPost("create-student-profile")]
        public async Task<ActionResult<int>> CreateStudentProfile([FromRoute] int userId)
            => Ok(await iterator.CreateStudentProfile(userId));
    }
}
