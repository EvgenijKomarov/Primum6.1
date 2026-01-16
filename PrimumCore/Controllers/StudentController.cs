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
        public async Task<IActionResult> GetLessons(int userId) => Ok(iterator.GetLessons(userId));

        [HttpGet("abonements")]
        public async Task<IActionResult> GetAbonements(int userId) => Ok(iterator.GetAbonements(userId));
    }
}
