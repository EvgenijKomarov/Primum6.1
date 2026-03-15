using CoreConnection.DTOs;
using CoreConnection.Entities;
using PrimumCore.Extentions;

namespace PrimumCore.Services.Iterators
{
    public class TeacherIterator(DatabaseIterator dbIterator)
    {
        public async Task<PageResult<TeacherProfileDto>> GetTeachers(bool isOnlyAvailable, int _page, int _pageSize)
        {
            return await dbIterator.Teachers(isOnlyAvailable).ToDto().ToPageResult(_page, _pageSize);
        }

        public async Task<TeacherProfileDto> GetTeacher(int teacherId, bool isOnlyAvailable)
        {
            return await dbIterator.Teachers(isOnlyAvailable).ToDto().One(x => x.UserId == teacherId);
        }
    }
}
