using CoreConnection;
using CoreConnection.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrimumWebAPI.Services;
using System.Net.Mail;

namespace PrimumWebAPI.Controllers
{
    [ApiController]
    [Route("api/public")]
    [AllowAnonymous]
    [Tags("Public")]
    public class PublicController(UserClient userClient, PublicClient client, JwtTokenService tokenService) : DefaultController
    {
        /// <summary>
        /// Контроллер для авторизации. Возвращает JWT токен
        /// </summary>
        /// <param name="mailAdress">Адрес почты</param>
        /// <param name="password">Пароль (голый, без шифрования)</param>
        /// <returns></returns>
        [HttpGet("login")]
        public async Task<ActionResult<string>> Login([FromQuery] string mailAdress, [FromQuery] string password)
        {
            var id = await client.LoginAsync(mailAdress, password);
            var user = await userClient.ProfileAsync(id);

            return Ok(tokenService.GenerateToken(user));
        }

        /// <summary>
        /// Регистрация. Возвращает при успехе JWT токен
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task<ActionResult<string>> RegUser([FromBody] RegistrationInputDto dto)
        {
            var id = await client.RegisterAsync(dto);
            var user = await userClient.ProfileAsync(id);

            return Ok(tokenService.GenerateToken(user));
        }

        /// <summary>
        /// Информация о ЛЮБОМ пользователе
        /// </summary>
        /// <param name="userId">Id пользователя</param>
        /// <returns></returns>
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<UserDtoLite>> GetUser([FromRoute] int userId) => Ok(await client.UserAsync(userId));

        /// <summary>
        /// Информация о преподавателе
        /// </summary>
        /// <param name="teacherId">Id преподавателя</param>
        /// <returns></returns>
        [HttpGet("teacher/{teacherId}")]
        public async Task<ActionResult<TeacherProfileDto>> GetTeacher([FromRoute] int teacherId) => Ok(await client.TeacherAsync(teacherId));

        /// <summary>
        /// Все доступные преподаватели
        /// </summary>
        /// <returns></returns>
        [HttpGet("teachers")]
        public async Task<ActionResult<IEnumerable<TeacherProfileDto>>> GetTeachers() => Ok(await client.TeachersAsync());

        /// <summary>
        /// Все курсы преподавателя
        /// </summary>
        /// <param name="teacherId">Id преподавателя</param>
        /// <returns></returns>
        [HttpGet("courses-by-teacher/{teacherId}")]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetCoursesByTeacher([FromRoute] int teacherId)
            => Ok(await client.CoursesByTeacherAsync(teacherId));

        /// <summary>
        /// Все расписания преподавателя
        /// </summary>
        /// <param name="teacherId">Id преподавателя</param>
        /// <returns></returns>
        [HttpGet("teacher-shedules/{teacherId}")]
        public async Task<ActionResult<IEnumerable<TeacherSheduleDto>>> GetTeacherShedules([FromRoute] int teacherId)
            => Ok(await client.TeacherShedulesAsync(teacherId));

        /// <summary>
        /// Темы курсов
        /// </summary>
        /// <returns></returns>
        [HttpGet("themes")]
        public async Task<ActionResult<IEnumerable<CourseThemeDto>>> GetThemes()
            => Ok(await client.ThemesAsync());

        /// <summary>
        /// Конкретная тема
        /// </summary>
        /// <param name="themeId"></param>
        /// <returns></returns>
        [HttpGet("theme/{themeId}")]
        public async Task<ActionResult<CourseThemeDto>> GetTheme([FromRoute] int themeId)
            => Ok(await client.ThemeAsync(themeId));

        /// <summary>
        /// Конкретный курс
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        [HttpGet("course/{courseId}")]
        public async Task<ActionResult<CourseDto>> GetCourse([FromRoute] int courseId)
            => Ok(await client.CourseAsync(courseId));

        /// <summary>
        /// Все курсы
        /// </summary>
        /// <returns></returns>
        [HttpGet("courses")]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetCourses()
            => Ok(await client.CoursesAsync());

        /// <summary>
        /// Курсы по теме
        /// </summary>
        /// <param name="themeId">Id темы</param>
        /// <returns></returns>
        [HttpGet("courses-by-theme/{themeId}")]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetCoursesByTheme([FromRoute] int themeId)
            => Ok(await client.CoursesByThemeAsync(themeId));

        /// <summary>
        /// Все промокоды, доступные на площадке
        /// </summary>
        /// <returns></returns>
        [HttpGet("available-promocodes")]
        public async Task<ActionResult<IEnumerable<PromocodeDto>>> GetPromocodes()
            => Ok(await client.AvailablePromocodesAsync());

        /// <summary>
        /// Посмотреть конкретный промокод
        /// </summary>
        /// <param name="promocodeId">Id промокода</param>
        /// <returns></returns>
        [HttpGet("promocode/{promocodeId}")]
        public async Task<ActionResult<PromocodeDto>> GetPromocode([FromRoute] int promocodeId)
            => Ok(await client.PromocodeAsync(promocodeId));
    }
}
