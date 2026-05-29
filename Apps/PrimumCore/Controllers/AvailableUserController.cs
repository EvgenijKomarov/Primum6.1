using CoreConnection.DTOs;
using Microsoft.AspNetCore.Mvc;
using PrimumCore.Entities;
using PrimumCore.Services.Iterators;
using Serilog;
using SignServiceConnection.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace PrimumCore.Controllers
{
    [ApiController]
    [Route("api/available-user/{userId}")]
    [Tags("User")]
    public class AvailableUserController(UserIterator iterator, ChatSignTokenIterator chatSignTokenIterator) : PrimumController
    {
        [HttpPost("create-teacher-profile")]
        public async Task<ActionResult<int>> CreateTeacherProfile([FromRoute] int userId, [FromBody] string about)
            => Ok(await iterator.CreateTeacherProfile(userId, about));

        [HttpPost("create-student-profile")]
        public async Task<ActionResult<int>> CreateStudentProfile([FromRoute] int userId)
            => Ok(await iterator.CreateStudentProfile(userId));

        [HttpPost("confirm-chat/{token}")]
        public async Task<ActionResult<int>> ConfirmChat([FromRoute] int userId, [FromRoute] string token)
            => Ok(await chatSignTokenIterator.AddChat(userId, token));

        [HttpGet("chat-signs")]
        public async Task<ActionResult<PageResult<ChatSign>>> GetChatSigns([FromRoute] int userId, [FromQuery] int page = 0, [FromQuery] int pageSize = 10)
            => Ok(await chatSignTokenIterator.GetChatSigns(userId, page, pageSize));
    }
}
