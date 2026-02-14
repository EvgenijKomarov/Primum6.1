using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreConnection.Enums
{
    public enum StatusLesson
    {
        Waiting = 0,
        Warned = 1,
        Happened = 2,
        Missed = 3,
        MissedWithoutReason = 4
    }
}
