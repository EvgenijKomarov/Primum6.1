using PrimumPlatformModel.Models.Enums;
using System;
using System.Collections.Generic;

namespace PrimumCore.Models;

public partial class Lesson
{
    public int LessonId { get; set; }

    public int AbonementId { get; set; }

    public int Price { get; set; }

    public string? StudentLink { get; set; }

    public string? TeacherLink { get; set; }

    public DateTime DateTime { get; set; }

    public LessonStatus Status { get; set; } = LessonStatus.Waiting;

    public virtual Abonement Abonement { get; set; } = null!;
}
