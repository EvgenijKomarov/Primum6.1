using System;
using System.Collections.Generic;
using System.Text;

namespace CoreDBModel.Models.Enums
{
    public enum IncidentStatus
    {
        Unknown = 0,
        NeedModeration = 1,
        NeedAdministration = 2,
        NeedManagerApprovement = 3,
        NeedInspectation = 4,

    }
}
