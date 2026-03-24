using CoreConnection.DTOs.Abstractions;
using CoreDBModel.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreConnection.DTOs
{
    public class LessonDto : AbstractLessonDto
    {
        public required DateTime DateTime {  get; set; }

        public required string? LessonLink { get; set; }

        public required float? Grade { get; set; }
    }
}
