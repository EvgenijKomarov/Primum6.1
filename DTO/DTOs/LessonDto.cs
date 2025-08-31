using DTO.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.DTOs
{
    public class LessonDto
    {
        public DateTime DateTime {  get; set; }

        public string CourseName { get; set; }

        public string TeacherName { get; set; }

        public string StudentName { get; set; }

        public string LessonLink { get; set; }

        public int AbonementId { get; set; }

        public int CourseId { get; set; }

        public LessonStatusDto LessonStatus { get; set; }
    }
}
