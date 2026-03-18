using Microsoft.Extensions.Options;
using PrimumCore.Options;

namespace PrimumCore.Constants
{
    public class MathFormulas(IOptions<CoreConstants> constants)
    {
        private Random random = new Random();
        private int GetRandomExpBonus() => random.Next(0, 101);

        public int CoinFormula(float finalGrade, int lessonCost)
        {
            const float maximumCashback = 0.1f;
            const int maximumGradeValue = 5;

            float cashBackIndex = (finalGrade / maximumGradeValue * maximumCashback);

            return (int)(lessonCost * cashBackIndex);
        }

        public int StudentExpFormula(float finalGrade)
        {
            return (int)(finalGrade * 100) + constants.Value.StudentLessonExpFloor + GetRandomExpBonus();
        }

        public int CourseExpFormula()
        {
            return constants.Value.CourseGainExp + GetRandomExpBonus();
        }

        public int TeacherExpFormula()
        {
            return constants.Value.CourseGainExp + GetRandomExpBonus();
        }
    }
}
