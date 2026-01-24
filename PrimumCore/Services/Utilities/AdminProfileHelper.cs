using Microsoft.EntityFrameworkCore;
using PrimumCore.Models;
using PrimumCore.Models.Enums;

namespace PrimumCore.Services.Utilities
{
    public class AdminProfileHelper(IPrimumContext context)
    {
        public async Task<User> CheckIteratingUser(int id, Permission permission)
        {
            var user = await GetIteratingUser(id);
            if (!user.AdminProfile.CheckPermissions(permission)) { throw new Exception("Iterator can't do it due permission policy"); }
            return user;
        }

        public async Task<User> GetIteratingUser(int id)
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

        public Dictionary<string, bool> GetAllPermissions(AdminProfile admin)
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
