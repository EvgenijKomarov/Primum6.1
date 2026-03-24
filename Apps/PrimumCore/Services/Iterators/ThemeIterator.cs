using CoreConnection.DTOs;
using CoreConnection.DTOs.Inputs;
using CoreConnection.Entities;
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

        public async Task<PageResult<CourseThemeDto>> GetThemes(bool isOnlyAvailable, int _page, int _pageSize)
        {
            return await Themes(isOnlyAvailable, null).ToDto().ToPageResult(_page, _pageSize);
        }

        public async Task<CourseThemeDto> GetTheme(int themeId, bool isOnlyAvailable)
        {
            return await Themes(isOnlyAvailable, null).ToDto().One(x => x.Id == themeId);
        }

        public async Task<int> CreateTheme(int adminId, CourseThemeInputDto dto)
        {
            var iteratingAdmin = await helper.CheckIteratingUser(adminId, Permission.EditCourseThemes);

            var theme = new CourseTheme
            {
                IsActive = dto.IsActive,
                ThemeName = dto.ThemeName,
            };
            await context.Set<CourseTheme>().AddAsync(theme);

            iteratingAdmin.IncidentLogs.Add(new IncidentLog
            {
                AdminProfileId = iteratingAdmin.Id,
                Description =
                $"Created course theme with name: {theme.ThemeName}, active: {theme.IsActive}"
            });
            await context.SaveChangesAsync();
            return theme.Id;
        }

        public async Task<int> EditTheme(int adminId, int themeId, CourseThemeInputDto dto)
        {
            var iteratingAdmin = await helper.CheckIteratingUser(adminId, Permission.EditCourseThemes);

            var theme = await Themes(false, null)
                .One(x => x.Id == themeId);

            theme.ThemeName = dto.ThemeName;
            theme.IsActive = dto.IsActive;

            iteratingAdmin.IncidentLogs.Add(new IncidentLog
            {
                AdminProfileId = iteratingAdmin.Id,
                Description =
                $"Edited course theme ({themeId}) to name: {theme.ThemeName}, active: {theme.IsActive}"
            });
            await context.SaveChangesAsync();
            return theme.Id;
        }
    }
}
