using CoreConnection;
using CoreConnection.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PrimumWebAPI.Controllers
{
    [Route("public/themes")]
    [AllowAnonymous]
    public class PublicThemeController(PublicClient client) : DefaultController
    {
        /// <summary>
        /// Темы курсов
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<CourseThemeDtoPageResult>> GetThemes(
            [FromQuery] int page = 0,
            [FromQuery] int pageSize = 10)
            => Ok(await client.ThemesAsync(page, pageSize));

        /// <summary>
        /// Конкретная тема
        /// </summary>
        /// <param name="themeId"></param>
        /// <returns></returns>
        [HttpGet("{themeId}")]
        public async Task<ActionResult<CourseThemeDto>> GetTheme([FromRoute] int themeId)
            => Ok(await client.ThemeAsync(themeId));
    }
}
