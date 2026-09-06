using CoreConnection.DTOs.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreConnection.DTOs
{
    public class TeacherRankDto //не наследуется от IHasId чтобы не сортировалось по общим правилам
    {
        public required int Id { get; set; }

        public required int Level { get; set; }

        public required string Rank { get; set; } = null!;

        public required int RequiredExperience { get; set; }

        public required float EarningMultiplier { get; set; }
    }
}
