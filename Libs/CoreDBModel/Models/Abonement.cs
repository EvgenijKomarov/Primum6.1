using CoreDBModel.Models.Enums;
using System;
using System.Collections.Generic;

namespace CoreDBModel.Models;

public partial class Abonement: BaseEntity
{
    public int CourseId { get; set; }

    public int StudentId { get; set; }

    public decimal PricePerLesson { get; set; }

    public int FreeLessons { get; set; }

    public float? Rating { get; set; } = null;

    public AbonementStatus AbonementStatus { get; set; } = AbonementStatus.Active;

    public virtual Course Course { get; set; } = null!;

    public virtual StudentProfile Student { get; set; } = null!;

    public virtual ICollection<AbonementShedule> AbonementShedules { get; set; } = new List<AbonementShedule>();

    public virtual ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
}
