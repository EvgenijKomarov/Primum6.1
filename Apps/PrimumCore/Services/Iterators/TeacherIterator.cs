using CoreConnection.DTOs;
using CoreConnection.DTOs.Inputs;
using CoreDBModel.Constants;
using CoreDBModel.Models;
using Microsoft.EntityFrameworkCore;
using PrimumCore.Exceptions;
using PrimumCore.Extentions;
using System.Linq.Expressions;

namespace PrimumCore.Services.Iterators
{
    public class TeacherIterator(PrimumContext context)
    {
        private IQueryable<TeacherProfileDto> Teachers(bool isOnlyAvailable) => context
            .Set<User>()
            .Include(x => x.TeacherProfile)
            .Where(x => x.TeacherProfile != null)
            .WhereIf(isOnlyAvailable, AvailabilityExpressions.IsTeacherAvailable)
            .Select(x => new TeacherProfileDto
            {
                DisplayName = x.DisplayName,
                About = x.TeacherProfile.About,
                UserId = x.Id,
                IsAvailable = AvailabilityExpressions.IsTeacherAvailable.Compile()(x)
            });

        public async Task<IEnumerable<TeacherProfileDto>> GetTeachers(bool isOnlyAvailable)
        {
            return await Teachers(isOnlyAvailable).ToArrayAsync();
        }

        public async Task<TeacherProfileDto> GetTeacher(int teacherId, bool isOnlyAvailable)
        {
            return await Teachers(isOnlyAvailable)
                .FirstOrDefaultAsync(x => x.UserId == teacherId) ?? throw new NotFoundException("Teacher");
        }
    }
}
