using Microsoft.AspNetCore.Mvc;
using PrimumCore.Services;

namespace PrimumCore.Controllers
{
    [ApiController]
    [Route("api/student/{userId}")]
    public class StudentController(StudentIterator iterator) : PrimumController
    {
        [HttpGet("lessons")]
        public async Task<IActionResult> GetLessons([FromRoute] int userId) => Ok(await iterator.GetLessons(userId));

        [HttpGet("abonements")]
        public async Task<IActionResult> GetAbonements([FromRoute] int userId) => Ok(await iterator.GetAbonements(userId));

        [HttpGet("shedules")]
        public async Task<IActionResult> GetShedules([FromRoute] int userId) => Ok(await iterator.GetShedules(userId));

        [HttpPatch("abonement/activate")]
        public async Task<IActionResult> ActivateAbonement([FromRoute] int userId, [FromQuery] int abonementId) => Ok(await iterator.ActivateAbonement(userId, abonementId));

        [HttpPatch("abonement/freeze")]
        public async Task<IActionResult> FreezeAbonement([FromRoute] int userId, [FromQuery] int abonementId) => Ok(await iterator.FreezeAbonement(userId, abonementId));

        [HttpDelete("abonement/delete")]
        public async Task<IActionResult> DeleteAbonement([FromRoute] int userId, [FromQuery] int abonementId) => Ok(await iterator.DeleteAbonement(userId, abonementId));

        [HttpPost("course/subscribe")]
        public async Task<IActionResult> SubscribeToCourse([FromRoute] int userId, [FromQuery] int courseId, [FromQuery] int teacherSheduleId)
            => Ok(await iterator.SubscribeToCourse(userId, courseId, teacherSheduleId));

        [HttpDelete("shedule/delete")]
        public async Task<IActionResult> DeleteShedule([FromRoute] int userId, [FromQuery] int abonementSheduleId) => Ok(await iterator.DeleteShedule(userId, abonementSheduleId));

        [HttpGet("available-teacher-shedules")]
        public async Task<IActionResult> GetAvailableTeacherShedules([FromRoute] int userId, [FromQuery] int teacherId)
            => Ok(await iterator.GetAvailableTeacherShedules(userId, teacherId));
    }
}
