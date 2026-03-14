using System;
using System.Collections.Generic;

namespace CoreDBModel.Models;

public partial class AbonementShedule: BaseEntity
{
    public int AbonementId { get; set; }

    public int TeacherSheduleId { get; set; }

    public DateTime LastIteration { get; set; }

    public virtual TeacherShedule TeacherShedule { get; set; } = null!;

    public virtual Abonement Abonement { get; set; } = null!;
}
