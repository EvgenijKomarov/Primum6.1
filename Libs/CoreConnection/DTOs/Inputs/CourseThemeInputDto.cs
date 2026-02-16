using System;
using System.Collections.Generic;
using System.Text;

namespace CoreConnection.DTOs.Inputs
{
    public class CourseThemeInputDto
    {
        public required string ThemeName { get; set; }

        public required bool IsActive { get; set; }
    }
}
