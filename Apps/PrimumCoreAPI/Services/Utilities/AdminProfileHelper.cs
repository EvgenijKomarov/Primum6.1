using CoreDBModel.Models;
using CoreDBModel.Models.Enums;
using Microsoft.EntityFrameworkCore;
using PrimumCore.Exceptions;

namespace PrimumCore.Services.Utilities
{
    public class AdminProfileHelper(PrimumContext context)
    {
        public async Task<User> CheckIteratingUser(int id, Permission permission)
        {
            var user = await GetIteratingUser(id);
            if (!user.AdminProfile.CheckPermissions(permission)) { throw new NoPermissionException(id, permission); }
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

            if (user is null || user.AdminProfile is null) { throw new NotFoundException("Iterator admin"); }
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
