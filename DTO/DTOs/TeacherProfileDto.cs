using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreConnection.DTOs
{
    public class TeacherProfileDto
    {
        public string DisplayName { get; set; } = null!;

        public string About { get; set; }

        public int UserId { get; set; }

        public bool IsAvailable { get; set; }
    }
}
