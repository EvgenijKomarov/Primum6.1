using CoreConnection;
using CoreConnection.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrimumWebAPI.Extensions;
using System.Runtime.ConstrainedExecution;

namespace PrimumWebAPI.Controllers
{
    [ApiController]
    [Route("api/student")]
    [Tags("Student")]
    [Authorize]
    public class StudentController(StudentClient client): DefaultController
    {
        [HttpGet("profile")]
        public async Task<ActionResult<StudentProfileDto>> GetStudentProfile()
            => Ok(await client.ProfileAsync(User.GetUserId()));

        [HttpGet("lessons")]
        public async Task<ActionResult<IEnumerable<LessonDto>>> GetLessons()
            => Ok(await client.LessonsAsync(User.GetUserId()));

        [HttpGet("lesson/{lessonId}")]
        public async Task<ActionResult<LessonDto>> GetLesson([FromRoute] int lessonId)
            => Ok(await client.LessonAsync(User.GetUserId(), lessonId));

        [HttpGet("abonements")]
        public async Task<ActionResult<IEnumerable<AbonementDto>>> GetAbonements()
            => Ok(await client.AbonementsAsync(User.GetUserId()));

        [HttpGet("abonement/{abonementId}")]
        public async Task<ActionResult<AbonementDto>> GetAbonement([FromRoute] int abonementId)
            => Ok(await client.AbonementAsync(User.GetUserId(), abonementId));

        [HttpGet("shedules")]
        public async Task<ActionResult<IEnumerable<StudentSheduleDto>>> GetShedules()
            => Ok(await client.ShedulesAsync(User.GetUserId()));

        [HttpGet("shedule/{sheduleId}")]
        public async Task<ActionResult<StudentSheduleDto>> GetShedule([FromRoute] int sheduleId)
            => Ok(await client.SheduleGetAsync(User.GetUserId(), sheduleId));

        [HttpGet("abonement-shedules/{abonementId}")]
        public async Task<ActionResult<IEnumerable<StudentSheduleDto>>> GetAbonementShedules([FromRoute] int abonementId)
            => Ok(await client.AbonementShedulesAsync(User.GetUserId(), abonementId));

        [HttpGet("abonement-lessons/{abonementId}")]
        public async Task<ActionResult<IEnumerable<LessonDto>>> GetAbonementLessons([FromRoute] int abonementId)
            => Ok(await client.AbonementLessonsAsync(User.GetUserId(), abonementId));

        [HttpGet("promocodes")]
        public async Task<ActionResult<IEnumerable<PromocodeDto>>> GetStudentPromocodes()
            => Ok(await client.PromocodesAsync(User.GetUserId()));

        [HttpGet("available-promocodes")]
        public async Task<ActionResult<IEnumerable<PromocodeDto>>> GetPromocodes()
            => Ok(await client.AvailablePromocodesAsync(User.GetUserId()));

        [HttpGet("promocode/{promocodeId}")]
        public async Task<ActionResult<PromocodeDto>> GetPromocode([FromRoute] int promocodeId)
            => Ok(await client.PromocodeAsync(promocodeId, User.GetUserId()));

        [HttpPatch("abonement-activate")]
        public async Task<ActionResult<int>> ActivateAbonement([FromQuery] int abonementId)
            => Ok(await client.AbonementActivateAsync(User.GetUserId(), abonementId));

        [HttpPatch("abonement-freeze")]
        public async Task<ActionResult<int>> FreezeAbonement([FromQuery] int abonementId)
            => Ok(await client.AbonementFreezeAsync(User.GetUserId(), abonementId));

        [HttpPost("course-subscribe")]
        public async Task<ActionResult<int>> SubscribeToCourse([FromQuery] int courseId, [FromQuery] int teacherSheduleId)
            => Ok(await client.CourseSubscribeAsync(User.GetUserId(), courseId, teacherSheduleId));

        [HttpPost("buy-promocode")]
        public async Task<ActionResult<PromocodeDto>> BuyPromocode([FromQuery] int promocodeId)
            => Ok(await client.BuyPromocodeAsync(User.GetUserId(), promocodeId));

        [HttpDelete("shedule-delete")]
        public async Task<ActionResult<int>> DeleteShedule([FromQuery] int abonementSheduleId)
            => Ok(await client.SheduleDeleteAsync(User.GetUserId(), abonementSheduleId));

        [HttpDelete("abonement-delete")]
        public async Task<ActionResult<int>> DeleteAbonement([FromQuery] int abonementId)
            => Ok(await client.AbonementDeleteAsync(User.GetUserId(), abonementId));
    }
}
