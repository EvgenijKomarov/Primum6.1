using PrimumCore.Models.Enums;

namespace PrimumCore.Models
{
    public class StudentGrading
    {
        public int StudentGradingId { get; set; }

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

            return gradeSum / gradeCount;
        }
    }
}
