using Microsoft.AspNetCore.Mvc;
using PrimumCore.Services.Iterators;

namespace PrimumCore.Controllers
{
    [ApiController]
    [Route("api/user/{userId}")]
    public class UserController(UserIterator userIterator, TokenIterator tokenIterator) : PrimumController
    {
        [HttpGet("profile")]
        public async Task<IActionResult> GetUser([FromRoute] int userId) => Ok(await userIterator.GetUser(userId, false));

        [HttpPost("send-email-verification")]
        public async Task<IActionResult> SendEmailVerification([FromRoute] int userId, [FromQuery] string? correctiveMail)
            => Ok(await tokenIterator.SendEmailVerification(userId, correctiveMail));

        [HttpPost("confirm-email")]
        public async Task<IActionResult> ConfirmEmail([FromRoute] int userId, [FromQuery] string token)
            => Ok(await tokenIterator.ConfirmToken(userId, token));
    }
}
