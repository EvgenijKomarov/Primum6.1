using CoreDBModel.Models.Enums;

namespace CoreDBModel.Models
{
    public class StudentGrading: BaseEntity
    {
        public int LessonId { get; set; }

        public virtual Lesson Lesson { get; set; } = null!;

        public Grading HomeworkGrade { get; set; } = Grading.None;

        public Grading LessonActivityGrade { get; set; } = Grading.None;

        public Grading RepetitionOfMaterialGrade { get; set; } = Grading.None;

        public Grading StudyInitiativeGrade { get; set; } = Grading.None;

        public float GetFinalGrade()
        {
            int gradeSum = 0;
            int gradeCount = 0;

            foreach(var grade in new List<Grading>
                { HomeworkGrade, LessonActivityGrade, RepetitionOfMaterialGrade, StudyInitiativeGrade })
            {
                if (grade != Grading.None) { gradeCount++; gradeSum += (int)grade; }
            }

            gradeCount = gradeCount == 0 ? 1 : gradeCount;

            return (float)Math.Round((float)gradeSum / gradeCount, 2);
        }
    }
}
