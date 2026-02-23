using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Pushables.Notifications
{
    public class UserEmailVerificationNotification: INotification
    {
        public required string EmailAdress { get; set; }
        public required string VerificationHash { get; set; }
        public required int UserId { get; set; }
    }
}
