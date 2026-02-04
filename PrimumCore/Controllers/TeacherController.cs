using CoreConnection.DTOs.Inputs;
using Microsoft.AspNetCore.Mvc;
using PrimumCore.Services.Iterators;

namespace PrimumCore.Controllers
{
    [ApiController]
    [Route("api/teacher/{userId}")]
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
        public async Task<IActionResult> GetTeacherProfile([FromRoute] int userId)
            => Ok(await teacherIterator.GetTeacher(userId, false));

        [HttpGet("lessons")]
        public async Task<IActionResult> GetLessons([FromRoute] int userId) 
            => Ok(await lessonIterator.GetTeacherLessons(userId));

        [HttpGet("courses")]
        public async Task<IActionResult> GetCourses([FromRoute] int userId) 
            => Ok(await courseIterator.GetCoursesByTeacher(userId, false));

        [HttpGet("shedules")]
        public async Task<IActionResult> GetShedules([FromRoute] int userId) 
            => Ok(await sheduleIterator.GetTeacherShedules(userId, false));

        [HttpGet("abonements")]
        public async Task<IActionResult> GetAbonements([FromRoute] int userId)
            => Ok(await abonementIterator.GetTeacherAbonements(userId));

        [HttpGet("abonement/{abonementId}/shedules")]
        public async Task<IActionResult> GetAbonementShedules([FromRoute] int userId, [FromRoute] int abonementId)
            => Ok(await sheduleIterator.GetAbonementShedules(abonementId));

        [HttpGet("abonement/{abonementId}/lessons")]
        public async Task<IActionResult> GetAbonementLessons([FromRoute] int userId, [FromRoute] int abonementId)
            => Ok(await lessonIterator.GetAbonementLessons(abonementId, false));

        [HttpPut("course/edit")]
        public async Task<IActionResult> EditCourse([FromRoute] int userId, [FromQuery] int courseId, [FromBody] CourseInputDto courseDto) 
            => Ok(await courseIterator.EditCourse(userId, courseId, courseDto));

        [HttpPost("course/create")]
        public async Task<IActionResult> CreateCourse([FromRoute] int userId, [FromBody] CourseInputDto courseDto) 
            => Ok(await courseIterator.CreateCourse(userId, courseDto));

        [HttpPatch("course/activate")]
        public async Task<IActionResult> ActivateCourse([FromRoute] int userId, [FromQuery] int courseId) 
            => Ok(await courseIterator.ActivateCourse(userId, courseId));

        [HttpPatch("course/deactivate")]
        public async Task<IActionResult> DeactivateCourse([FromRoute] int userId, [FromQuery] int courseId) 
            => Ok(await courseIterator.DeactivateCourse(userId, courseId));

        [HttpPost("shedule/create")]
        public async Task<IActionResult> CreateShedule([FromRoute] int userId, [FromBody] TeacherSheduleInputDto sheduleDto) 
            => Ok(await sheduleIterator.CreateTeacherShedule(userId, sheduleDto));

        [HttpPost("lessons/grade")]
        public async Task<IActionResult> GradeLesson([FromRoute] int userId, [FromQuery] int lessonId, [FromBody] GradingInputDto gradingDto)
            => Ok(await gradingIterator.GradeLesson(userId, lessonId, gradingDto));

        [HttpDelete("shedule/delete")]
        public async Task<IActionResult> DeleteShedule([FromRoute] int userId, [FromQuery] int sheduleId) 
            => Ok(await sheduleIterator.DeleteTeacherShedule(userId, sheduleId));
    }
}
