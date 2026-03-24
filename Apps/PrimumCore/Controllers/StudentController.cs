using CoreConnection.DTOs;
using PrimumCore.Entities;
using CoreDBModel.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using PrimumCore.Services.Iterators;

namespace PrimumCore.Controllers
{
    [ApiController]
    [Route("api/student/{userId}")]
    [Tags("Student")]
    public class StudentController(
        StudentIterator studentIterator,
        StudentSheduleIterator sheduleIterator,
        LessonIterator lessonIterator,
        AbonementIterator abonementIterator,
        PromocodeIterator promocodeIterator
        ) : PrimumController
    {
        [HttpGet("profile")]
        public async Task<ActionResult<StudentProfileDto>> GetStudentProfile([FromRoute] int userId)
            => Ok(await studentIterator.GetStudentProfile(userId));

        [HttpGet("lessons")]
        public async Task<ActionResult<PageResult<LessonDto>>> GetLessons([FromRoute] int userId, [FromQuery] int page = 0, [FromQuery] int pageSize = 10) 
            => Ok(await lessonIterator.GetStudentLessons(userId, page, pageSize));

        [HttpGet("future-lessons")]
        public async Task<ActionResult<PageResult<LessonsByDateDto>>> GetFutureLessons([FromRoute] int userId, [FromQuery] int page = 0, [FromQuery] int pageSize = 10)
            => Ok(await lessonIterator.GetStudentFutureLessons(userId, page, pageSize));

        [HttpGet("lesson/{lessonId}")]
        public async Task<ActionResult<LessonDto>> GetLesson([FromRoute] int userId, [FromRoute] int lessonId)
            => Ok(await lessonIterator.GetStudentLesson(userId, lessonId));

        [HttpGet("abonements")]
        public async Task<ActionResult<PageResult<AbonementDto>>> GetAbonements([FromRoute] int userId, [FromQuery] int page = 0, [FromQuery] int pageSize = 10) 
            => Ok(await abonementIterator.GetStudentAbonements(userId, page, pageSize));

        [HttpGet("abonement/{abonementId}")]
        public async Task<ActionResult<AbonementDto>> GetAbonement([FromRoute] int userId, [FromRoute] int abonementId)
            => Ok(await abonementIterator.GetStudentAbonement(userId, abonementId));

        [HttpGet("shedules")]
        public async Task<ActionResult<PageResult<AbonementSheduleDto>>> GetShedules([FromRoute] int userId, [FromQuery] int page = 0, [FromQuery] int pageSize = 10)
            => Ok(await sheduleIterator.GetStudentShedules(userId, page, pageSize));

        [HttpGet("shedule/{sheduleId}")]
        public async Task<ActionResult<AbonementSheduleDto>> GetShedule([FromRoute] int userId, [FromRoute] int sheduleId)
            => Ok(await sheduleIterator.GetStudentShedule(userId, sheduleId));

        [HttpGet("abonement-shedules/{abonementId}")]
        public async Task<ActionResult<PageResult<AbonementSheduleDto>>> GetAbonementShedules(
            [FromRoute] int userId, 
            [FromRoute] int abonementId, 
            [FromQuery] int page = 0, 
            [FromQuery] int pageSize = 10)
            => Ok(await sheduleIterator.GetAbonementShedules(userId, page, pageSize));

        [HttpGet("abonement-lessons/{abonementId}")]
        public async Task<ActionResult<PageResult<LessonDto>>> GetAbonementLessons(
            [FromRoute] int userId, 
            [FromRoute] int abonementId, 
            [FromQuery] int page = 0, 
            [FromQuery] int pageSize = 10)
            => Ok(await lessonIterator.GetAbonementLessons(userId, true, page, pageSize));

        [HttpGet("promocodes")]
        public async Task<ActionResult<PageResult<PromocodeDto>>> GetStudentPromocodes(
            [FromRoute] int userId, 
            [FromQuery] int page = 0, 
            [FromQuery] int pageSize = 10)
            => Ok(await promocodeIterator.GetStudentPromocodes(userId, page, pageSize));

        [HttpGet("available-promocodes")]
        public async Task<ActionResult<PageResult<PromocodeDto>>> GetPromocodes([FromRoute] int userId, [FromQuery] int page = 0, [FromQuery] int pageSize = 10)
            => Ok(await promocodeIterator.GetPromocodes(true, page, pageSize));

        [HttpGet("promocode/{promocodeId}")]
        public async Task<ActionResult<PromocodeDto>> GetPromocode([FromRoute] int userId, [FromRoute] int promocodeId)
            => Ok(await promocodeIterator.GetPromocode(promocodeId, true));

        [HttpPatch("abonement-activate/{abonementId}")]
        public async Task<ActionResult<int>> ActivateAbonement([FromRoute] int userId, [FromRoute] int abonementId) 
            => Ok(await abonementIterator.AbonementChangeStatus(userId, abonementId, AbonementStatus.Active));

        [HttpPatch("abonement-freeze/{abonementId}")]
        public async Task<ActionResult<int>> FreezeAbonement([FromRoute] int userId, [FromRoute] int abonementId) 
            => Ok(await abonementIterator.AbonementChangeStatus(userId, abonementId, AbonementStatus.Freezed));

        [HttpPost("course-subscribe/{courseId}/{teacherSheduleId}")]
        public async Task<ActionResult<int>> SubscribeToCourse([FromRoute] int userId, [FromRoute] int courseId, [FromRoute] int teacherSheduleId)
            => Ok(await studentIterator.SubscribeToCourse(userId, courseId, teacherSheduleId));

        [HttpPost("buy-promocode/{promocodeId}")]
        public async Task<ActionResult<PromocodeDto>> BuyPromocode([FromRoute] int userId, [FromRoute] int promocodeId)
            => Ok(await promocodeIterator.BuyPromocode(userId, promocodeId));

        [HttpDelete("shedule/{abonementSheduleId}")]
        public async Task<ActionResult<int>> DeleteShedule([FromRoute] int userId, [FromRoute] int abonementSheduleId) 
            => Ok(await sheduleIterator.DeleteStudentShedule(userId, abonementSheduleId));

        [HttpDelete("abonement-delete/{abonementId}")]
        public async Task<ActionResult<int>> DeleteAbonement([FromRoute] int userId, [FromRoute] int abonementId)
            => Ok(await abonementIterator.AbonementChangeStatus(userId, abonementId, AbonementStatus.Deleted));
    }
}
