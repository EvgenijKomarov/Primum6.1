using System;
using System.Collections.Generic;
using System.Text;

namespace CoreDBModel.Models
{
    public class TeacherRank : BaseEntity
    {
        public int Level { get; set; }

        public string Rank { get; set; } = null!;

        public int RequiredExperience { get; set; } = 0;

        public float EarningMultiplier { get; set; } = 0;

        public virtual ICollection<TeacherProfile> Teachers { get; set; } = new List<TeacherProfile>();
    }
}
