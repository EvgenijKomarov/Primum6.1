using CoreConnection.DTOs;
using CoreConnection.DTOs.Inputs;
using Microsoft.EntityFrameworkCore;
using PrimumCore.Extentions;
using PrimumCore.Models;
using PrimumCore.Models.Enums;
using PrimumCore.Services.Utilities;

namespace PrimumCore.Services.Iterators
{
    public class IncendentIterator(IPrimumContext context)
    {
        private AdminProfileHelper helper = new AdminProfileHelper(context);

        public async Task<IEnumerable<IncendentDto>> GetIncedents(int userId)
        {
            Permission[] userPermissions = (await helper.GetIteratingUser(userId)).AdminProfile.Permissions.Select(x => x.Permission).ToArray();
            IncendentCollector collector = new IncendentCollector(context);

            return await collector.GetIncedents(userPermissions);
        }

        public async Task<IEnumerable<IncendentLogDto>> GetIncendentLogs(int userId, bool OnlyUnrevisioned)
        {
            await helper.CheckIteratingUser(userId, Permission.InspectIncendentLogs);

            return await context.Set<IncendentLog>()
                .Include(x => x.AdminProfile)
                .ThenInclude(x => x.User)
                .Where(x => x.AdminProfile != null)
                .WhereIf(OnlyUnrevisioned, x => !x.IsRevisioned)
                .Select(x => new IncendentLogDto
                {
                    LogId = x.LogId,
                    AdminUserId = x.AdminProfile.User.Id,
                    AdminDisplayName = x.AdminProfile.User.DisplayName,
                    DateTime = x.DecisionDate,
                    Description = x.Description
                })
                .ToArrayAsync();
        }

        public async Task<IncendentLogDto> GetIncendentLog(int userId, int logId)
        {
            await helper.CheckIteratingUser(userId, Permission.InspectIncendentLogs);

            var log = await context.Set<IncendentLog>()
                .Include(x => x.AdminProfile)
                .ThenInclude(x => x.User)
                .Where(x => x.AdminProfile != null)
                .Select(x => new IncendentLogDto
                {
                    LogId = x.LogId,
                    AdminUserId = x.AdminProfile.User.Id,
                    AdminDisplayName = x.AdminProfile.User.DisplayName,
                    DateTime = x.DecisionDate,
                    Description = x.Description
                })
                .FirstOrDefaultAsync(x => x.LogId == logId);
            if (log is null) { throw new Exception("Log not found"); }

            return log;
        }

        public async Task<int> RevisionIncendentLog(int userId, int logId)
        {
            var user = await helper.CheckIteratingUser(userId, Permission.InspectIncendentLogs);

            var log = await context.Set<IncendentLog>()
                .Include(x => x.AdminProfile)
                .ThenInclude(x => x.User)
                .Where(x => x.AdminProfile != null)
                .FirstOrDefaultAsync(x => x.LogId == logId);
            if (log is null) { throw new Exception("Log not found"); }
            log.IsRevisioned = true;

            await context.SaveChangesAsync();
            return log.LogId;
        }

        public async Task<int> SolveIncedent(int userId, IncendentDecisionInputDto dto)
        {
            var iteratingUserAdminProfile = (await helper.GetIteratingUser(userId)).AdminProfile;
            Permission[] userPermissions = iteratingUserAdminProfile.Permissions.Select(x => x.Permission).ToArray();
            IncendentSolver solver = new IncendentSolver(context);

            var subjectId = await solver.SolveIncendent(iteratingUserAdminProfile.AdminId, userPermissions, dto);
            return subjectId;
        }
    }
}
