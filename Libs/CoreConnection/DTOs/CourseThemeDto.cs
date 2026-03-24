using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreConnection.DTOs
{
    public class CourseThemeDto : IHasId
    {
        public required int Id { get; set; }

        public required string ThemeName { get; set; }

        public required bool IsActive { get; set; }
    }
}
