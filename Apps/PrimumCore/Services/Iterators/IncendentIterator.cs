using CoreConnection.DTOs;
using CoreConnection.DTOs.Inputs;
using CoreDBModel.Models;
using CoreDBModel.Models.Enums;
using Microsoft.EntityFrameworkCore;
using PrimumCore.Exceptions;
using PrimumCore.Extentions;
using PrimumCore.Services.Utilities;

namespace PrimumCore.Services.Iterators
{
    public class IncidentIterator(PrimumContext context)
    {
        private AdminProfileHelper helper = new AdminProfileHelper(context);

        public async Task<IEnumerable<IncidentDto>> GetIncedents(int userId)
        {
            Permission[] userPermissions = (await helper.GetIteratingUser(userId)).AdminProfile.Permissions.Select(x => x.Permission).ToArray();
            IncidentCollector collector = new IncidentCollector(context);

            return await collector.GetIncedents(userPermissions);
        }

        public async Task<IEnumerable<IncidentLogDto>> GetIncidentLogs(int userId, bool OnlyUnrevisioned)
        {
            await helper.CheckIteratingUser(userId, Permission.InspectIncidentLogs);

            return await context.Set<IncidentLog>()
                .Include(x => x.AdminProfile)
                .ThenInclude(x => x.User)
                .Where(x => x.AdminProfile != null)
                .WhereIf(OnlyUnrevisioned, x => !x.IsRevisioned)
                .Select(x => new IncidentLogDto
                {
                    LogId = x.LogId,
                    AdminUserId = x.AdminProfile.User.Id,
                    AdminDisplayName = x.AdminProfile.User.DisplayName,
                    DateTime = x.DecisionDate,
                    Description = x.Description
                })
                .ToArrayAsync();
        }

        public async Task<IncidentLogDto> GetIncidentLog(int userId, int logId)
        {
            await helper.CheckIteratingUser(userId, Permission.InspectIncidentLogs);

            var log = await context.Set<IncidentLog>()
                .Include(x => x.AdminProfile)
                .ThenInclude(x => x.User)
                .Where(x => x.AdminProfile != null)
                .Select(x => new IncidentLogDto
                {
                    LogId = x.LogId,
                    AdminUserId = x.AdminProfile.User.Id,
                    AdminDisplayName = x.AdminProfile.User.DisplayName,
                    DateTime = x.DecisionDate,
                    Description = x.Description
                })
                .FirstOrDefaultAsync(x => x.LogId == logId);
            if (log is null) { throw new NotFoundException("Log"); }

            return log;
        }

        public async Task<int> RevisionIncidentLog(int userId, int logId)
        {
            var user = await helper.CheckIteratingUser(userId, Permission.InspectIncidentLogs);

            var log = await context.Set<IncidentLog>()
                .Include(x => x.AdminProfile)
                .ThenInclude(x => x.User)
                .Where(x => x.AdminProfile != null)
                .FirstOrDefaultAsync(x => x.LogId == logId);
            if (log is null) { throw new NotFoundException("Log"); }
            log.IsRevisioned = true;

            await context.SaveChangesAsync();
            return log.LogId;
        }

        public async Task<int> SolveIncedent(int userId, IncidentDecisionInputDto dto)
        {
            var iteratingUserAdminProfile = (await helper.GetIteratingUser(userId)).AdminProfile;
            Permission[] userPermissions = iteratingUserAdminProfile.Permissions.Select(x => x.Permission).ToArray();
            IncidentSolver solver = new IncidentSolver(context);

            var subjectId = await solver.SolveIncident(iteratingUserAdminProfile.AdminId, userPermissions, dto, userId);
            return subjectId;
        }
    }
}
