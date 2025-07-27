using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimumPlatformModel.Models.Enums
{
    public enum ApproveStatus
    {
        NeedModeratorReview = 0,
        Approved = 1,
        NeedAdministratorReview = 2,
        NeedManagerReview = 3
    }
}
