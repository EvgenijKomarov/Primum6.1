using DTO;
using DTO.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrimumCore.Models;
using PrimumPlatformModel.Models.Enums;

namespace PrimumCore.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController(IPrimumContext context) : PrimumController
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = context.Set<User>()
                .Include(u => u.StudentProfile)
                .Include(u => u.TeacherProfile)
                .Include(u => u.AdminProfile)
                .FirstOrDefault(x => x.Id == id);
            if (user is null) { return NotFound(); }

            return Ok(new UserDTO
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Patronymic = user.Patronymic,
                Password = user.Password,
                IsApprovedStudent = user.StudentProfile is not null ? 
                    user.StudentProfile.ApproveStatus == ApproveStatus.Approved : null,
                IsApprovedTeacher = user.TeacherProfile is not null ? 
                    user.TeacherProfile.ApproveStatus == ApproveStatus.Approved : null,
                IsApprovedAdmin = user.AdminProfile is not null
            });
        }

        [HttpPost("reg-teacher")]
        public async Task<IActionResult> RegTeacher([FromBody] UserDTO dto, [FromBody] string about)
        {
            var user = new User
            {
                Name = dto.Name,
                Surname = dto.Surname,
                Patronymic = dto.Patronymic,
                Password = dto.Password,
                TeacherProfile = new TeacherProfile
                {
                    About = about
                }
            };

            context.Set<User>().Add(user);
            await context.SaveChangesAsync();

            return Ok(user.Id);
        }


        [HttpPost("reg-user")]
        public async Task<IActionResult> RegStudent([FromBody] UserDTO dto)
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
