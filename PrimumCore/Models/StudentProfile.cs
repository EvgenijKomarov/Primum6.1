using PrimumPlatformModel.Models.Enums;
using System;
using System.Collections.Generic;

namespace PrimumCore.Models;

public partial class StudentProfile
{
    public int StudentId { get; set; }

    public int UserId { get; set; }

    public ApproveStatus ApproveStatus { get; set; } = ApproveStatus.NeedModeratorReview;

    public User User { get; set; } = null!;

    public virtual ICollection<Abonement> Abonements { get; set; } = new List<Abonement>();
}
