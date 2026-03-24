using CoreConnection.DTOs.Abstractions;
using CoreDBModel.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreConnection.DTOs
{
    public class FutureLessonDto : AbstractLessonDto
    {
        public required TimeSpan Time { get; set; }
    }
}
