using System;
using System.Collections.Generic;

namespace PrimumCore.Models;

public partial class TeacherShedule
{
    public int TeacherSheduleId { get; set; }

    public int TeacherId { get; set; }

    public string DayOfWeek { get; set; } = null!;

    public int Time { get; set; }

    public virtual TeacherProfile Teacher { get; set; } = null!;

    public virtual AbonementShedule? AbonementShedule { get; set; }
}
