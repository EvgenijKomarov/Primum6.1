using CoreConnection.DTOs;
using PrimumCore.Entities;
using PrimumCore.Extentions;

namespace PrimumCore.Services.Iterators
{
    public class TeacherIterator(DatabaseIterator dbIterator)
    {
        public async Task<PageResult<TeacherProfileDto>> GetTeachers(bool isOnlyAvailable, bool isConfidential, int _page, int _pageSize)
        {
            return await dbIterator.Teachers(isOnlyAvailable).ToDto(isConfidential).ToPageResult(_page, _pageSize);
        }

        public async Task<TeacherProfileDto> GetTeacher(int teacherId, bool isOnlyAvailable, bool isConfidential)
        {
            return await dbIterator.Teachers(isOnlyAvailable).ToDto(isConfidential).One(x => x.UserId == teacherId);
        }
    }
}
