using DTO.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.DTOs
{
    public class AbonementDto
    {
        public string StudentName { get; set; }

        public string TeacherName { get; set; }

        public string CourseName { get; set; }

        public int? CourseId { get; set; }

        public int PricePerLesson { get; set; }

        public int PaidLessons { get; set; }

        public AbonementStatusDto AbonementStatus { get; set; }
    }
}
