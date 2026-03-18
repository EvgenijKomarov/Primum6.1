using CoreDBModel.Models.Enums;
using System;
using System.Collections.Generic;

namespace CoreDBModel.Models;

public partial class TeacherProfile: BaseEntity
{
    public string About { get; set; } = null!;

    public int UserId { get; set; }

    public ApproveStatus ApproveStatus { get; set; } = ApproveStatus.NeedModeratorReview;

    public User User { get; set; } = null!;

    public int Experience { get; set; } = 0; 
    
    public int RankId { get; set; } = -1;

    public TeacherRank Rank { get; set; } = null!;

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();

    public virtual ICollection<TeacherShedule> TeacherShedules { get; set; } = new List<TeacherShedule>();
}
