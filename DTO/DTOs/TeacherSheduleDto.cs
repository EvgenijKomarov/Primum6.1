using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreConnection.DTOs
{
    public class TeacherSheduleDto
    {
        public DayOfWeek DayOfWeek { get; set; }

        public int Time { get; set; }

        public bool IsBusy { get; set; }

        public string? StudentName { get; set; }

        public int? StudentId { get; set; }

        public string? CourseName { get; set; }

        public int? CourseId { get; set; }
    }
}
