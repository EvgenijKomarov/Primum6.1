using CoreConnection.DTOs;
using Microsoft.AspNetCore.Mvc;
using PrimumCore.Services.Iterators;

namespace PrimumCore.Controllers
{
    [ApiController]
    [Route("api/user/{userId}")]
    [Tags("User")]
    public class UserController(UserIterator userIterator, TokenIterator tokenIterator) : PrimumController
    {
        [HttpGet("profile")]
        public async Task<ActionResult<UserDto>> GetUser([FromRoute] int userId) => Ok(await userIterator.GetUser(userId, false));

        [HttpPost("send-email-verification")]
        public async Task<ActionResult<int>> SendEmailVerification([FromRoute] int userId, [FromQuery] string? correctiveMail)
            => Ok(await tokenIterator.SendEmailVerification(userId, correctiveMail));

        [HttpPost("confirm-email")]
        public async Task<ActionResult<int>> ConfirmEmail([FromRoute] int userId, [FromQuery] string token)
            => Ok(await tokenIterator.ConfirmToken(userId, token));
    }
}
