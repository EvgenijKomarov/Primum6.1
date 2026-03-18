using System;
using System.Collections.Generic;
using System.Text;

namespace CoreDBModel.Models
{
    public class StudentRank: BaseEntity
    {
        public int Level { get; set; }

        public string Rank { get; set; } = null!;

        public int RequiredExperience { get; set; } = 0;

        public float CoinDiscount { get; set; } = 0;

        public virtual ICollection<StudentProfile> Students { get; set; } = new List<StudentProfile>();
    }
}
