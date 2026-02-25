using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Pushables.Events
{
    public record UserVerifiedEmailEvent : IPushable
    {
        public required string EmailAdress { get; set; }

        public required int Userid { get; set; }
    }
}
