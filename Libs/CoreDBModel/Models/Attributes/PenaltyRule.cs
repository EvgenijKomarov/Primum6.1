using System;
using System.Collections.Generic;
using System.Text;

namespace CoreDBModel.Models.Attributes
{
    public class PenaltyRule: Attribute
    {
        public required Action<Lesson> Pardon { get; set; }

        public required Action<Lesson> LightPenalty { get; set; }

        public required Action<Lesson> HardPenalty { get; set; }

        public required Action<Lesson> BanUser { get; set; }
    }
}
