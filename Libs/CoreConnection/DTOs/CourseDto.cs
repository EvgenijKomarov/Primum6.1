using System;
using System.Collections.Generic;
using System.Text;

namespace CoreConnection.DTOs
{
    public class CourseDto : CourseDtoLite
    {
        public required string ReferalLink { get; set; }

        public required int Experience { get; set; }

        public required bool OnCheck { get; set; }
    }
}
