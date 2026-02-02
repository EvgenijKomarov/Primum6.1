using CoreConnection.DTOs;
using CoreConnection.DTOs.Inputs;
using CoreConnection.Enums;
using Microsoft.EntityFrameworkCore;
using PrimumCore.Constants;
using PrimumCore.Extentions;
using PrimumCore.Models;

namespace PrimumCore.Services.Iterators
{
    public class TeacherIterator(IPrimumContext context)
    {
        public async Task<IEnumerable<TeacherProfileDto>> GetTeachers(bool isOnlyAvailable)
        {
            return await context.Set<User>()
                .Include(x => x.TeacherProfile)
                .Where(x => x.TeacherProfile != null)
                .WhereIf(isOnlyAvailable, AvailabilityExpressions.IsTeacherAvailable)
                .Select(x => new TeacherProfileDto
                {
                    DisplayName = x.DisplayName,
                    About = x.TeacherProfile.About,
                    UserId = x.Id,
                    IsAvailable = AvailabilityExpressions.IsTeacherAvailable.Compile()(x)
                })
                .ToArrayAsync();
        }

        public async Task<TeacherProfileDto> GetTeacher(int teacherId)
        {
            var user = await context.Set<User>()
                .Include(x => x.TeacherProfile)
                .Where(x => x.TeacherProfile != null)
                .Select(x => new TeacherProfileDto
                {
                    DisplayName = x.DisplayName,
                    About = x.TeacherProfile.About,
                    UserId = x.Id,
                    IsAvailable = AvailabilityExpressions.IsTeacherAvailable.Compile()(x)
                })
                .FirstOrDefaultAsync(x => x.UserId == teacherId);
            if (user is null) { throw new Exception("Teacher not found"); }

            return user;
        }
    }
}
