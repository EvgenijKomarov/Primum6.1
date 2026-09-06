using CoreConnection;
using CoreConnection.DTOs.Inputs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrimumWebAPI.Extensions;

namespace PrimumWebAPI.Controllers
{
    [Route("admin/themes")]
    [Authorize]
    public class AdminCourseThemeController(AdminClient client) : DefaultController
    {
        
        /// <summary>
        /// Посмотреть все темы курсов
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<int>> GetThemes([FromQuery] int page = 0, [FromQuery] int pageSize = 10)
            => Ok(await client.GetCourseThemesAsync(User.GetUserId(), page, pageSize));

        /// <summary>
        /// Создать тему. Только для админов с правом EditCourseThemes
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<int>> CreateTheme([FromBody] CourseThemeInputDto dto = null!)
            => Ok(await client.CreateCourseThemeAsync(User.GetUserId(), dto));

        /// <summary>
        /// Реадктирование темы курсов. Только для админов с правом EditCourseThemes
        /// </summary>
        /// <param name="themeId"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPatch("{themeId}")]
        public async Task<ActionResult<int>> EditTheme([FromRoute] int themeId, [FromBody] CourseThemeInputDto dto = null!)
            => Ok(await client.EditCourseThemeAsync(User.GetUserId(), themeId, dto));
    }
}
