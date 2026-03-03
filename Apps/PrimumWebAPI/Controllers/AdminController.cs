using CoreConnection;
using CoreConnection.DTOs;
using CoreConnection.DTOs.Inputs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrimumWebAPI.Extensions;

namespace PrimumWebAPI.Controllers
{
    [ApiController]
    [Route("api/admin")]
    [Tags("Admin")]
    [Authorize]
    public class AdminController(AdminClient client): DefaultController
    {
        /// <summary>
        /// Профиль админа
        /// </summary>
        /// <returns></returns>
        [HttpGet("profile")]
        public async Task<ActionResult<AdminProfileDto>> GetProfile()
            => Ok(await client.ProfileAsync(User.GetUserId()));

        /// <summary>
        /// Информация о конкретном пользователе
        /// </summary>
        /// <param name="objectUserId"></param>
        /// <returns></returns>
        [HttpGet("get-user/{objectUserId}")]
        public async Task<ActionResult<UserDto>> GetUser([FromRoute] int objectUserId)
            => Ok(await client.GetUserAsync(User.GetUserId(), objectUserId));

        /// <summary>
        /// Список всех пользователей
        /// </summary>
        /// <returns></returns>
        [HttpGet("get-users")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers() => Ok(await client.GetUsersAsync(User.GetUserId()));

        /// <summary>
        /// Список всех инцидентов. Доступно всем
        /// </summary>
        /// <returns></returns>
        [HttpGet("incidents")]
        public async Task<ActionResult<IEnumerable<IncidentDto>>> GetIncidents() => Ok(await client.IncidentsAsync(User.GetUserId()));

        /// <summary>
        /// Список всех админов
        /// </summary>
        /// <returns></returns>
        [HttpGet("other-admins")]
        public async Task<ActionResult<IEnumerable<AdminProfileDto>>> GetAdmins() => Ok(await client.AdminsAsync(User.GetUserId()));

        /// <summary>
        /// Конкретный админ
        /// </summary>
        /// <param name="objectUserId"></param>
        /// <returns></returns>
        [HttpGet("other-admin/{objectUserId}")]
        public async Task<ActionResult<AdminProfileDto>> GetAdmin([FromRoute] int objectUserId)
            => Ok(await client.AdminAsync(User.GetUserId(), objectUserId));

        /// <summary>
        /// Посмотреть логи действий всех админов. Только для админов с правом InspectIncidentLogs
        /// </summary>
        /// <param name="OnlyUnrevisioned"></param>
        /// <returns></returns>
        [HttpGet("incident-logs")]
        public async Task<ActionResult<IEnumerable<IncidentLogDto>>> GetIncidentLogs([FromQuery] bool OnlyUnrevisioned = true)
            => Ok(await client.IncidentLogsAsync(User.GetUserId(), OnlyUnrevisioned));

        /// <summary>
        /// Конкретный лог действия админа. Только для админов с правом InspectIncidentLogs
        /// </summary>
        /// <param name="logId"></param>
        /// <returns></returns>
        [HttpGet("incident-log/{logId}")]
        public async Task<ActionResult<IncidentLogDto>> GetIncidentLog([FromRoute] int logId)
            => Ok(await client.IncidentLogAsync(User.GetUserId(), logId));

        /// <summary>
        /// Отметить лог просмотренным (предполагается, что это делается кнопкой под карточкой лога)
        /// </summary>
        /// <param name="logId"></param>
        /// <returns></returns>
        [HttpGet("revise-incident-log/{logId}")]
        public async Task<ActionResult<int>> RevisionIncidentLog([FromRoute] int logId)
            => Ok(await client.ReviseIncidentLogAsync(User.GetUserId(), logId));

        /// <summary>
        /// Посмотреть все промокоды (даже купленные)
        /// </summary>
        /// <returns></returns>
        [HttpGet("all-promocodes")]
        public async Task<ActionResult<IEnumerable<PromocodeDto>>> GetPromocodes()
            => Ok(await client.AllPromocodesAsync(User.GetUserId()));

        /// <summary>
        /// Конкретный промокод
        /// </summary>
        /// <param name="promocodeId"></param>
        /// <returns></returns>
        [HttpGet("promocode/{promocodeId}")]
        public async Task<ActionResult<PromocodeDto>> GetPromocode([FromRoute] int promocodeId)
            => Ok(await client.PromocodeAsync(User.GetUserId(), promocodeId));

        /// <summary>
        /// Решение инцидента. Доступно всем
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("solve-incident")]
        public async Task<ActionResult<int>> SolveIncedent([FromBody] IncidentDecisionInputDto dto = null!)
            => Ok(await client.SolveIncidentAsync(User.GetUserId(), dto));

        /// <summary>
        /// Добавить (отнять при отрицательном значении cash) деньги у пользователя. Только для админов с правом AddCash
        /// </summary>
        /// <param name="objectUserId"></param>
        /// <param name="cash"></param>
        /// <returns></returns>
        [HttpPatch("add-cash/{objectUserId}")]
        public async Task<ActionResult<int>> AddCash([FromRoute] int objectUserId, [FromQuery] int cash = 0)
            => Ok(await client.AddCashAsync(User.GetUserId(), objectUserId, cash));

        /// <summary>
        /// Забанить пользователя. Только для админов с правом ChangeBanStatus
        /// </summary>
        /// <param name="objectUserId"></param>
        /// <returns></returns>
        [HttpPatch("ban/{objectUserId}")]
        public async Task<ActionResult<int>> BanUser([FromRoute] int objectUserId)
            => Ok(await client.BanAsync(User.GetUserId(), objectUserId));

        /// <summary>
        /// Разбанить пользователя. Только для админов с правом ChangeBanStatus
        /// </summary>
        /// <param name="objectUserId"></param>
        /// <returns></returns>
        [HttpPatch("unban/{objectUserId}")]
        public async Task<ActionResult<int>> UnbanUser([FromRoute] int objectUserId)
            => Ok(await client.UnbanAsync(User.GetUserId(), objectUserId));

        /// <summary>
        /// Редактирование прав админа. Только для админов с правом EditPermissions
        /// </summary>
        /// <param name="objectUserId"></param>
        /// <param name="permissions"></param>
        /// <returns></returns>
        [HttpPatch("edit-permissions/{objectUserId}")]
        public async Task<ActionResult<int>> EditPermissions([FromRoute] int objectUserId, [FromBody] Dictionary<string, bool> permissions = null!)
            => Ok(await client.EditPermissionsAsync(User.GetUserId(), objectUserId, permissions));

        /// <summary>
        /// Реадктирование темы курсов. Только для админов с правом EditCourseThemes
        /// </summary>
        /// <param name="themeId"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPatch("edit-course-theme/{themeId}")]
        public async Task<ActionResult<int>> EditTheme([FromRoute] int themeId, [FromBody] CourseThemeInputDto dto = null!)
            => Ok(await client.EditCourseThemeAsync(User.GetUserId(), themeId, dto));

        /// <summary>
        /// Создать профиль админа пользователю. Только для админов с правом CreateAdminProfiles
        /// </summary>
        /// <param name="objectUserId"></param>
        /// <param name="status">Статус (просто приписка, ни на что не влияющая)</param>
        /// <returns></returns>
        [HttpPut("create-admin-profile/{objectUserId}")]
        public async Task<ActionResult<int>> CreateAdminProfile([FromRoute] int objectUserId, [FromQuery] string status)
            => Ok(await client.CreateAdminProfileAsync(User.GetUserId(), objectUserId, status));

        /// <summary>
        /// Добавить промокод. Только для админов с правом AddPromocodes
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("add-promocode")]
        public async Task<ActionResult<int>> AddPromocode([FromBody] PromocodeInputDto dto = null!)
            => Ok(await client.AddPromocodeAsync(User.GetUserId(), dto));

        /// <summary>
        /// Удалить профиль админа у пользователя. Только для админов с правом CreateAdminProfiles
        /// </summary>
        /// <param name="objectUserId">Id пользователя</param>
        /// <returns></returns>
        [HttpPut("delete-admin-profile/{objectUserId}")]
        public async Task<ActionResult<int>> DeleteAdminProfile([FromRoute] int objectUserId)
            => Ok(await client.DeleteAdminProfileAsync(User.GetUserId(), objectUserId));

        /// <summary>
        /// Создать тему. Только для админов с правом EditCourseThemes
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("create-course-theme")]
        public async Task<ActionResult<int>> CreateTheme([FromBody] CourseThemeInputDto dto = null!)
            => Ok(await client.CreateCourseThemeAsync(User.GetUserId(), dto));

        /// <summary>
        /// Удалить промокод. Только для админов с правом DeletePromocodes
        /// </summary>
        /// <param name="promocodeId"></param>
        /// <returns></returns>
        [HttpDelete("delete-promocode/{promocodeId}")]
        public async Task<ActionResult<int>> DeletePromocode([FromRoute] int promocodeId)
            => Ok(await client.DeletePromocodeAsync(User.GetUserId(), promocodeId));
    }
}
