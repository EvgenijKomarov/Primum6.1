using CoreConnection;
using CoreConnection.DTOs;
using Microsoft.AspNetCore.Mvc;
using PrimumWebAPI.Services;
using System.Net.Mail;

namespace PrimumWebAPI.Controllers
{
    [ApiController]
    [Route("api/public")]
    [Tags("Public")]
    public class PublicController(UserClient userClient, PublicClient client, JwtTokenService tokenService) : DefaultController
    {
        [HttpGet("login")]
        public async Task<ActionResult<string>> Login([FromQuery] string mailAdress, [FromQuery] string password)
        {
            var id = await client.LoginAsync(mailAdress, password);
            var user = await userClient.ProfileAsync(id);

            return Ok(tokenService.GenerateToken(user));
        }

        [HttpPost("register")]
        public async Task<ActionResult<string>> RegUser([FromBody] RegistrationInputDto dto)
        {
            var id = await client.RegisterAsync(dto);
            var user = await userClient.ProfileAsync(id);

            return Ok(tokenService.GenerateToken(user));
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<UserDtoLite>> GetUser([FromRoute] int userId) => Ok(await client.UserAsync(userId));

        [HttpGet("teacher/{teacherId}")]
        public async Task<ActionResult<TeacherProfileDto>> GetTeacher([FromRoute] int teacherId) => Ok(await client.TeacherAsync(teacherId));

        [HttpGet("teachers")]
        public async Task<ActionResult<IEnumerable<TeacherProfileDto>>> GetTeachers() => Ok(await client.TeachersAsync());

        [HttpGet("courses-by-teacher/{teacherId}")]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetCoursesByTeacher([FromRoute] int teacherId)
            => Ok(await client.CoursesByTeacherAsync(teacherId));

        [HttpGet("teacher-shedules/{teacherId}")]
        public async Task<ActionResult<IEnumerable<TeacherSheduleDto>>> GetTeacherShedules([FromRoute] int teacherId)
            => Ok(await client.TeacherShedulesAsync(teacherId));

        [HttpGet("themes")]
        public async Task<ActionResult<IEnumerable<CourseThemeDto>>> GetThemes()
            => Ok(await client.ThemesAsync());

        [HttpGet("theme/{themeId}")]
        public async Task<ActionResult<CourseThemeDto>> GetTheme([FromRoute] int themeId)
            => Ok(await client.ThemeAsync(themeId));

        [HttpGet("course/{courseId}")]
        public async Task<ActionResult<CourseDto>> GetCourse([FromRoute] int courseId)
            => Ok(await client.CourseAsync(courseId));

        [HttpGet("courses")]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetCourses()
            => Ok(await client.CoursesAsync());

        [HttpGet("courses-by-theme/{themeId}")]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetCoursesByTheme([FromRoute] int themeId)
            => Ok(await client.CoursesByThemeAsync(themeId));

        [HttpGet("available-promocodes")]
        public async Task<ActionResult<IEnumerable<PromocodeDto>>> GetPromocodes()
            => Ok(await client.AvailablePromocodesAsync());

        [HttpGet("promocode/{promocodeId}")]
        public async Task<ActionResult<PromocodeDto>> GetPromocode([FromRoute] int promocodeId)
            => Ok(await client.PromocodeAsync(promocodeId));
    }
}
