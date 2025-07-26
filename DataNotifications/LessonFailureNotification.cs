using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataNotifications
{
    public class LessonFailureNotification
    {
        public required int StudentUserId { get; set; }

        public required int TeacherUserId { get; set; }

        public required string CourseName { get; set; }

        public required DateTime DateTime { get; set; }
    }
}
