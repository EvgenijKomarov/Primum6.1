using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Entities
{
    public class TeacherEarningCalculation
    {
        public decimal TotalEarningMultiplier { get; set; }

        public decimal EarningMultiplierByRank { get; set; }

        public decimal EarningMultiplierByConvertion { get; set; }
    }
}
