using Microsoft.Extensions.Options;

namespace PrimumCore.Constants
{
    public class MathFormulas
    {
        private const int StudentLessonExpFloor = 100;
        private const int CourseGainExp = 450;
        private const int TeacherGainExp = 300;

        public int CoinFormula(float finalGrade, decimal lessonCost)
        {
            const float maximumCashback = 0.1f;
            const int maximumGradeValue = 5;

            float cashBackIndex = (finalGrade / maximumGradeValue * maximumCashback);

            return (int)(lessonCost * (decimal)cashBackIndex);
        }

        public int StudentExpFormula(float finalGrade)
        {
            return (int)(finalGrade * 100) + StudentLessonExpFloor;
        }

        public int CourseExpFormula()
        {
            return CourseGainExp;
        }

        public int TeacherExpFormula()
        {
            return TeacherGainExp;
        }
    }
}
