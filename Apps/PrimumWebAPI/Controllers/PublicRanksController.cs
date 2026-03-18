using CoreConnection;
using CoreConnection.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PrimumWebAPI.Controllers
{
    [Route("public/ranks")]
    [AllowAnonymous]
    public class PublicRanksController(PublicClient client) : DefaultController
    {
        /// <summary>
        /// Все ранги преподавателей
        /// </summary>
        /// <returns></returns>
        [HttpGet("teacher")]
        public async Task<ActionResult<TeacherRankDtoPageResult>> GetTeacherRank(
            [FromQuery] int page = 0,
            [FromQuery] int pageSize = 10)
            => Ok(await client.TeacherRanksAsync(page, pageSize));

        /// <summary>
        /// Все ранги учеников
        /// </summary>
        /// <returns></returns>
        [HttpGet("student")]
        public async Task<ActionResult<StudentRankDtoPageResult>> GetStudentRank(
            [FromQuery] int page = 0,
            [FromQuery] int pageSize = 10)
            => Ok(await client.StudentRanksAsync(page, pageSize));

        /// <summary>
        /// Все ранги курсов
        /// </summary>
        /// <returns></returns>
        [HttpGet("course")]
        public async Task<ActionResult<CourseRankDtoPageResult>> GetCourseRank(
            [FromQuery] int page = 0,
            [FromQuery] int pageSize = 10)
            => Ok(await client.CourseRanksAsync(page, pageSize));
    }
}
