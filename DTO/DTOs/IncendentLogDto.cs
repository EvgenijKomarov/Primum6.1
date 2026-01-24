using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreConnection.DTOs
{
    public class IncendentLogDto
    {
        public int LogId { get; set; }

        public int AdminUserId { get; set; }

        public string AdminDisplayName { get; set; }

        public string Description { get; set; }
    }
}
