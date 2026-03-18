using System;
using System.Collections.Generic;
using System.Text;

namespace CoreDBModel.Models
{
    public class CourseRank : BaseEntity
    {
        public int Level { get; set; }

        public string Rank { get; set; } = null!;

        public int RequiredExperience { get; set; } = 0;

        public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}
