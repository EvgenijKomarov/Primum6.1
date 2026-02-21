using CoreConnection.DTOs;
using Microsoft.AspNetCore.Mvc;
using PrimumCore.Services.Iterators;

namespace PrimumCore.Controllers
{
    [ApiController]
    [Route("api/user/{userId}")]
    [Tags("User")]
    public class UserController(
        UserIterator userIterator, 
        TokenIterator tokenIterator,
        AnonymousTokenIterator anonTokenIterator) : PrimumController
    {
        [HttpGet("profile")]
        public async Task<ActionResult<UserDto>> GetUser([FromRoute] int userId) => Ok(await userIterator.GetUser(userId, false));

        [HttpPost("send-email-verification")]
        public async Task<ActionResult<int>> SendEmailVerification([FromRoute] int userId, [FromQuery] string? correctiveMail)
            => Ok(await tokenIterator.SendEmailVerification(userId, correctiveMail));

        [HttpPost("confirm-email/{token}")]
        public async Task<ActionResult<int>> ConfirmEmail([FromRoute] int userId, [FromRoute] string token)
            => Ok(await tokenIterator.ConfirmToken(userId, token));

        [HttpPost("confirm-chat/{token}")]
        public async Task<ActionResult<int>> ConfirmChat([FromRoute] int userId, [FromRoute] string token)
            => Ok(await anonTokenIterator.AddChat(userId, token));
    }
}
