using PrimumCore.Models.Attributes;

namespace PrimumCore.Models.Enums
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
