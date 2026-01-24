using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreConnection.DTOs
{
    public class CourseThemeDto
    {
        public required int CourseThemeId { get; set; }

        public required string ThemeName { get; set; }

        public required bool IsActive { get; set; }
    }
}
