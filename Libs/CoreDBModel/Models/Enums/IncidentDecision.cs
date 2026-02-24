using System;
using System.Collections.Generic;
using System.Text;

namespace CoreDBModel.Models.Enums
{
    public enum IncidentDecision
    {
        Approve = 0,
        Delete = 1,
        SendToAdministrator = 2,
        SendToManager = 3,
        Revisioned = 4
    }
}
