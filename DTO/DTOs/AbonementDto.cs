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
        public required string StudentDisplayName { get; set; }

        public required int StudentId { get; set; }

        public required string TeacherDisplayName { get; set; }

        public required int TeacherId { get; set; }

        public required string CourseName { get; set; }

        public required int? CourseId { get; set; }

        public required string CourseThemeName { get; set; }

        public required int AbonementId { get; set; }

        public required int CourseThemeId { get; set; }

        public required int PricePerLesson { get; set; }

        public required StatusAbonement AbonementStatus { get; set; }
    }
}
