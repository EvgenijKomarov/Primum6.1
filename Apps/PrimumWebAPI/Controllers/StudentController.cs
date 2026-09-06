using CoreConnection;
using CoreConnection.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PaymentServiceConnection;
using PrimumWebAPI.Extensions;
using System.Runtime.ConstrainedExecution;

namespace PrimumWebAPI.Controllers
{
    [Route("student")]
    [Authorize]
    public class StudentController(StudentClient client) : DefaultController
    {
        /// <summary>
        /// Профиль ученика, включая имя, количество монет и id пользователя
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<StudentProfileDto>> GetStudentProfile()
            => Ok(await client.ProfileAsync(User.GetUserId()));

        /// <summary>
        /// Подписаться на курс
        /// </summary>
        /// <param name="courseId">Id курса</param>
        /// <param name="teacherSheduleId">Id расписания преподавателя</param>
        /// <returns></returns>
        [HttpPost("subscribe/{courseId}/{teacherSheduleId}")]
        public async Task<ActionResult<int>> SubscribeToCourse([FromRoute] int courseId, [FromRoute] int teacherSheduleId)
            => Ok(await client.CourseSubscribeAsync(User.GetUserId(), courseId, teacherSheduleId));
    }
}
