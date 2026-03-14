using CoreDBModel.Models.Enums;
using System;
using System.Collections.Generic;

namespace CoreDBModel.Models;

public partial class Lesson: BaseEntity
{
    public int AbonementId { get; set; }

    public int Price { get; set; }

    public string? StudentLink { get; set; }

    public string? TeacherLink { get; set; }

    public DateTime DateTime { get; set; }

    public LessonStatus Status { get; set; } = LessonStatus.Waiting;

    public virtual Abonement Abonement { get; set; } = null!;

    public virtual StudentGrading? Grading { get; set; }
}
