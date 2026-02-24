using CoreConnection.DTOs;
using CoreConnection.DTOs.Inputs;
using CoreDBModel.Constants;
using CoreDBModel.Models;
using Microsoft.EntityFrameworkCore;
using PrimumCore.Exceptions;
using PrimumCore.Extentions;

namespace PrimumCore.Services.Iterators
{
    public class TeacherIterator(PrimumContext context)
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

        public async Task<TeacherProfileDto> GetTeacher(int teacherId, bool isOnlyAvailable)
        {
            var user = (await GetTeachers(isOnlyAvailable))
                .FirstOrDefault(x => x.UserId == teacherId);
            if (user is null) { throw new NotFoundException("Teacher"); }

            return user;
        }
    }
}
