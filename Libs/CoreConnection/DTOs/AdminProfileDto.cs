using CoreConnection.DTOs.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreConnection.DTOs
{
    public class AdminProfileDto
    {
        public required string DisplayName { get; set; }

        public required int UserId { get; set; }

        public required string Status {  get; set; }

        public required Dictionary<string, bool> Permissions { get; set; } = [];
    }
}
