using CoreConnection.DTOs;
using CoreConnection.DTOs.Inputs;
using CoreConnection.Entities;
using CoreDBModel.Models;
using CoreDBModel.Models.Enums;
using Microsoft.EntityFrameworkCore;
using PrimumCore.Exceptions;
using PrimumCore.Extentions;
using PrimumCore.Services.Utilities;

namespace PrimumCore.Services.Iterators
{
    public class IncidentIterator(PrimumContext context, IncidentCollector collector, IncidentSolver solver, AdminProfileHelper helper)
    {
        public async Task<PageResult<IncidentDto>> GetIncedents(int userId, int _page, int _pageSize)
        {
            Permission[] userPermissions = (await helper.GetIteratingUser(userId)).Permissions.Select(x => x.Permission).ToArray();

            return await collector.GetIncedents(userPermissions, _page, _pageSize);
        }

        private IQueryable<IncidentLog> IncidentLogs(bool OnlyUnrevisioned) => context
            .Set<IncidentLog>()
            .Include(x => x.AdminProfile)
            .ThenInclude(x => x.User)
            .Where(x => x.AdminProfile != null)
            .WhereIf(OnlyUnrevisioned, x => !x.IsRevisioned);

        public async Task<PageResult<IncidentLogDto>> GetIncidentLogs(int userId, bool OnlyUnrevisioned, int _page, int _pageSize)
        {
            await helper.CheckIteratingUser(userId, Permission.InspectIncidentLogs);

            return await IncidentLogs(OnlyUnrevisioned).ToDto().ToPageResult(_page, _pageSize);
        }

        public async Task<IncidentLogDto> GetIncidentLog(int userId, int logId)
        {
            await helper.CheckIteratingUser(userId, Permission.InspectIncidentLogs);

            return await IncidentLogs(false).ToDto().One(x => x.LogId == logId);
        }

        public async Task<int> RevisionIncidentLog(int userId, int logId)
        {
            var user = await helper.CheckIteratingUser(userId, Permission.InspectIncidentLogs);

            var log = await IncidentLogs(false).One(x => x.Id == logId);
            log.IsRevisioned = true;

            await context.SaveChangesAsync();
            return log.Id;
        }

        public async Task<int> SolveIncedent(int userId, IncidentDecisionInputDto dto)
        {
            var iteratingUserAdminProfile = await helper.GetIteratingUser(userId);
            Permission[] userPermissions = iteratingUserAdminProfile.Permissions.Select(x => x.Permission).ToArray();

            var subjectId = await solver.SolveIncident(iteratingUserAdminProfile.Id, userPermissions, dto, userId);
            return subjectId;
        }
    }
}
