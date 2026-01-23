using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreConnection.DTOs
{
    public class AdminProfileDto
    {
        public string DisplayName { get; set; } = null!;

        public int UserId { get; set; }

        public string Status {  get; set; } = null!;

        public Dictionary<string, bool> Permissions { get; set; } = [];
    }
}
