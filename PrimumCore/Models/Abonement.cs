using PrimumPlatformModel.Models.Enums;
using System;
using System.Collections.Generic;

namespace PrimumCore.Models;

public partial class Abonement
{
    public int AbonementId { get; set; }

    public int CourseId { get; set; }

    public int StudentId { get; set; }

    public int PricePerLesson { get; set; }

    public AbonementStatus AbonementStatus { get; set; } = AbonementStatus.Active;

    public virtual Course Course { get; set; } = null!;

    public virtual StudentProfile Student { get; set; } = null!;

    public virtual ICollection<AbonementShedule> AbonementShedules { get; set; } = new List<AbonementShedule>();

    public virtual ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();

    public bool IsAvailable => AbonementStatus == AbonementStatus.Active;
}
