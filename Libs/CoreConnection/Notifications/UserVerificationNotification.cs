using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreConnection.Notifications
{
    public class UserVerificationNotification: INotification
    {
        public required string EmailAdress { get; set; }

        public required string VerificationHash { get; set; }

        public required int Userid { get; set; }
    }
}
