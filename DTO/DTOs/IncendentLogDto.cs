using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreConnection.DTOs
{
    public class IncendentLogDto
    {
        public required int LogId { get; set; }

        public required int AdminUserId { get; set; }

        public required DateTime DateTime { get; set; }

        public required string AdminDisplayName { get; set; }

        public required string Description { get; set; }
    }
}
