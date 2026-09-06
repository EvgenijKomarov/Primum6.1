using CoreDBModel.Models.Enums;
using System;
using System.Collections.Generic;

namespace CoreDBModel.Models;

public partial class Course: BaseEntity
{
    public string Name { get; set; } = null!;

    public string About { get; set; } = null!;

    public int TeacherId { get; set; }

    public decimal Price { get; set; }

    public int MaxLessons { get; set; }

    public int FreeLessons { get; set; }

    public int CourseThemeId { get; set; }

    public bool IsActive { get; set; } = true;

    public int Experience { get; set; } = 0;

    public string ReferalToken { get; set; } = null!;

    public ApproveStatus ApproveStatus { get; set; } = ApproveStatus.NeedModeratorReview;

    public int RankId { get; set; } = -1;

    public CourseRank Rank { get; set; } = null!;

    public virtual ICollection<Abonement> Abonements { get; set; } = new List<Abonement>();

    public virtual TeacherProfile Teacher { get; set; } = null!;

    public virtual CourseTheme CourseTheme { get; set; } = null!;
}
