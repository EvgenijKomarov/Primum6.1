using PrimumCore.Models.Attributes;
using PrimumCore.Models.Enums;

namespace PrimumCore.Extentions
{
    public static class PermissionExtension
    {
        public static IEnumerable<AvailableIncident> GetAvailableIncidentsAttributes(this Permission permission)
        {
            var field = typeof(Permission).GetField(permission.ToString());
            if (field == null)
                return Array.Empty<AvailableIncident>();

            var attributes = field.GetCustomAttributes(typeof(AvailableIncident), inherit: false)
                                  .Cast<AvailableIncident>()
                                  .ToArray();

            return attributes;
        }
    }
}
