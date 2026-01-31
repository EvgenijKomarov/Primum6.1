using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreConnection.DTOs.Inputs
{
    public class TeacherSheduleInputDto
    {
        public int Time {  get; set; }

        public DayOfWeek DayOfWeek { get; set; }
    }
}
