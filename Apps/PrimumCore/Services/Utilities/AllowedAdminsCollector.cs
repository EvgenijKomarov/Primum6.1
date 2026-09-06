using CoreDBModel.Models;
using CoreDBModel.Models.Enums;
using Microsoft.EntityFrameworkCore;
using PrimumCore.Services.Iterators;

namespace PrimumCore.Services.Utilities
{
    public class AllowedAdminsCollector(DatabaseIterator dbIterator)
    {
        public async Task<List<AdminProfile>> GetAllowedAdmins(Permission[] permissions)
        {
            return await dbIterator
                .Admins()
                .Where(x => x.Permissions.Select(p => p.Permission).Any(p => permissions.Contains(p)))
                .ToListAsync();
        }
    }
}
