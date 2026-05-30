using CoreConnection.DTOs.Abstractions;
using CoreDBModel.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreConnection.DTOs
{
    public class CourseDto : IHasId, IHasLevel
    {
        public required int Id { get; set; }

        public required string Name { get; set; }

        public required string TeacherName { get; set; }

        public required string CourseThemeName { get; set; }

        public required string About { get; set; }

        public required int CourseThemeId {  get; set; }

        public required int TeacherId {  get; set; }

        public required decimal Price { get; set; }

        public required int MaxLessons { get; set; }

        public required int FreeLessons { get; set; }

        public required string TeacherAbout { get; set; }

        public required bool IsActive { get; set; }

        public required bool IsAvailable { get; set; }

        public required int Level { get; set; }

        public required string Rank { get; set; }

        public required int Experience { get; set; }
    }
}
