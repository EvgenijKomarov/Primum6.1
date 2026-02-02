using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreConnection.Enums
{
    public enum IncidentDecisionDto
    {
        Approve = 0,
        Delete = 1,
        SendToAdministrator = 2,
        SendToManager = 3,
        Revisioned = 4
    }
}
