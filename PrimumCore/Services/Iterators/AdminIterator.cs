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
        private AdminProfileHelper helper = new AdminProfileHelper(context);

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
            var admin = await context.Set<User>()
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
                .FirstOrDefaultAsync(x => x.UserId == userId);
            if (admin == null) { throw new Exception("Admin not found"); }

            return admin;
        }

        public async Task<int> AddCash(int userId, int objUserId, int cash)
        {
            var iteratingUser = await helper.CheckIteratingUser(userId, Permission.AddCash);

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
            var iteratingUser = await helper.CheckIteratingUser(userId, Permission.GivePermissions);

            var user = await context.Set<User>()
                .Include(x => x.AdminProfile)
                .ThenInclude(x => x.Permissions)
                .FirstOrDefaultAsync(x => x.Id == objUserId);
            if (user is null || user.AdminProfile is null) { throw new Exception("User not found"); }

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
            var iteratingUser = await helper.CheckIteratingUser(userId, Permission.CreateAdminProfiles);

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
            var iteratingUser = await helper.CheckIteratingUser(userId, Permission.CreateAdminProfiles);

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
    }
}
