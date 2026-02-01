using Microsoft.AspNetCore.Mvc;
using PrimumCore.Services.Iterators;

namespace PrimumCore.Controllers
{
    [ApiController]
    [Route("api/student/{userId}")]
    public class StudentController(
        StudentIterator studentIterator,
        SheduleIterator sheduleIterator,
        LessonIterator lessonIterator,
        AbonementIterator abonementIterator,
        PromocodeIterator promocodeIterator
        ) : PrimumController
    {
        [HttpGet("profile")]
        public async Task<IActionResult> GetStudentProfile([FromRoute] int userId)
            => Ok(await studentIterator.GetStudentProfile(userId));

        [HttpGet("lessons")]
        public async Task<IActionResult> GetLessons([FromRoute] int userId) 
            => Ok(await lessonIterator.GetStudentLessons(userId));

        [HttpGet("abonements")]
        public async Task<IActionResult> GetAbonements([FromRoute] int userId) 
            => Ok(await abonementIterator.GetStudentAbonements(userId));

        [HttpGet("shedules")]
        public async Task<IActionResult> GetShedules([FromRoute] int userId)
            => Ok(await sheduleIterator.GetStudentShedules(userId));

        [HttpGet("abonement/{abonementId}/shedules")]
        public async Task<IActionResult> GetAbonementShedules([FromRoute] int userId, [FromRoute] int abonementId)
            => Ok(await sheduleIterator.GetAbonementShedules(userId));

        [HttpGet("abonement/{abonementId}/lessons")]
        public async Task<IActionResult> GetAbonementLessons([FromRoute] int userId, [FromRoute] int abonementId)
            => Ok(await lessonIterator.GetAbonementLessons(userId, true));

        [HttpGet("promocodes")]
        public async Task<IActionResult> GetStudentPromocodes([FromRoute] int userId)
            => Ok(await promocodeIterator.GetStudentPromocodes(userId));

        [HttpGet("available-promocodes")]
        public async Task<IActionResult> GetPromocodes()
            => Ok(await promocodeIterator.GetPromocodes(true));

        [HttpGet("promocode/{promocodeId}")]
        public async Task<IActionResult> GetPromocode([FromRoute] int promocodeId)
            => Ok(await promocodeIterator.GetPromocode(promocodeId, true));

        [HttpPatch("abonement/activate")]
        public async Task<IActionResult> ActivateAbonement([FromRoute] int userId, [FromQuery] int abonementId) 
            => Ok(await abonementIterator.ActivateAbonement(userId, abonementId));

        [HttpPatch("abonement/freeze")]
        public async Task<IActionResult> FreezeAbonement([FromRoute] int userId, [FromQuery] int abonementId) 
            => Ok(await abonementIterator.FreezeAbonement(userId, abonementId));

        [HttpDelete("abonement/delete")]
        public async Task<IActionResult> DeleteAbonement([FromRoute] int userId, [FromQuery] int abonementId) 
            => Ok(await abonementIterator.DeleteAbonement(userId, abonementId));

        [HttpPost("course/subscribe")]
        public async Task<IActionResult> SubscribeToCourse([FromRoute] int userId, [FromQuery] int courseId, [FromQuery] int teacherSheduleId)
            => Ok(await studentIterator.SubscribeToCourse(userId, courseId, teacherSheduleId));

        [HttpPost("buy-promocode")]
        public async Task<IActionResult> BuyPromocode([FromRoute] int userId, [FromQuery] int promocodeId)
            => Ok(await promocodeIterator.BuyPromocode(userId, promocodeId));

        [HttpDelete("shedule/delete")]
        public async Task<IActionResult> DeleteShedule([FromRoute] int userId, [FromQuery] int abonementSheduleId) 
            => Ok(await sheduleIterator.DeleteStudentShedule(userId, abonementSheduleId));
    }
}
