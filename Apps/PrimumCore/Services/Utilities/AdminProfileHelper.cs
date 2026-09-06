using CoreDBModel.Models;
using CoreDBModel.Models.Enums;
using Microsoft.EntityFrameworkCore;
using PrimumCore.Exceptions;
using PrimumCore.Extentions;
using PrimumCore.Services.Iterators;

namespace PrimumCore.Services.Utilities
{
    public class AdminProfileHelper(DatabaseIterator dbIterator)
    {
        public async Task<AdminProfile> CheckIteratingUser(int id, Permission permission)
        {
            var admin = await GetIteratingUser(id);
            if (!admin.CheckPermissions(permission)) { throw new NoPermissionException(id, permission); }
            return admin;
        }

        public async Task<AdminProfile> GetIteratingUser(int id)
        {
            return await dbIterator.Admins()
                .One(x => x.User.Id == id);
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
