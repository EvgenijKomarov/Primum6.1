using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreConnection.DTOs.Inputs
{
    public class CourseInputDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int FreeLessons { get; set; }

        public int MaxLessons { get; set; }

        public int CourseThemeId { get; set; }
    }
}
