using CoreConnection.DTOs;
using CoreDBModel.Models;
using CoreDBModel.Models.Enums;
using Microsoft.EntityFrameworkCore;
using PrimumCore.Exceptions;
using PrimumCore.Services.Utilities;

namespace PrimumCore.Services.Iterators
{
    public class AdminIterator(PrimumContext context, AdminProfileHelper helper)
    {
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
                    Permissions = helper.GetAllPermissions(x.AdminProfile)
                })
                .ToArrayAsync();
        }

        public async Task<AdminProfileDto> GetAdmin(int userId)
        {
            var admin = (await GetAdmins())
                .FirstOrDefault(x => x.UserId == userId);
            if (admin == null) { throw new NotFoundException("Admin"); }

            return admin;
        }

        public async Task<int> AddCash(int userId, int objUserId, int cash)
        {
            var iteratingUser = await helper.CheckIteratingUser(userId, Permission.AddCash);

            var user = await context.Set<User>()
                .FirstOrDefaultAsync(x => x.Id == objUserId);
            if (user is null) { throw new NotFoundException("User"); }

            user.Cash += cash;

            iteratingUser.AdminProfile.IncidentLogs.Add(new IncidentLog
            {
                AdminProfileId = iteratingUser.AdminProfile.AdminId,
                Description = $"Added cash ({cash} to userId {objUserId})",
                DecisionDate = DateTime.Now
            });
            await context.SaveChangesAsync();
            return objUserId;
        }

        public async Task<int> EditPermissions(int userId, int objUserId, Dictionary<string, bool> editedPermissions)
        {
            var iteratingUser = await helper.CheckIteratingUser(userId, Permission.EditPermissions);

            var user = await context.Set<User>()
                .Include(x => x.AdminProfile)
                .ThenInclude(x => x.Permissions)
                .FirstOrDefaultAsync(x => x.Id == objUserId);
            if (user is null || user.AdminProfile is null) { throw new NotFoundException("Admin"); }

            var adminPermissions = helper.GetAllPermissions(user.AdminProfile);
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

            iteratingUser.AdminProfile.IncidentLogs.Add(new IncidentLog
            {
                AdminProfileId = iteratingUser.AdminProfile.AdminId,
                Description = 
                $"Given permissions: [{string.Join(", ", permissionsToGive)}]\n" +
                $"Taken permissions: [{string.Join(", ", permissionsToTake)}]",
                DecisionDate = DateTime.Now
            });

            await context.SaveChangesAsync();
            return user.Id;
        }

        public async Task<int> CreateAdminProfile(int userId, int objUserId, string status)
        {
            var iteratingUser = await helper.CheckIteratingUser(userId, Permission.CreateAdminProfiles);

            var user = await context.Set<User>()
                .Include(x => x.AdminProfile)
                .ThenInclude(x => x.Permissions)
                .FirstOrDefaultAsync(x => x.Id == objUserId);
            if (user is null) { throw new NotFoundException("User"); }
            if (user.AdminProfile is not null) { throw new BusinessLogicException("AdminProfile already exists"); }

            user.AdminProfile = new AdminProfile { Status = status };

            iteratingUser.AdminProfile.IncidentLogs.Add(new IncidentLog
            {
                AdminProfileId = iteratingUser.AdminProfile.AdminId,
                Description =
                $"Created AdminProfile to {objUserId}",
                DecisionDate = DateTime.Now
            });
            await context.SaveChangesAsync();
            return user.Id;
        }

        public async Task<int> DeleteAdminProfile(int userId, int objUserId)
        {
            var iteratingUser = await helper.CheckIteratingUser(userId, Permission.CreateAdminProfiles);

            var user = await context.Set<User>()
                .Include(x => x.AdminProfile)
                .ThenInclude(x => x.Permissions)
                .FirstOrDefaultAsync(x => x.Id == objUserId);
            if (user is null) { throw new NotFoundException("User"); }
            if (user.AdminProfile is null) { throw new BusinessLogicException("AdminProfile not exists"); }

            context.Set<AdminPermission>().RemoveRange(user.AdminProfile.Permissions);
            context.Set<AdminProfile>().Remove(user.AdminProfile);

            iteratingUser.AdminProfile.IncidentLogs.Add(new IncidentLog
            {
                AdminProfileId = iteratingUser.AdminProfile.AdminId,
                Description =
                $"Deleted AdminProfile to {objUserId}",
                DecisionDate = DateTime.Now
            });
            await context.SaveChangesAsync();
            return user.Id;
        }

        public async Task<int> BanUser(int userId, int objUserId, bool banStatus)
        {
            var iteratingUser = await helper.CheckIteratingUser(userId, Permission.ChangeBanStatus);

            var user = await context.Set<User>()
                .FirstOrDefaultAsync(x => x.Id == objUserId);
            if (user is null) { throw new NotFoundException("User"); }

            user.IsBanned = banStatus;
            await context.SaveChangesAsync();
            return user.Id;
        }
    }
}
