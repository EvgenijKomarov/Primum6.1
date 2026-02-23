using CoreConnection.Enums;
using CoreDBModel.Models.Enums;

namespace PrimumCore.Exceptions
{
    public class NoPermissionException: Exception
    {
        public NoPermissionException(int adminId, Permission permission) :
            base($"Admin ({adminId}) can't do it because don't have permission {permission.ToString()}") { }

        public NoPermissionException(int adminId, IncidentMeaningDto meaning, IncidentDecisionDto decision) :
            base($"Admin ({adminId}) can't do it because don't have permission to {decision.ToString()} {meaning.ToString()}")
        { }
    }
}
