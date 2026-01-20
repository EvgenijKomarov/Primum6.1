using CoreConnection.DTOs;
using CoreConnection.Enums;
using Microsoft.EntityFrameworkCore;
using PrimumCore.Models;

namespace PrimumCore.Services
{
    public class CommonIterator(IPrimumContext context)
    {
        public async Task<IEnumerable<CourseThemeDto>> GetThemes()
        {
            return await context.Set<CourseTheme>()
                .Include(x => x.Courses)
                .ThenInclude(x => x.Teacher)
                .ThenInclude(x => x.User)
                .Where(x => x.IsActive)
                .Select(x => new CourseThemeDto
                {
                    CourseThemeId = x.CourseThemeId,
                    ThemeName = x.ThemeName,
                    IsActive = x.IsActive,
                    Courses = x.Courses.Where(y => y.IsAvailable).Select(y => new CourseDto
                    {
                        CourseId = y.CourseId,
                        Name = y.Name,
                        TeacherName = y.Teacher.User.DisplayName,
                        CourseThemeName = x.ThemeName,
                        CourseThemeId = x.CourseThemeId,
                        TeacherId = y.TeacherId,
                        Price = y.Price,
                        MaxLessons = y.MaxLessons,
                        FreeLessons = y.FreeLessons,
                        TeacherAbout = y.Teacher.About,
                        ApproveStatus = (ApproveStatusDto)y.ApproveStatus,
                    })
                })
                .ToArrayAsync();
        }

        public async Task<CourseThemeDto> GetTheme(int themeId)
        {
            var theme = await context.Set<CourseTheme>()
                .Include(x => x.Courses)
                .ThenInclude(x => x.Teacher)
                .ThenInclude(x => x.User)
                .Where(x => x.IsActive)
                .Select(x => new CourseThemeDto
                {
                    CourseThemeId = x.CourseThemeId,
                    ThemeName = x.ThemeName,
                    IsActive = x.IsActive,
                    Courses = x.Courses.Where(y => y.IsAvailable).Select(y => new CourseDto
                    {
                        CourseId = y.CourseId,
                        Name = y.Name,
                        TeacherName = y.Teacher.User.DisplayName,
                        CourseThemeName = x.ThemeName,
                        CourseThemeId = x.CourseThemeId,
                        TeacherId = y.TeacherId,
                        Price = y.Price,
                        MaxLessons = y.MaxLessons,
                        FreeLessons = y.FreeLessons,
                        TeacherAbout = y.Teacher.About,
                        ApproveStatus = (ApproveStatusDto)y.ApproveStatus,
                    })
                })
                .FirstOrDefaultAsync(x => x.CourseThemeId == themeId);
            if (theme is null) { throw new Exception("Theme not found"); }
            return theme;
        }
    }
}
