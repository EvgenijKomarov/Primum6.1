using System;
using System.Collections.Generic;
using System.Text;

namespace CoreConnection.DTOs
{
    public class LessonsByDateDto
    {
        public required DateOnly Date { get; set; }

        public required List<FutureLessonDto> Lessons { get; set; } = null!;

        public required DayOfWeek DayOfWeek { get; set; }
    }
}
