using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreConnection.DTOs
{
    public class CourseThemeDto
    {
        public int CourseThemeId { get; set; }

        public string ThemeName { get; set; } = null!;

        public bool IsActive { get; set; }

        public IEnumerable<CourseDto> Courses { get; set; }
    }
}
