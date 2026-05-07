using CoreConnection.DTOs.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreConnection.DTOs
{
    public class StudentProfileDto : IHasRating
    {
        public required string DisplayName { get; set; }

        public required int UserId { get; set; }

        public required int Coins { get; set; }

        public required float? Rating { get; set; }

        public required int Level { get; set; }

        public required decimal Cash { get; set; }

        public required string Rank { get; set; }
    }
}
