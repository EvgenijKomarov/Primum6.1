using CoreConnection.DTOs;
using Microsoft.EntityFrameworkCore;
using PrimumCore.Extentions;
using PrimumCore.Models;

namespace PrimumCore.Services.Iterators
{
    public class ThemeIterator(IPrimumContext context)
    {
        public async Task<IEnumerable<CourseThemeDto>> GetThemes(bool isOnlyAvailable)
        {
            return await context.Set<CourseTheme>()
                .Include(x => x.Courses)
                .ThenInclude(x => x.Teacher)
                .ThenInclude(x => x.User)
                .WhereIf(isOnlyAvailable, x => x.IsActive)
                .Select(x => new CourseThemeDto
                {
                    CourseThemeId = x.CourseThemeId,
                    ThemeName = x.ThemeName,
                    IsActive = x.IsActive
                })
                .ToArrayAsync();
        }

        public async Task<CourseThemeDto> GetTheme(int themeId, bool isOnlyAvailable)
        {
            var theme = (await GetThemes(isOnlyAvailable))
                .FirstOrDefault(x => x.CourseThemeId == themeId);
            if (theme is null) { throw new Exception("Theme not found"); }
            return theme;
        }
    }
}
