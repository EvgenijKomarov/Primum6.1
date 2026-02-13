using CoreConnection.DTOs;
using Microsoft.AspNetCore.Mvc;
using PrimumCore.Services.Iterators;

namespace PrimumCore.Controllers
{
    [ApiController]
    [Route("api/student/{userId}")]
    [Tags("Student")]
    public class StudentController(
        StudentIterator studentIterator,
        SheduleIterator sheduleIterator,
        LessonIterator lessonIterator,
        AbonementIterator abonementIterator,
        PromocodeIterator promocodeIterator
        ) : PrimumController
    {
        [HttpGet("profile")]
        public async Task<ActionResult<StudentProfileDto>> GetStudentProfile([FromRoute] int userId)
            => Ok(await studentIterator.GetStudentProfile(userId));

        [HttpGet("lessons")]
        public async Task<ActionResult<IEnumerable<LessonDto>>> GetLessons([FromRoute] int userId) 
            => Ok(await lessonIterator.GetStudentLessons(userId));

        [HttpGet("lesson/{lessonId}")]
        public async Task<ActionResult<LessonDto>> GetLesson([FromRoute] int userId, [FromRoute] int lessonId)
            => Ok(await lessonIterator.GetStudentLesson(userId, lessonId));

        [HttpGet("abonements")]
        public async Task<ActionResult<IEnumerable<AbonementDto>>> GetAbonements([FromRoute] int userId) 
            => Ok(await abonementIterator.GetStudentAbonements(userId));

        [HttpGet("abonement/{abonementId}")]
        public async Task<ActionResult<AbonementDto>> GetAbonement([FromRoute] int userId, [FromRoute] int abonementId)
            => Ok(await abonementIterator.GetStudentAbonement(userId, abonementId));

        [HttpGet("shedules")]
        public async Task<ActionResult<IEnumerable<StudentSheduleDto>>> GetShedules([FromRoute] int userId)
            => Ok(await sheduleIterator.GetStudentShedules(userId));

        [HttpGet("shedule/{sheduleId}")]
        public async Task<ActionResult<StudentSheduleDto>> GetShedule([FromRoute] int userId, [FromRoute] int sheduleId)
            => Ok(await sheduleIterator.GetStudentShedule(userId, sheduleId));

        [HttpGet("abonement/{abonementId}/shedules")]
        public async Task<ActionResult<IEnumerable<StudentSheduleDto>>> GetAbonementShedules([FromRoute] int userId, [FromRoute] int abonementId)
            => Ok(await sheduleIterator.GetAbonementShedules(userId));

        [HttpGet("abonement/{abonementId}/lessons")]
        public async Task<ActionResult<IEnumerable<LessonDto>>> GetAbonementLessons([FromRoute] int userId, [FromRoute] int abonementId)
            => Ok(await lessonIterator.GetAbonementLessons(userId, true));

        [HttpGet("promocodes")]
        public async Task<ActionResult<IEnumerable<PromocodeDto>>> GetStudentPromocodes([FromRoute] int userId)
            => Ok(await promocodeIterator.GetStudentPromocodes(userId));

        [HttpGet("available-promocodes")]
        public async Task<ActionResult<IEnumerable<PromocodeDto>>> GetPromocodes()
            => Ok(await promocodeIterator.GetPromocodes(true));

        [HttpGet("promocode/{promocodeId}")]
        public async Task<ActionResult<PromocodeDto>> GetPromocode([FromRoute] int promocodeId)
            => Ok(await promocodeIterator.GetPromocode(promocodeId, true));

        [HttpPatch("abonement/activate")]
        public async Task<ActionResult<int>> ActivateAbonement([FromRoute] int userId, [FromQuery] int abonementId) 
            => Ok(await abonementIterator.ActivateAbonement(userId, abonementId));

        [HttpPatch("abonement/freeze")]
        public async Task<ActionResult<int>> FreezeAbonement([FromRoute] int userId, [FromQuery] int abonementId) 
            => Ok(await abonementIterator.FreezeAbonement(userId, abonementId));

        [HttpPost("course/subscribe")]
        public async Task<ActionResult<int>> SubscribeToCourse([FromRoute] int userId, [FromQuery] int courseId, [FromQuery] int teacherSheduleId)
            => Ok(await studentIterator.SubscribeToCourse(userId, courseId, teacherSheduleId));

        [HttpPost("buy-promocode")]
        public async Task<ActionResult<PromocodeDto>> BuyPromocode([FromRoute] int userId, [FromQuery] int promocodeId)
            => Ok(await promocodeIterator.BuyPromocode(userId, promocodeId));

        [HttpDelete("shedule/delete")]
        public async Task<ActionResult<int>> DeleteShedule([FromRoute] int userId, [FromQuery] int abonementSheduleId) 
            => Ok(await sheduleIterator.DeleteStudentShedule(userId, abonementSheduleId));

        [HttpDelete("abonement/delete")]
        public async Task<ActionResult<int>> DeleteAbonement([FromRoute] int userId, [FromQuery] int abonementId)
            => Ok(await abonementIterator.DeleteAbonement(userId, abonementId));
    }
}
