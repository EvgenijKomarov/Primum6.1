using DTO;
using Microsoft.AspNetCore.Mvc;
using PrimumCore.Models;
using PrimumPlatformModel.Models.Enums;
using System.Net.Http;

namespace PrimumCore.Controllers
{
    [ApiController]
    [Route("api/student/[controller]")]
    public class StudentController(DbContextFactory<IPrimumContext> _contextFactory) : PrimumController
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudent(int id)
        {
            using var context = _contextFactory.CreateDbContext();

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
            using var context = _contextFactory.CreateDbContext();
            var user = new User
            {
                Name = dto.Name,
                Surname = dto.Surname,
                Patronymic = dto.Patronymic,
                Password = dto.Password,
                StudentProfile = new StudentProfile()
            };

            context.Set<User>().Add(user);
            await _contextFactory.SafeSaveChangesAsync(context);

            return Ok(user.Id);
        }
    }
}
