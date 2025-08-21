using DTO;
using Microsoft.AspNetCore.Mvc;
using PrimumCore.Models;
using PrimumPlatformModel.Models.Enums;
using System.Net.Http;

namespace PrimumCore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController(IPrimumContext context) : PrimumController
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudent(int id)
        {
            var user = context.Set<User>().FirstOrDefault(x => x.Id == id);
            if (user is null) { return NotFound(); }
            if (user.StudentProfile is null) { return NotFound(); }

            return Ok(new StudentDTO
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Patronymic = user.Patronymic,
                Password = user.Password,
                IsApproved = user.StudentProfile.ApproveStatus == ApproveStatus.Approved
            });
        }

        [HttpPost]
        public async Task<IActionResult> RegStudent(StudentDTO dto)
        {
            var user = new User
            {
                Name = dto.Name,
                Surname = dto.Surname,
                Patronymic = dto.Patronymic,
                Password = dto.Password,
                StudentProfile = new StudentProfile()
            };

            context.Set<User>().Add(user);
            await context.SaveChangesAsync();

            return Ok(user.Id);
        }
    }
}
