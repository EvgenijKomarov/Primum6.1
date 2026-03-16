using CoreConnection.DTOs.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreConnection.DTOs
{
    public class TeacherProfileDto : IHasRating
    {
        public required string DisplayName { get; set; }

        public required string About { get; set; }

        public required int UserId { get; set; }

        public required bool IsAvailable { get; set; }

        public required float? Rating { get; set; }
    }
}
