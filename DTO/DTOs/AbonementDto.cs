using CoreConnection.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreConnection.DTOs
{
    public class AbonementDto
    {
        public string StudentDisplayName { get; set; } = null!;

        public int StudentId { get; set; }

        public string TeacherDisplayName { get; set; } = null!;

        public int TeacherId { get; set; }

        public string CourseName { get; set; } = null!;

        public int? CourseId { get; set; }

        public string CourseThemeName { get; set; } = null!;

        public int AbonementId { get; set; }

        public int CourseThemeId { get; set; }

        public int PricePerLesson { get; set; }

        public AbonementStatusDto AbonementStatus { get; set; }
    }
}
