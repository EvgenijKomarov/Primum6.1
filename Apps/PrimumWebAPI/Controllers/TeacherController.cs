using CoreConnection;
using CoreConnection.DTOs;
using CoreConnection.DTOs.Inputs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrimumWebAPI.Extensions;

namespace PrimumWebAPI.Controllers
{
    [ApiController]
    [Route("api/teacher")]
    [Tags("Teacher")]
    [Authorize]
    public class TeacherController(TeacherClient client): DefaultController
    {
        [HttpGet("profile")]
        public async Task<ActionResult<TeacherProfileDto>> GetTeacherProfile()
            => Ok(await client.ProfileAsync(User.GetUserId()));

        [HttpGet("lessons")]
        public async Task<ActionResult<IEnumerable<LessonDto>>> GetLessons()
            => Ok(await client.LessonsAsync(User.GetUserId()));

        [HttpGet("future-lessons")]
        public async Task<ActionResult<IEnumerable<LessonDto>>> GetFututreLessons()
            => Ok(await client.FutureLessonsAsync(User.GetUserId()));

        [HttpGet("lesson/{lessonId}")]
        public async Task<ActionResult<LessonDto>> GetLesson([FromRoute] int lessonId)
            => Ok(await client.LessonAsync(User.GetUserId(), lessonId));

        [HttpGet("courses")]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetCourses()
            => Ok(await client.CoursesAsync(User.GetUserId()));

        [HttpGet("shedules")]
        public async Task<ActionResult<IEnumerable<TeacherSheduleDto>>> GetShedules()
            => Ok(await client.ShedulesAsync(User.GetUserId()));

        [HttpGet("shedule/{sheduleId}")]
        public async Task<ActionResult<TeacherSheduleDto>> GetShedule([FromRoute] int sheduleId)
            => Ok(await client.SheduleAsync(User.GetUserId(), sheduleId));

        [HttpGet("abonements")]
        public async Task<ActionResult<IEnumerable<AbonementDto>>> GetAbonements()
            => Ok(await client.AbonementsAsync(User.GetUserId()));

        [HttpGet("abonement/{abonementId}")]
        public async Task<ActionResult<AbonementDto>> GetAbonement([FromRoute] int abonementId)
            => Ok(await client.AbonementAsync(User.GetUserId(), abonementId));

        [HttpGet("abonement-shedules/{abonementId}")]
        public async Task<ActionResult<IEnumerable<StudentSheduleDto>>> GetAbonementShedules([FromRoute] int abonementId)
            => Ok(await client.AbonementShedulesAsync(User.GetUserId(), abonementId));

        [HttpGet("abonement-lessons/{abonementId}")]
        public async Task<ActionResult<IEnumerable<LessonDto>>> GetAbonementLessons([FromRoute] int abonementId)
            => Ok(await client.AbonementLessonsAsync(User.GetUserId(), abonementId));

        [HttpPut("course-edit/{courseId}")]
        public async Task<ActionResult<int>> EditCourse([FromRoute] int courseId, [FromBody] CourseInputDto courseDto = null!)
            => Ok(await client.CourseEditAsync(User.GetUserId(), courseId, courseDto));

        [HttpPost("course-create")]
        public async Task<ActionResult<int>> CreateCourse([FromBody] CourseInputDto courseDto)
            => Ok(await client.CourseCreateAsync(User.GetUserId(), courseDto));

        [HttpPatch("course-activate/{courseId}")]
        public async Task<ActionResult<int>> ActivateCourse([FromRoute] int courseId)
            => Ok(await client.CourseActivateAsync(User.GetUserId(), courseId));

        [HttpPatch("course-deactivate/{courseId}")]
        public async Task<ActionResult<int>> DeactivateCourse([FromRoute] int courseId)
            => Ok(await client.CourseDeactivateAsync(User.GetUserId(), courseId));

        [HttpPost("shedule-create")]
        public async Task<ActionResult<int>> CreateShedule([FromBody] TeacherSheduleInputDto sheduleDto = null!)
            => Ok(await client.SheduleCreateAsync(User.GetUserId(), sheduleDto));

        [HttpPost("lesson-grade/{lessonId}")]
        public async Task<ActionResult<int>> GradeLesson([FromRoute] int lessonId, [FromBody] GradingInputDto gradingDto = null!)
            => Ok(await client.LessonGradeAsync(User.GetUserId(), lessonId, gradingDto));

        [HttpDelete("shedule-delete/{sheduleId}")]
        public async Task<ActionResult<int>> DeleteShedule([FromRoute] int sheduleId)
            => Ok(await client.SheduleDeleteAsync(User.GetUserId(), sheduleId));
    }
}
