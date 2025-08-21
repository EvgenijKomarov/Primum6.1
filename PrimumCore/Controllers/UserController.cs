using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrimumCore.Models;

namespace PrimumCore.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController(IPrimumContext context) : PrimumController
    {
        [HttpGet("platformAuthorization/{id}")]
        public async Task<IActionResult> PlatformAuthorize(int id)
        {
            var user = context.Set<User>()
                .Include(u => u.StudentProfile)
                .Include(u => u.TeacherProfile)
                .Include(u => u.AdminProfile)
                .FirstOrDefault(x => x.Id == id);
            if (user is null) { return NotFound(); }

            return Ok(new 
            {
                isStudent = user.StudentProfile is not null,
                isTeacher = user.TeacherProfile is not null,
                isAdmin = user.AdminProfile is not null
            });
        }
    }
}
