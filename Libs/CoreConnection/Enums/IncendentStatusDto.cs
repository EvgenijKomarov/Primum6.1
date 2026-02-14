using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreConnection.Enums
{
    public enum IncidentStatusDto
    {
        Unknown = 0,
        NeedModeration = 1,
        NeedAdministration = 2,
        NeedManagerApprovement = 3,
        NeedInspectation = 4,

    }
}
