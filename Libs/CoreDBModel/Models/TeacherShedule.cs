using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreDBModel.Models;

public partial class TeacherShedule: BaseEntity
{
    public int TeacherId { get; set; }

    public DayOfWeek DayOfWeek { get; set; }

    public int Time { get; set; }

    public virtual TeacherProfile Teacher { get; set; } = null!;

    public virtual AbonementShedule? AbonementShedule { get; set; }
}
