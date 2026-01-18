using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreConnection.DTOs
{
    public class StudentSheduleDto
    {
        public DayOfWeek DayOfWeek { get; set; }

        public int Time { get; set; }

        public string CourseName { get; set; } = null!;

        public int CourseId { get; set; }

        public string TeacherDisplayName { get; set; } = null!;

        public int TeacherId { get; set; }
    }
}
