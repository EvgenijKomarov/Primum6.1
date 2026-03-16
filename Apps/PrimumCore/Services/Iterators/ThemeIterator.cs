using CoreConnection.DTOs;
using CoreConnection.DTOs.Inputs;
using PrimumCore.Entities;
using CoreDBModel.Models;
using CoreDBModel.Models.Enums;
using PrimumCore.Extentions;
using PrimumCore.Services.Utilities;

namespace PrimumCore.Services.Iterators
{
    public class ThemeIterator(DatabaseIterator dbIterator, AdminProfileHelper helper)
    {
        public async Task<PageResult<CourseThemeDto>> GetThemes(bool isOnlyAvailable, int _page, int _pageSize)
        {
            return await dbIterator.Themes(isOnlyAvailable).ToDto().ToPageResult(_page, _pageSize);
        }

        public async Task<CourseThemeDto> GetTheme(int themeId, bool isOnlyAvailable)
        {
            return await dbIterator.Themes(isOnlyAvailable).ToDto().One(x => x.Id == themeId);
        }

        public async Task<int> CreateTheme(int adminId, CourseThemeInputDto dto)
        {
            var iteratingAdmin = await helper.CheckIteratingUser(adminId, Permission.EditCourseThemes);

            var theme = new CourseTheme
            {
                IsActive = dto.IsActive,
                ThemeName = dto.ThemeName,
            };
            await dbIterator.AddAsync(theme);

            iteratingAdmin.IncidentLogs.Add(new IncidentLog
            {
                AdminProfileId = iteratingAdmin.Id,
                Description =
                $"Created course theme with name: {theme.ThemeName}, active: {theme.IsActive}"
            });
            await dbIterator.SaveChangesAsync();
            return theme.Id;
        }

        public async Task<int> EditTheme(int adminId, int themeId, CourseThemeInputDto dto)
        {
            var iteratingAdmin = await helper.CheckIteratingUser(adminId, Permission.EditCourseThemes);

            var theme = await dbIterator.Themes(false)
                .One(x => x.Id == themeId);

            theme.ThemeName = dto.ThemeName;
            theme.IsActive = dto.IsActive;

            iteratingAdmin.IncidentLogs.Add(new IncidentLog
            {
                AdminProfileId = iteratingAdmin.Id,
                Description =
                $"Edited course theme ({themeId}) to name: {theme.ThemeName}, active: {theme.IsActive}"
            });
            await dbIterator.SaveChangesAsync();
            return theme.Id;
        }
    }
}
