using CoreConnection.DTOs;
using PrimumCore.Entities;
using Microsoft.AspNetCore.Mvc;
using PrimumCore.Services.Iterators;

namespace PrimumCore.Controllers
{
    [ApiController]
    [Route("api/public")]
    [Tags("Public")]
    public class PublicController(
        UserIterator userIterator,
        TeacherIterator teacherIterator,
        CourseIterator courseIterator,
        TeacherSheduleIterator sheduleIterator,
        ThemeIterator themeIterator,
        PromocodeIterator promocodeIterator
        ) : PrimumController
    {
        [HttpGet("login")]
        public async Task<ActionResult<int>> Login([FromQuery] string mailAdress, [FromQuery] string password)
            => Ok(await userIterator.Login(mailAdress, password));

        [HttpPost("register")]
        public async Task<ActionResult<int>> RegUser([FromBody] RegistrationInputDto dto)
            => Ok(await userIterator.RegUser(dto));

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<UserDtoLite>> GetUser([FromRoute] int userId) => Ok(await userIterator.GetUserLite(userId, true));

        [HttpGet("teacher/{teacherId}")]
        public async Task<ActionResult<TeacherProfileDto>> GetTeacher([FromRoute] int teacherId) => Ok(await teacherIterator.GetTeacher(teacherId, true));

        [HttpGet("teachers")]
        public async Task<ActionResult<PageResult<TeacherProfileDto>>> GetTeachers([FromQuery] int page = 0, [FromQuery] int pageSize = 10) => Ok(await teacherIterator.GetTeachers(true, page, pageSize));

        [HttpGet("courses-by-teacher/{teacherId}")]
        public async Task<ActionResult<PageResult<CourseDto>>> GetCoursesByTeacher([FromRoute] int teacherId, [FromQuery] int page = 0, [FromQuery] int pageSize = 10) 
            => Ok(await courseIterator.GetCoursesByTeacher(teacherId, true, page, pageSize));

        [HttpGet("teacher-shedules/{teacherId}")]
        public async Task<ActionResult<PageResult<TeacherSheduleDto>>> GetTeacherShedules([FromRoute] int teacherId, [FromQuery] int page = 0, [FromQuery] int pageSize = 10)
            => Ok(await sheduleIterator.GetTeacherShedules(teacherId, true, page, pageSize));

        [HttpGet("themes")]
        public async Task<ActionResult<PageResult<CourseThemeDto>>> GetThemes([FromQuery] int page = 0, [FromQuery] int pageSize = 10)
            => Ok(await themeIterator.GetThemes(true, page, pageSize));

        [HttpGet("theme/{themeId}")]
        public async Task<ActionResult<CourseThemeDto>> GetTheme([FromRoute] int themeId)
            => Ok(await themeIterator.GetTheme(themeId, true));

        [HttpGet("course/{courseId}")]
        public async Task<ActionResult<CourseDto>> GetCourse([FromRoute] int courseId)
            => Ok(await courseIterator.GetCourse(courseId, true));

        [HttpGet("courses")]
        public async Task<ActionResult<PageResult<CourseDto>>> GetCourses([FromQuery] int page = 0, [FromQuery] int pageSize = 10)
            => Ok(await courseIterator.GetCourses(true, page, pageSize));

        [HttpGet("courses-by-theme/{themeId}")]
        public async Task<ActionResult<PageResult<CourseDto>>> GetCoursesByTheme([FromRoute] int themeId, [FromQuery] int page = 0, [FromQuery] int pageSize = 10)
            => Ok(await courseIterator.GetCoursesByTheme(themeId, true, page, pageSize));

        [HttpGet("available-promocodes")]
        public async Task<ActionResult<PageResult<PromocodeDto>>> GetPromocodes([FromQuery] int page = 0, [FromQuery] int pageSize = 10)
            => Ok(await promocodeIterator.GetPromocodes(true, page, pageSize));

        [HttpGet("promocode/{promocodeId}")]
        public async Task<ActionResult<PromocodeDto>> GetPromocode([FromRoute] int promocodeId)
            => Ok(await promocodeIterator.GetPromocode(promocodeId, true));
    }
}
