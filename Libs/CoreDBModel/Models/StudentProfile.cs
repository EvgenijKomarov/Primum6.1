using CoreDBModel.Models.Enums;
using System;
using System.Collections.Generic;

namespace CoreDBModel.Models;

public partial class StudentProfile: BaseEntity
{
    public int UserId { get; set; }

    public int Coins { get; set; }

    public ApproveStatus ApproveStatus { get; set; } = ApproveStatus.NeedModeratorReview;

    public User User { get; set; } = null!;

    public virtual ICollection<Abonement> Abonements { get; set; } = new List<Abonement>();

    public virtual ICollection<Promocode> Promocodes { get; set; } = new List<Promocode>();
}
