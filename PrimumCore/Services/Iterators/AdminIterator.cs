using CoreConnection.DTOs;
using Microsoft.EntityFrameworkCore;
using PrimumCore.Models;
using PrimumCore.Models.Enums;
using PrimumCore.Services.Utilities;

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

        public async Task<int> SolveIncedent(int userId, IncendentDecisionInputDto dto)
        {
            Permission[] userPermissions = (await GetIteratingUser(userId)).AdminProfile.Permissions.Select(x => x.Permission).ToArray();
            IncendentSolver solver = new IncendentSolver(context);

            return await solver.SolveIncendent(userPermissions, dto);
        }

        public async Task<int> AddCash(int userId, int objUserId, int cash)
        {
            await CheckIteratingUser(userId, Permission.AddCash);

            var user = await context.Set<User>()
                .FirstOrDefaultAsync(x => x.Id == objUserId);
            if (user is null) { throw new Exception("User not found"); }

            user.Cash += cash;
            await context.SaveChangesAsync();
            return objUserId;
        }

        public async Task<int> GivePermission(int userId, int objUserId, string permissionIndex)
        {
            var iteratingUser = await CheckIteratingUser(userId, Permission.GivePermissions);

            var user = await context.Set<User>()
                .Include(x => x.AdminProfile)
                .ThenInclude(x => x.Permissions)
                .FirstOrDefaultAsync(x => x.Id == objUserId);
            if (user is null || user.AdminProfile is null) { throw new Exception("User not found"); }

            if (Enum.TryParse(permissionIndex, out Permission permission)) { throw new Exception("Invalid index of permission"); }
            if (user.AdminProfile.Permissions.Any(x => x.Permission == permission)) { throw new Exception("User already have this permission"); }

            var adminPermissionToGive = new AdminPermission
            {
                AdminProfile = iteratingUser.AdminProfile,
                PromoterAdminProfile = user.AdminProfile,
                PromotionDate = DateTime.Now,
                Permission = permission
            };
            user.AdminProfile.Permissions.Add(adminPermissionToGive);

            await context.SaveChangesAsync();
            return adminPermissionToGive.AdminPermissionId;
        }

        public async Task<int> TakeBackPermission(int userId, int objUserId, string permissionIndex)
        {
            var iteratingUser = await CheckIteratingUser(userId, Permission.GivePermissions);

            var user = await context.Set<User>()
                .Include(x => x.AdminProfile)
                .ThenInclude(x => x.Permissions)
                .FirstOrDefaultAsync(x => x.Id == objUserId);
            if (user is null || user.AdminProfile is null) { throw new Exception("User not found"); }

            if (Enum.TryParse(permissionIndex, out Permission permission)) { throw new Exception("Invalid index of permission"); }
            var adminPermissionToTake = user.AdminProfile.Permissions.FirstOrDefault(x => x.Permission == permission);
            if (adminPermissionToTake is null) { throw new Exception("User don't have this permission"); }

            user.AdminProfile.Permissions.Remove(adminPermissionToTake);

            await context.SaveChangesAsync();
            return adminPermissionToTake.AdminPermissionId;
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
    }
}
