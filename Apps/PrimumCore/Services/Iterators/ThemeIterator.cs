using CoreConnection.DTOs;
using CoreConnection.DTOs.Inputs;
using CoreDBModel.Constants;
using CoreDBModel.Models;
using CoreDBModel.Models.Enums;
using Microsoft.EntityFrameworkCore;
using PrimumCore.Exceptions;
using PrimumCore.Extentions;
using PrimumCore.Services.Utilities;
using System.Linq.Expressions;

namespace PrimumCore.Services.Iterators
{
    public class ThemeIterator(PrimumContext context, AdminProfileHelper helper)
    {
        private IQueryable<CourseTheme> Themes(bool isOnlyAvailable, Expression<Func<CourseTheme, bool>>? predicate) => context
            .Set<CourseTheme>()
            .WhereIf(predicate is not null, predicate!)
            .WhereIf(isOnlyAvailable, AvailabilityExpressions.IsThemeAvailable)
            .Include(x => x.Courses)
            .ThenInclude(x => x.Teacher)
            .ThenInclude(x => x.User);

        private IQueryable<CourseThemeDto> ToDto(IQueryable<CourseTheme> queryable) => queryable
            .Select(x => new CourseThemeDto
            {
                CourseThemeId = x.CourseThemeId,
                ThemeName = x.ThemeName,
                IsActive = x.IsActive
            });

        public async Task<IEnumerable<CourseThemeDto>> GetThemes(bool isOnlyAvailable)
        {
            return await ToDto(
                    Themes(isOnlyAvailable, null)
                ).ToArrayAsync();
        }

        public async Task<CourseThemeDto> GetTheme(int themeId, bool isOnlyAvailable)
        {
            return await ToDto(
                    Themes(isOnlyAvailable, null)
                ).FirstOrDefaultAsync(x => x.CourseThemeId == themeId) ?? throw new NotFoundException("Theme");
        }

        public async Task<int> CreateTheme(int adminId, CourseThemeInputDto dto)
        {
            var iteratingUser = await helper.CheckIteratingUser(adminId, Permission.EditCourseThemes);

            var theme = new CourseTheme
            {
                IsActive = dto.IsActive,
                ThemeName = dto.ThemeName,
            };
            await context.Set<CourseTheme>().AddAsync(theme);

            iteratingUser.AdminProfile!.IncidentLogs.Add(new IncidentLog
            {
                AdminProfileId = iteratingUser.AdminProfile.AdminId,
                Description =
                $"Created course theme with name: {theme.ThemeName}, active: {theme.IsActive}",
                DecisionDate = DateTime.Now
            });
            await context.SaveChangesAsync();
            return theme.CourseThemeId;
        }

        public async Task<int> EditTheme(int adminId, int themeId, CourseThemeInputDto dto)
        {
            var iteratingUser = await helper.CheckIteratingUser(adminId, Permission.EditCourseThemes);

            var theme = await Themes(false, null)
                .FirstOrDefaultAsync(x => x.CourseThemeId == themeId);
            if (theme is null) { throw new NotFoundException("Theme"); }

            theme.ThemeName = dto.ThemeName;
            theme.IsActive = dto.IsActive;

            iteratingUser.AdminProfile!.IncidentLogs.Add(new IncidentLog
            {
                AdminProfileId = iteratingUser.AdminProfile.AdminId,
                Description =
                $"Edited course theme ({themeId}) to name: {theme.ThemeName}, active: {theme.IsActive}",
                DecisionDate = DateTime.Now
            });
            await context.SaveChangesAsync();
            return theme.CourseThemeId;
        }
    }
}
