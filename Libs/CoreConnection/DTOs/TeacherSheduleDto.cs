using CoreConnection.DTOs.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreConnection.DTOs
{
    public class TeacherSheduleDto : IHasId
    {
        public required int Id {  get; set; }

        public required DayOfWeek DayOfWeek { get; set; }

        public required int Time { get; set; }

        public required bool IsAvailable { get; set; }

        public required string? StudentName { get; set; }

        public required int? StudentId { get; set; }

        public required string? CourseName { get; set; }

        public required int? CourseId { get; set; }

        public required int? AbonementId { get; set; }
    }
}
