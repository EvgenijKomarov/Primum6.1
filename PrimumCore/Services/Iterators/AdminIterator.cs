using CoreConnection.DTOs;
using CoreConnection.DTOs.Inputs;
using Microsoft.EntityFrameworkCore;
using PrimumCore.Extentions;
using PrimumCore.Models;
using PrimumCore.Models.Enums;
using PrimumCore.Services.Utilities;
using System.Security;

namespace PrimumCore.Services.Iterators
{
    public class AdminIterator(IPrimumContext context)
    {
        public async Task<IEnumerable<IncendentDto>> GetIncedents(int userId)
        {
            Permission[] userPermissions = (await GetIteratingUser(userId)).AdminProfile.Permissions.Select(x => x.Permission).ToArray();
            IncendentCollector collector = new IncendentCollector(context);

            return await collector.GetIncedents(userPermissions);
        }

        public async Task<IEnumerable<AdminProfileDto>> GetAdmins()
        {
            return await context.Set<User>()
                .Include(x => x.AdminProfile)
                .ThenInclude(x => x.Permissions)
                .Where(x => x.AdminProfile != null)
                .Select(x => new AdminProfileDto
                {
                    DisplayName = x.DisplayName,
                    UserId = x.Id,
                    Status = x.AdminProfile.Status,
                    Permissions = GetAllPermissions(x.AdminProfile)
                })
                .ToArrayAsync();
        }

        public async Task<AdminProfileDto> GetAdmin(int userId)
        {
            var admin = await context.Set<User>()
                .Include(x => x.AdminProfile)
                .ThenInclude(x => x.Permissions)
                .Where(x => x.AdminProfile != null)
                .Select(x => new AdminProfileDto
                {
                    DisplayName = x.DisplayName,
                    UserId = x.Id,
                    Status = x.AdminProfile.Status,
                    Permissions = GetAllPermissions(x.AdminProfile)
                })
                .FirstOrDefaultAsync(x => x.UserId == userId);
            if (admin == null) { throw new Exception("Admin not found"); }

            return admin;
        }

        public async Task<IEnumerable<IncendentLogDto>> GetIncendentLogs(int userId, bool OnlyUnrevisioned)
        {
            await CheckIteratingUser(userId, Permission.InspectIncendentLogs);

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
                    Description = x.Description
                })
                .ToArrayAsync();
        }

        public async Task<IncendentLogDto> GetIncendentLog(int userId, int logId)
        {
            await CheckIteratingUser(userId, Permission.InspectIncendentLogs);

            var log = await context.Set<IncendentLog>()
                .Include(x => x.AdminProfile)
                .ThenInclude(x => x.User)
                .Where(x => x.AdminProfile != null)
                .Select(x => new IncendentLogDto
                {
                    LogId = x.LogId,
                    AdminUserId = x.AdminProfile.User.Id,
                    AdminDisplayName = x.AdminProfile.User.DisplayName,
                    Description = x.Description
                })
                .FirstOrDefaultAsync(x => x.LogId == logId);
            if (log is null) { throw new Exception("Log not found"); }

            return log;
        }

        public async Task<int> RevisionIncendentLog(int userId, int logId)
        {
            var user = await CheckIteratingUser(userId, Permission.InspectIncendentLogs);

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
            var iteratingUserAdminProfile = (await GetIteratingUser(userId)).AdminProfile;
            Permission[] userPermissions = iteratingUserAdminProfile.Permissions.Select(x => x.Permission).ToArray();
            IncendentSolver solver = new IncendentSolver(context);

            var subjectId = await solver.SolveIncendent(userPermissions, dto);
            iteratingUserAdminProfile.IncendentLogs.Add(new IncendentLog
            {
                AdminProfileId = iteratingUserAdminProfile.AdminId,
                Description = $"Info:\n" +
                $"{dto.IncendentInfo}\n" +
                $"Object: {dto.Meaning.ToString()} with Id {dto.ObjectId}\n" +
                $"Decision: {dto.Decision.ToString()}"
            });

            await context.SaveChangesAsync();
            return subjectId;
        }

        public async Task<int> AddCash(int userId, int objUserId, int cash)
        {
            var iteratingUser = await CheckIteratingUser(userId, Permission.AddCash);

            var user = await context.Set<User>()
                .FirstOrDefaultAsync(x => x.Id == objUserId);
            if (user is null) { throw new Exception("User not found"); }

            user.Cash += cash;

            iteratingUser.AdminProfile.IncendentLogs.Add(new IncendentLog
            {
                AdminProfileId = iteratingUser.AdminProfile.AdminId,
                Description = $"Added cash ({cash} to userId {objUserId})"
            });
            await context.SaveChangesAsync();
            return objUserId;
        }

        public async Task<int> EditPermissions(int userId, int objUserId, Dictionary<string, bool> editedPermissions)
        {
            var iteratingUser = await CheckIteratingUser(userId, Permission.GivePermissions);

            var user = await context.Set<User>()
                .Include(x => x.AdminProfile)
                .ThenInclude(x => x.Permissions)
                .FirstOrDefaultAsync(x => x.Id == objUserId);
            if (user is null || user.AdminProfile is null) { throw new Exception("User not found"); }

            var adminPermissions = GetAllPermissions(user.AdminProfile);
            Permission[] permissionsToTake = adminPermissions
                .Where(kvp => kvp.Value == true && editedPermissions.TryGetValue(kvp.Key, out bool val) && val == false)
                .Select(kvp =>
                {
                    Enum.TryParse(kvp.Key, out Permission permission);
                    return permission;
                })
                .ToArray();
            Permission[] permissionsToGive = editedPermissions
                .Where(kvp => kvp.Value == true && adminPermissions.TryGetValue(kvp.Key, out bool val) && val == false)
                .Select(kvp =>
                {
                    Enum.TryParse(kvp.Key, out Permission permission);
                    return permission;
                })
                .ToArray();

            foreach (var newPermission in permissionsToGive) 
            {
                user.AdminProfile.Permissions.Add(new AdminPermission
                {
                    AdminProfile = iteratingUser.AdminProfile,
                    PromoterAdminProfile = user.AdminProfile,
                    PromotionDate = DateTime.Now,
                    Permission = newPermission
                });
            }
            foreach(var permissionToTake in permissionsToTake)
            {
                user.AdminProfile.Permissions
                    .Remove(user.AdminProfile.Permissions.FirstOrDefault(x => x.Permission == permissionToTake));
            }

            iteratingUser.AdminProfile.IncendentLogs.Add(new IncendentLog
            {
                AdminProfileId = iteratingUser.AdminProfile.AdminId,
                Description = 
                $"Given permissions: {permissionsToGive.ToString()}\n" +
                $"Taken permissions: {permissionsToTake.ToString()}"
            });

            await context.SaveChangesAsync();
            return user.Id;
        }

        public async Task<int> CreateAdminProfile(int userId, int objUserId, string status)
        {
            var iteratingUser = await CheckIteratingUser(userId, Permission.CreateAdminProfiles);

            var user = await context.Set<User>()
                .Include(x => x.AdminProfile)
                .ThenInclude(x => x.Permissions)
                .FirstOrDefaultAsync(x => x.Id == objUserId);
            if (user is null) { throw new Exception("User not found"); }
            if (user.AdminProfile is not null) { throw new Exception("AdminProfile already exists"); }

            user.AdminProfile = new AdminProfile { Status = status };

            iteratingUser.AdminProfile.IncendentLogs.Add(new IncendentLog
            {
                AdminProfileId = iteratingUser.AdminProfile.AdminId,
                Description =
                $"Created AdminProfile to {objUserId}"
            });
            await context.SaveChangesAsync();
            return user.Id;
        }

        public async Task<int> DeleteAdminProfile(int userId, int objUserId)
        {
            var iteratingUser = await CheckIteratingUser(userId, Permission.CreateAdminProfiles);

            var user = await context.Set<User>()
                .Include(x => x.AdminProfile)
                .ThenInclude(x => x.Permissions)
                .FirstOrDefaultAsync(x => x.Id == objUserId);
            if (user is null) { throw new Exception("User not found"); }
            if (user.AdminProfile is null) { throw new Exception("AdminProfile not exists"); }

            context.Set<AdminPermission>().RemoveRange(user.AdminProfile.Permissions);
            context.Set<AdminProfile>().Remove(user.AdminProfile);

            iteratingUser.AdminProfile.IncendentLogs.Add(new IncendentLog
            {
                AdminProfileId = iteratingUser.AdminProfile.AdminId,
                Description =
                $"Deleted AdminProfile to {objUserId}"
            });
            await context.SaveChangesAsync();
            return user.Id;
        }

        private async Task<User> CheckIteratingUser(int id, Permission permission)
        {
            var user = await GetIteratingUser(id);
            if (!user.AdminProfile.CheckPermissions(permission)) { throw new Exception("Iterator can't do it due permission policy"); }
            return user;
        }

        private async Task<User> GetIteratingUser(int id)
        {
            var user = await context.Set<User>()
                .Include(x => x.AdminProfile)
                .ThenInclude(x => x.Permissions)
                .Include(x => x.AdminProfile)
                .ThenInclude(x => x.GivenPermissions)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (user is null || user.AdminProfile is null) { throw new Exception("Iterator admin not found"); }
            if (!user.IsActive) { throw new Exception("User is not active"); }
            return user;
        }

        private Dictionary<string, bool> GetAllPermissions(AdminProfile admin)
        {
            return Enum.GetValues(typeof(Permission))
                .Cast<Permission>()
                .ToDictionary(
                            perm => perm.ToString(),
                            hpermission => admin.Permissions.Select(p => p.Permission).Contains(hpermission)
                            );
        }
    }
}
