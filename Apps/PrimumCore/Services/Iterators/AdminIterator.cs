using CoreConnection.DTOs;
using PrimumCore.Entities;
using CoreDBModel.Models;
using CoreDBModel.Models.Enums;
using Microsoft.EntityFrameworkCore;
using PrimumCore.Exceptions;
using PrimumCore.Extentions;
using PrimumCore.Services.Utilities;

namespace PrimumCore.Services.Iterators
{
    public class AdminIterator(DatabaseIterator dbIterator, AdminProfileHelper helper)
    {
        public async Task<PageResult<AdminProfileDto>> GetAdmins(int _page, int _pageSize)
        {
            return await dbIterator.Admins().ToDto(helper).ToPageResult(_page, _pageSize);
        }

        public async Task<AdminProfileDto> GetAdmin(int userId)
        {
            return await dbIterator.Admins().ToDto(helper).One(x => x.UserId == userId);
        }

        public async Task<int> AddCash(int userId, int objUserId, int cash)
        {
            var iteratingAdmin = await helper.CheckIteratingUser(userId, Permission.AddCash);

            var user = await dbIterator.Users(false)
                .One(x => x.Id == objUserId);

            user.Cash += cash;

            iteratingAdmin.IncidentLogs.Add(new IncidentLog
            {
                AdminProfileId = iteratingAdmin.Id,
                Description = $"Added cash ({cash}) to user with Id {objUserId}"
            });
            await dbIterator.SaveChangesAsync();
            return objUserId;
        }

        public async Task<int> EditPermissions(int userId, int objUserId, Dictionary<string, bool> editedPermissions)
        {
            var iteratingAdmin = await helper.CheckIteratingUser(userId, Permission.EditPermissions);

            var admin = await dbIterator.Admins()
                .One(x => x.User.Id == objUserId);

            var adminPermissions = helper.GetAllPermissions(admin);
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
                admin.Permissions.Add(new AdminPermission
                {
                    AdminProfile = admin,
                    PromoterAdminProfile = iteratingAdmin,
                    PromotionDate = DateTime.Now,
                    Permission = newPermission
                });
            }
            foreach(var permissionToTake in permissionsToTake)
            {
                admin.Permissions
                    .Remove(admin.Permissions.FirstOrDefault(x => x.Permission == permissionToTake));
            }

            iteratingAdmin.IncidentLogs.Add(new IncidentLog
            {
                AdminProfileId = iteratingAdmin.Id,
                Description = 
                $"Given permissions: [{string.Join(", ", permissionsToGive)}]\n" +
                $"Taken permissions: [{string.Join(", ", permissionsToTake)}]"
            });

            await dbIterator.SaveChangesAsync();
            return admin.User.Id;
        }

        public async Task<int> CreateAdminProfile(int userId, int objUserId, string status)
        {
            var iteratingAdmin = await helper.CheckIteratingUser(userId, Permission.CreateAdminProfiles);

            var user = await dbIterator.Users(false)
                .One(x => x.Id == objUserId);
            if (user.AdminProfile is not null) { throw new BusinessLogicException("AdminProfile already exists"); }

            user.AdminProfile = new AdminProfile { Status = status };

            iteratingAdmin.IncidentLogs.Add(new IncidentLog
            {
                AdminProfileId = iteratingAdmin.Id,
                Description =
                $"Created AdminProfile to {objUserId}"
            });
            await dbIterator.SaveChangesAsync();
            return user.Id;
        }

        public async Task<int> DeleteAdminProfile(int userId, int objUserId)
        {
            var iteratingAdmin = await helper.CheckIteratingUser(userId, Permission.CreateAdminProfiles);

            var user = await dbIterator.Users(false)
                .One(x => x.Id == objUserId);
            if (user.AdminProfile is null) { throw new BusinessLogicException("AdminProfile not exists"); }

            await dbIterator.RemoveRangeAsync(user.AdminProfile.Permissions);
            await dbIterator.RemoveAsync(user.AdminProfile);

            iteratingAdmin.IncidentLogs.Add(new IncidentLog
            {
                AdminProfileId = iteratingAdmin.Id,
                Description =
                $"Deleted AdminProfile to {objUserId}"
            });
            await dbIterator.SaveChangesAsync();
            return user.Id;
        }

        public async Task<int> ChangeBanStatus(int userId, int objUserId, bool banStatus)
        {
            var iteratingAdmin = await helper.CheckIteratingUser(userId, Permission.ChangeBanStatus);

            var user = await dbIterator.Users(false)
                .One(x => x.Id == objUserId);

            user.IsBanned = banStatus;
            await dbIterator.SaveChangesAsync();
            return user.Id;
        }
    }
}
