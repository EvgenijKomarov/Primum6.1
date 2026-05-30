using CoreConnection.DTOs.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreConnection.DTOs
{
    public class TeacherProfileDto : IHasLevel
    {
        public required string DisplayName { get; set; }

        public required string About { get; set; }

        public required int UserId { get; set; }

        public required bool IsAvailable { get; set; }

        public required int Level { get; set; }

        public required string Rank { get; set; }

        public required int Experience { get; set; }
    }
}
