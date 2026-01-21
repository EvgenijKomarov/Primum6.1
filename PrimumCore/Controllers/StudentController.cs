using Microsoft.AspNetCore.Mvc;
using PrimumCore.Services.Iterators;

namespace PrimumCore.Controllers
{
    [ApiController]
    [Route("api/student/{userId}")]
    public class StudentController(
        StudentIterator studentIterator,
        CommonIterator commonIterator) : PrimumController
    {
        [HttpGet("lessons")]
        public async Task<IActionResult> GetLessons([FromRoute] int userId) 
            => Ok(await studentIterator.GetLessons(userId));

        [HttpGet("abonements")]
        public async Task<IActionResult> GetAbonements([FromRoute] int userId) 
            => Ok(await studentIterator.GetAbonements(userId));

        [HttpGet("shedules")]
        public async Task<IActionResult> GetShedules([FromRoute] int userId)
            => Ok(await studentIterator.GetShedules(userId));

        [HttpGet("abonement/{abonementId}/shedules")]
        public async Task<IActionResult> GetAbonementShedules([FromRoute] int userId, [FromRoute] int abonementId)
            => Ok(await commonIterator.GetAbonementShedules(userId));

        [HttpGet("abonement/{abonementId}/lessons")]
        public async Task<IActionResult> GetAbonementLessons([FromRoute] int userId, [FromRoute] int abonementId)
            => Ok(await commonIterator.GetAbonementLessons(userId, true));

        [HttpPatch("abonement/activate")]
        public async Task<IActionResult> ActivateAbonement([FromRoute] int userId, [FromQuery] int abonementId) 
            => Ok(await studentIterator.ActivateAbonement(userId, abonementId));

        [HttpPatch("abonement/freeze")]
        public async Task<IActionResult> FreezeAbonement([FromRoute] int userId, [FromQuery] int abonementId) 
            => Ok(await studentIterator.FreezeAbonement(userId, abonementId));

        [HttpDelete("abonement/delete")]
        public async Task<IActionResult> DeleteAbonement([FromRoute] int userId, [FromQuery] int abonementId) 
            => Ok(await studentIterator.DeleteAbonement(userId, abonementId));

        [HttpPost("course/subscribe")]
        public async Task<IActionResult> SubscribeToCourse([FromRoute] int userId, [FromQuery] int courseId, [FromQuery] int teacherSheduleId)
            => Ok(await studentIterator.SubscribeToCourse(userId, courseId, teacherSheduleId));

        [HttpDelete("shedule/delete")]
        public async Task<IActionResult> DeleteShedule([FromRoute] int userId, [FromQuery] int abonementSheduleId) 
            => Ok(await studentIterator.DeleteShedule(userId, abonementSheduleId));
    }
}
