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
