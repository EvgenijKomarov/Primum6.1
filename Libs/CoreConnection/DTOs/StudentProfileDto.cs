using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreConnection.DTOs
{
    public class StudentProfileDto : IHasId
    {
        public required string DisplayName { get; set; }

        public required int UserId { get; set; }

        public required int Coins { get; set; }

        public int Id => UserId;
    }
}
