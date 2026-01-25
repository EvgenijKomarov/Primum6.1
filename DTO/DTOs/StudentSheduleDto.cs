using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreConnection.DTOs
{
    public class StudentSheduleDto
    {
        public required DayOfWeek DayOfWeek { get; set; }

        public required int Time { get; set; }

        public required string CourseName { get; set; }

        public required int CourseId { get; set; }

        public required string TeacherDisplayName { get; set; }

        public required int TeacherId { get; set; }
    }
}
