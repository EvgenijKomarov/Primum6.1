using CoreDBModel.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreConnection.DTOs.Abstractions
{
    public abstract class AbstractLessonDto: IHasId
    {
        public required string CourseName { get; set; }

        public required int CourseId { get; set; }

        public required int Id { get; set; }

        public required string TeacherDisplayName { get; set; }

        public required int TeacherId { get; set; }

        public required string StudentDisplayName { get; set; }

        public required int StudentId { get; set; }

        public required int AbonementId { get; set; }

        public required int Price { get; set; }

        public required LessonStatus LessonStatus { get; set; }
    }
}
