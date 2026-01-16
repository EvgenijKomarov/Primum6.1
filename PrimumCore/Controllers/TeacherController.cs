using CoreConnection.DTOs;
using DTO.DTOs;
using DTO.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrimumCore.Models;
using PrimumCore.Services;

namespace PrimumCore.Controllers
{
    [ApiController]
    [Route("api/teacher/{userId}")]
    public class TeacherController(TeacherIterator iterator) : PrimumController
    {
        [HttpGet("lessons")]
        public async Task<IActionResult> GetLessons(int userId) => Ok(iterator.GetLessons(userId));

        [HttpGet("courses")]
        public async Task<IActionResult> GetCourses(int userId) => Ok(iterator.GetCourses(userId));

        [HttpGet("shedules")]
        public async Task<IActionResult> GetShedules(int userId) => Ok(iterator.GetShedules(userId));
    }
}
