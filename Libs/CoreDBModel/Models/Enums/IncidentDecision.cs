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
        BanUser = 5,
        Revise = 7,

        SetMissedByNoReason = 4,
        SetMissedByValidReason = 6,

        LightBlame = 8,
        HardBlame = 9,
        Pardon = 10,
    }
}
