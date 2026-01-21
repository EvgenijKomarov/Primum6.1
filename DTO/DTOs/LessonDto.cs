using CoreConnection.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreConnection.DTOs
{
    public class LessonDto
    {
        public DateTime DateTime {  get; set; }

        public string CourseName { get; set; }

        public int CourseId { get; set; }

        public string TeacherDisplayName { get; set; }

        public int TeacherId { get; set; }

        public string StudentDisplayName { get; set; }

        public int StudentId { get; set; }

        public string? LessonLink { get; set; }

        public int AbonementId { get; set; }

        public int Price { get; set; }

        public LessonStatusDto LessonStatus { get; set; }
    }
}
