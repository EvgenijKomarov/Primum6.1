using CoreConnection.DTOs;
using PrimumCore.Entities;
using PrimumCore.Extentions;

namespace PrimumCore.Services.Iterators
{
    public class RanksIterator(DatabaseIterator iterator)
    {
        public async Task<PageResult<CourseRankDto>> GetCourseRanks(int page, int pageSize)
        {
            return await iterator.CourseRanks().OrderBy(x => x.Id).ToDto().ToPageResult(page, pageSize);
        }

        public async Task<PageResult<TeacherRankDto>> GetTeacherRanks(int page, int pageSize)
        {
            return await iterator.TeacherRanks().OrderBy(x => x.Id).ToDto().ToPageResult(page, pageSize);
        }

        public async Task<PageResult<StudentRankDto>> GetStudentRanks(int page, int pageSize)
        {
            return await iterator.StudentRanks().OrderBy(x => x.Id).ToDto().ToPageResult(page, pageSize);
        }
    }
}
