using CoreConnection.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreConnection.DTOs
{
    public class CourseDto
    {
        public required int CourseId { get; set; }

        public required string Name { get; set; }

        public required string TeacherName { get; set; }

        public required string CourseThemeName { get; set; }

        public required int CourseThemeId {  get; set; }

        public required int TeacherId {  get; set; }

        public required int Price { get; set; }

        public required int MaxLessons { get; set; }

        public required int FreeLessons { get; set; }

        public required string TeacherAbout { get; set; }

        public required bool IsActive { get; set; }

        public required StatusApprove ApproveStatus { get; set; }
    }
}
