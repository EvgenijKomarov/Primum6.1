namespace CoreDBModel.Models
{
    public partial class CourseTheme
    {
        public int CourseThemeId { get; set; }

        public string ThemeName { get; set; } = null!;

        public bool IsActive { get; set; }

        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}
