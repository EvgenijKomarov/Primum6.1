using CoreConnection.DTOs;
using CoreConnection.DTOs.Inputs;
using CoreConnection.Entities;
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
        private IQueryable<User> Teachers(bool isOnlyAvailable) => context
            .Set<User>()
            .Include(x => x.TeacherProfile)
            .Where(x => x.TeacherProfile != null)
            .WhereIf(isOnlyAvailable, AvailabilityExpressions.IsTeacherAvailable);

        public async Task<PageResult<TeacherProfileDto>> GetTeachers(bool isOnlyAvailable, int _page, int _pageSize)
        {
            return await Teachers(isOnlyAvailable).Select(x => x.TeacherProfile!).ToDto().ToPageResult(_page, _pageSize);
        }

        public async Task<TeacherProfileDto> GetTeacher(int teacherId, bool isOnlyAvailable)
        {
            return await Teachers(isOnlyAvailable).Select(x => x.TeacherProfile!).ToDto().One(x => x.UserId == teacherId);
        }
    }
}
