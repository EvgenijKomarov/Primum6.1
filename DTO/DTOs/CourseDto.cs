using DTO.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.DTOs
{
    public class CourseDto
    {
        public int CourseId { get; set; }

        public string Name { get; set; }

        public string TeacherName { get; set; }

        public int TeacherId {  get; set; }

        public int Price { get; set; }

        public int MaxLessons { get; set; }

        public int FreeLessons { get; set; }
        public string TeacherAbout { get; set; }

        public ApproveStatusDto ApproveStatus { get; set; }
    }
}
