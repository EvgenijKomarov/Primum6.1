using CoreConnection.DTOs;
using PrimumCore.Entities;
using PrimumCore.Extentions;

namespace PrimumCore.Services.Iterators
{
    public class RanksIterator(DatabaseIterator iterator)
    {
        public async Task<PageResult<CourseRankDto>> GetCourseRanks(int page, int pageSize)
        {
            return await iterator.CourseRanks().ToDto().ToPageResult(page, pageSize);
        }

        public async Task<PageResult<TeacherRankDto>> GetTeacherRanks(int page, int pageSize)
        {
            return await iterator.TeacherRanks().ToDto().ToPageResult(page, pageSize);
        }

        public async Task<PageResult<StudentRankDto>> GetStudentRanks(int page, int pageSize)
        {
            return await iterator.StudentRanks().ToDto().ToPageResult(page, pageSize);
        }
    }
}
