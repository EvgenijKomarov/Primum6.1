using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreConnection.Enums
{
    public enum ApproveStatusDto
    {
        NeedModeratorReview = 0,
        Approved = 1,
        NeedAdministratorReview = 2,
        NeedManagerReview = 3
    }
}
