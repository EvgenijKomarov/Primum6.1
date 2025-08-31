using PrimumPlatformModel.Models.Enums;
using System;
using System.Collections.Generic;

namespace PrimumCore.Models;

public partial class TeacherProfile
{
    public int TeacherId { get; set; }

    public string? About { get; set; }

    public int UserId { get; set; }

    public ApproveStatus ApproveStatus { get; set; } = ApproveStatus.NeedModeratorReview;

    public User User { get; set; } = null!;

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();

    public virtual ICollection<TeacherShedule> TeacherShedules { get; set; } = new List<TeacherShedule>();
}
