using CoreConnection.DTOs;
using Microsoft.AspNetCore.Mvc;
using PrimumCore.Entities;
using PrimumCore.Services.Iterators;
using Serilog;
using SignServiceConnection.Models;
using Swashbuckle.AspNetCore.Annotations;
using YamlDotNet.Core.Tokens;

namespace PrimumCore.Controllers
{
    [ApiController]
    [Route("api/available-user/{userId}")]
    [Tags("User")]
    public class AvailableUserController(UserIterator iterator, ChatSignTokenIterator chatSignTokenIterator) : PrimumController
    {
        [HttpPost("create-teacher-profile")]
        public async Task<ActionResult<int>> CreateTeacherProfile(
            [FromRoute] int userId, 
            [FromBody] TeacherRegistrationInputDto dto
            )
            => Ok(await iterator.CreateTeacherProfile(userId, dto));

        [HttpPost("create-student-profile")]
        public async Task<ActionResult<int>> CreateStudentProfile([FromRoute] int userId)
            => Ok(await iterator.CreateStudentProfile(userId));

        [HttpPost("confirm-chat/{token}")]
        public async Task<ActionResult<int>> ConfirmChat([FromRoute] int userId, [FromRoute] string token)
            => Ok(await chatSignTokenIterator.AddChat(userId, token));

        [HttpDelete("delete-chat-sign")]
        public async Task<ActionResult<int>> DeleteChatSign([FromRoute] int userId, [FromBody] ChatSign sign = null!)
            => Ok(await chatSignTokenIterator.DeleteChatSign(userId, sign));

        [HttpGet("chat-signs")]
        public async Task<ActionResult<PageResult<ChatSign>>> GetChatSigns([FromRoute] int userId, [FromQuery] int page = 0, [FromQuery] int pageSize = 10)
            => Ok(await chatSignTokenIterator.GetChatSigns(userId, page, pageSize));
    }
}
