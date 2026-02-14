using CoreConnection.DTOs;
using Microsoft.AspNetCore.Mvc;
using PrimumCore.Services.Iterators;
using Swashbuckle.AspNetCore.Annotations;

namespace PrimumCore.Controllers
{
    [ApiController]
    [Route("api/public")]
    [Tags("Public")]
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
        public async Task<ActionResult<int>> Login([FromQuery] string mailAdress, [FromQuery] string password)
            => Ok(await userIterator.Login(mailAdress, password));

        [HttpPost("register")]
        public async Task<ActionResult<int>> RegUser([FromBody] RegistrationInputDto dto)
            => Ok(await userIterator.RegUser(dto));

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<UserDtoLite>> GetUser([FromRoute] int userId) => Ok(await userIterator.GetLiteUser(userId, true));

        [HttpGet("teacher/{teacherId}")]
        public async Task<ActionResult<TeacherProfileDto>> GetTeacher([FromRoute] int teacherId) => Ok(await teacherIterator.GetTeacher(teacherId, true));

        [HttpGet("teachers")]
        public async Task<ActionResult<IEnumerable<TeacherProfileDto>>> GetTeachers() => Ok(await teacherIterator.GetTeachers(true));

        [HttpGet("courses-by-teacher/{teacherId}")]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetCoursesByTeacher([FromRoute] int teacherId) 
            => Ok(await courseIterator.GetCoursesByTeacher(teacherId, true));

        [HttpGet("teacher-shedules/{teacherId}")]
        public async Task<ActionResult<IEnumerable<TeacherSheduleDto>>> GetTeacherShedules([FromRoute] int teacherId)
            => Ok(await sheduleIterator.GetTeacherShedules(teacherId, true));

        [HttpGet("themes")]
        public async Task<ActionResult<IEnumerable<CourseThemeDto>>> GetThemes()
            => Ok(await themeIterator.GetThemes(true));

        [HttpGet("theme/{themeId}")]
        public async Task<ActionResult<CourseThemeDto>> GetTheme([FromRoute] int themeId)
            => Ok(await themeIterator.GetTheme(themeId, true));

        [HttpGet("course/{courseId}")]
        public async Task<ActionResult<CourseDto>> GetCourse([FromRoute] int courseId)
            => Ok(await courseIterator.GetCourse(courseId, true));

        [HttpGet("courses")]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetCourses()
            => Ok(await courseIterator.GetCourses(true));

        [HttpGet("courses-by-theme/{themeId}")]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetCoursesByTheme([FromRoute] int themeId)
            => Ok(await courseIterator.GetCoursesByTheme(themeId, true));

        [HttpGet("available-promocodes")]
        public async Task<ActionResult<IEnumerable<PromocodeDto>>> GetPromocodes()
            => Ok(await promocodeIterator.GetPromocodes(true));

        [HttpGet("promocode/{promocodeId}")]
        public async Task<ActionResult<PromocodeDto>> GetPromocode([FromRoute] int promocodeId)
            => Ok(await promocodeIterator.GetPromocode(promocodeId, true));
    }
}
