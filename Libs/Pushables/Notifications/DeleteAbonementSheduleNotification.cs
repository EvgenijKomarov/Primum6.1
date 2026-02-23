using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Pushables.Notifications
{
    public class DeleteAbonementSheduleNotification : INotification
    {
        public required string StudentName { get; set; }

        public required int StudentUserId { get; set; }

        public required string TeacherName { get; set; }

        public required int TeacherUserId { get; set; }

        public required string CourseName { get; set; }

        public required int AbonementId { get; set; }

        public required int AbonementSheduleId { get; set; }

        public required string DayOfWeek { get; set; }

        public required int Time { get; set; }
    }
}
