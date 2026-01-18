using CoreConnection.DTOs;
using DTO.DTOs;
using DTO.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrimumCore.Models;
using PrimumCore.Services;
using PrimumPlatformModel.Models.Enums;
using System.Net.Http;

namespace PrimumCore.Controllers
{
    [ApiController]
    [Route("api/student/{userId}")]
    public class StudentController(StudentIterator iterator) : PrimumController
    {
        [HttpGet("lessons")]
        public async Task<IActionResult> GetLessons(int userId) => Ok(await iterator.GetLessons(userId));

        [HttpGet("abonements")]
        public async Task<IActionResult> GetAbonements(int userId) => Ok(await iterator.GetAbonements(userId));

        [HttpGet("shedules")]
        public async Task<IActionResult> GetShedules(int userId) => Ok(await iterator.GetShedules(userId));

        [HttpGet("abonement/activate")]
        public async Task<IActionResult> ActivateAbonement(int userId, [FromBody] int abonementId) => Ok(await iterator.ActivateAbonement(userId, abonementId));

        [HttpGet("abonement/freeze")]
        public async Task<IActionResult> FreezeAbonement(int userId, [FromBody] int abonementId) => Ok(await iterator.FreezeAbonement(userId, abonementId));

        [HttpGet("abonement/delete")]
        public async Task<IActionResult> DeleteAbonement(int userId, [FromBody] int abonementId) => Ok(await iterator.DeleteAbonement(userId, abonementId));

        [HttpGet("course/subscribe")]
        public async Task<IActionResult> SubscribeToCourse(int userId, [FromBody] int courseId, [FromBody] int teacherSheduleId)
            => Ok(await iterator.SubscribeToCourse(userId, courseId, teacherSheduleId));

        [HttpGet("shedule/delete")]
        public async Task<IActionResult> DeleteShedule(int userId, [FromBody] int abonementSheduleId) => Ok(await iterator.DeleteShedule(userId, abonementSheduleId));
    }
}
