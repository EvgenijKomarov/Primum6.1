using System;
using System.Collections.Generic;

namespace PrimumCore.Models;

public partial class Course
{
    public int CourseId { get; set; }

    public string Name { get; set; } = null!;

    public int TeacherId { get; set; }

    public int Price { get; set; }

    public int MaxLessons { get; set; }

    public int FreeLessons { get; set; }

    public int ApproveStatus { get; set; }

    public virtual ICollection<Abonement> Abonements { get; set; } = new List<Abonement>();

    public virtual TeacherProfile Teacher { get; set; } = null!;
}
