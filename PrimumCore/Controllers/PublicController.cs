using CoreConnection.DTOs;
using Microsoft.AspNetCore.Mvc;
using PrimumCore.Services.Iterators;

namespace PrimumCore.Controllers
{
    [ApiController]
    [Route("api/public")]
    public class PublicController(
        UserIterator userIterator,
        TeacherIterator teacherIterator,
        CourseIterator courseIterator,
        SheduleIterator sheduleIterator,
        ThemeIterator themeIterator,
        PromocodeIterator promocodeIterator
        ) : PrimumController
    {
        [HttpGet("login")]
        public async Task<IActionResult> Login([FromQuery] string mailAdress, [FromQuery] string password)
        {
            var result = await userIterator.Login(mailAdress, password);
            if (result.Item1 is null) { return Unauthorized(result.Item2); }
            return Ok(result.Item1);
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegUser([FromBody] RegistrationInputDto dto)
            => Ok(await userIterator.RegUser(dto));

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUser([FromRoute] int userId) => Ok(await userIterator.GetUser(userId, true));

        [HttpGet("teacher/{teacherId}")]
        public async Task<IActionResult> GetTeacher([FromRoute] int teacherId) => Ok(await teacherIterator.GetTeacher(teacherId, true));

        [HttpGet("teachers")]
        public async Task<IActionResult> GetTeachers() => Ok(await teacherIterator.GetTeachers(true));

        [HttpGet("courses-by-teacher/{teacherId}")]
        public async Task<IActionResult> GetCoursesByTeacher([FromRoute] int teacherId) 
            => Ok(await courseIterator.GetCoursesByTeacher(teacherId, true));

        [HttpGet("teacher-shedules/{teacherId}")]
        public async Task<IActionResult> GetTeacherShedules([FromRoute] int teacherId)
            => Ok(await sheduleIterator.GetTeacherShedules(teacherId, true));

        [HttpGet("themes")]
        public async Task<IActionResult> GetThemes()
            => Ok(await themeIterator.GetThemes(true));

        [HttpGet("theme/{themeId}")]
        public async Task<IActionResult> GetTheme([FromRoute] int themeId)
            => Ok(await themeIterator.GetTheme(themeId, true));

        [HttpGet("course/{courseId}")]
        public async Task<IActionResult> GetCourse([FromRoute] int courseId)
            => Ok(await courseIterator.GetCourse(courseId, true));

        [HttpGet("courses")]
        public async Task<IActionResult> GetCourses()
            => Ok(await courseIterator.GetCourses(true));

        [HttpGet("courses-by-theme/{themeId}")]
        public async Task<IActionResult> GetCoursesByTheme([FromRoute] int themeId)
            => Ok(await courseIterator.GetCoursesByTheme(themeId, true));

        [HttpGet("available-promocodes")]
        public async Task<IActionResult> GetPromocodes()
            => Ok(await promocodeIterator.GetPromocodes(true));

        [HttpGet("promocode/{promocodeId}")]
        public async Task<IActionResult> GetPromocode([FromRoute] int promocodeId)
            => Ok(await promocodeIterator.GetPromocode(promocodeId, true));
    }
}
