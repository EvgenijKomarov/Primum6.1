using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreConnection.DTOs
{
    public class SheduleDto
    {
        public DayOfWeek DayOfWeek { get; set; }

        public int Time { get; set; }

        public string StudentName { get; set; }
    }
}
