using PrimumCore.Models.Attributes;
using PrimumCore.Models.Enums;

namespace PrimumCore.Extentions
{
    public static class PermissionExtention
    {
        public static IEnumerable<AvailableIncendent> GetAvailableIncendentsAttributes(this Permission permission)
        {
            var field = typeof(Permission).GetField(permission.ToString());
            if (field == null)
                return Array.Empty<AvailableIncendent>();

            var attributes = field.GetCustomAttributes(typeof(AvailableIncendent), inherit: false)
                                  .Cast<AvailableIncendent>()
                                  .ToArray();

            return attributes;
        }
    }
}
