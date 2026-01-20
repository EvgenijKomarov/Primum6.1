using Microsoft.AspNetCore.Mvc;
using PrimumCore.Services;

namespace PrimumCore.Controllers
{
    [ApiController]
    [Route("api/common")]
    public class CommonController(CommonIterator iterator) : PrimumController
    {
        [HttpGet("themes")]
        public async Task<IActionResult> GetThemes() => Ok(await iterator.GetThemes());

        [HttpGet("theme")]
        public async Task<IActionResult> GetTheme([FromRoute] int themeId) => Ok(await iterator.GetTheme(themeId));
    }
}
