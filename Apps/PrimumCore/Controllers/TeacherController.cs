using CoreConnection.DTOs;
using CoreConnection.DTOs.Inputs;
using Microsoft.AspNetCore.Mvc;
using PrimumCore.Services.Iterators;

namespace PrimumCore.Controllers
{
    [ApiController]
    [Route("api/teacher/{userId}")]
    [Tags("Teacher")]
    public class TeacherController(
        TeacherIterator teacherIterator,
        CourseIterator courseIterator,
        SheduleIterator sheduleIterator,
        ThemeIterator themeIterator,
        LessonIterator lessonIterator,
        AbonementIterator abonementIterator,
        GradingIterator gradingIterator) : PrimumController
    {
        [HttpGet("profile")]
        public async Task<ActionResult<TeacherProfileDto>> GetTeacherProfile([FromRoute] int userId)
            => Ok(await teacherIterator.GetTeacher(userId, false));

        [HttpGet("lessons")]
        public async Task<ActionResult<IEnumerable<LessonDto>>> GetLessons([FromRoute] int userId) 
            => Ok(await lessonIterator.GetTeacherLessons(userId));

        [HttpGet("future-lessons")]
        public async Task<ActionResult<IEnumerable<LessonDto>>> GetFutureLessons([FromRoute] int userId)
            => Ok(await lessonIterator.GetTeacherFutureLessons(userId));

        [HttpGet("lesson/{lessonId}")]
        public async Task<ActionResult<LessonDto>> GetLesson([FromRoute] int userId, [FromRoute] int lessonId)
            => Ok(await lessonIterator.GetTeacherLesson(userId, lessonId));

        [HttpGet("courses")]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetCourses([FromRoute] int userId) 
            => Ok(await courseIterator.GetCoursesByTeacher(userId, false));

        [HttpGet("course/{courseId}")]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetCourse([FromRoute] int userId, [FromRoute] int courseId)
            => Ok(await courseIterator.GetCourseByTeacher(userId, courseId, false));

        [HttpGet("shedules")]
        public async Task<ActionResult<IEnumerable<TeacherSheduleDto>>> GetShedules([FromRoute] int userId) 
            => Ok(await sheduleIterator.GetTeacherShedules(userId, false));

        [HttpGet("shedule/{sheduleId}")]
        public async Task<ActionResult<TeacherSheduleDto>> GetShedule([FromRoute] int userId, [FromRoute] int sheduleId)
            => Ok(await sheduleIterator.GetTeacherShedule(userId, sheduleId, false));

        [HttpGet("abonements")]
        public async Task<ActionResult<IEnumerable<AbonementDto>>> GetAbonements([FromRoute] int userId)
            => Ok(await abonementIterator.GetTeacherAbonements(userId));

        [HttpGet("abonement/{abonementId}")]
        public async Task<ActionResult<AbonementDto>> GetAbonement([FromRoute] int userId, [FromRoute] int abonementId)
            => Ok(await abonementIterator.GetTeacherAbonement(userId, abonementId));

        [HttpGet("abonement-shedules/{abonementId}")]
        public async Task<ActionResult<IEnumerable<StudentSheduleDto>>> GetAbonementShedules([FromRoute] int userId, [FromRoute] int abonementId)
            => Ok(await sheduleIterator.GetAbonementShedules(abonementId));

        [HttpGet("abonement-lessons/{abonementId}")]
        public async Task<ActionResult<IEnumerable<LessonDto>>> GetAbonementLessons([FromRoute] int userId, [FromRoute] int abonementId)
            => Ok(await lessonIterator.GetAbonementLessons(abonementId, false));

        [HttpPut("course-edit/{courseId}")]
        public async Task<ActionResult<int>> EditCourse([FromRoute] int userId, [FromRoute] int courseId, [FromBody] CourseInputDto courseDto = null!) 
            => Ok(await courseIterator.EditCourse(userId, courseId, courseDto));

        [HttpPost("course-create")]
        public async Task<ActionResult<int>> CreateCourse([FromRoute] int userId, [FromBody] CourseInputDto courseDto) 
            => Ok(await courseIterator.CreateCourse(userId, courseDto));

        [HttpPatch("course-activate/{courseId}")]
        public async Task<ActionResult<int>> ActivateCourse([FromRoute] int userId, [FromRoute] int courseId) 
            => Ok(await courseIterator.ActivateCourse(userId, courseId));

        [HttpPatch("course-deactivate/{courseId}")]
        public async Task<ActionResult<int>> DeactivateCourse([FromRoute] int userId, [FromRoute] int courseId) 
            => Ok(await courseIterator.DeactivateCourse(userId, courseId));

        [HttpPost("shedule-create")]
        public async Task<ActionResult<int>> CreateShedule([FromRoute] int userId, [FromBody] TeacherSheduleInputDto sheduleDto = null!) 
            => Ok(await sheduleIterator.CreateTeacherShedule(userId, sheduleDto));

        [HttpPost("lesson-grade/{lessonId}")]
        public async Task<ActionResult<int>> GradeLesson([FromRoute] int userId, [FromRoute] int lessonId, [FromBody] GradingInputDto gradingDto = null!)
            => Ok(await gradingIterator.GradeLesson(userId, lessonId, gradingDto));

        [HttpDelete("shedule-delete/{sheduleId}")]
        public async Task<ActionResult<int>> DeleteShedule([FromRoute] int userId, [FromRoute] int sheduleId) 
            => Ok(await sheduleIterator.DeleteTeacherShedule(userId, sheduleId));
    }
}
