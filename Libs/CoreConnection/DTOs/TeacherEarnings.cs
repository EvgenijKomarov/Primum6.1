using System;
using System.Collections.Generic;
using System.Text;

namespace CoreConnection.DTOs
{
    public class TeacherEarningDto
    {
        public decimal TotalEarningMultiplier { get; set; }

        public decimal EarningMultiplierByRank { get; set; }

        public decimal EarningMultiplierByConvertion { get; set; }
    }
}
