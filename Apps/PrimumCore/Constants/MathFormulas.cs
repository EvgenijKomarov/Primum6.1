using Microsoft.Extensions.Options;
using PrimumCore.Options;

namespace PrimumCore.Constants
{
    public class MathFormulas(IOptions<CoreConstants> constants)
    {
        public int CoinFormula(float finalGrade, decimal lessonCost)
        {
            const float maximumCashback = 0.1f;
            const int maximumGradeValue = 5;

            float cashBackIndex = (finalGrade / maximumGradeValue * maximumCashback);

            return (int)(lessonCost * (decimal)cashBackIndex);
        }

        public int StudentExpFormula(float finalGrade)
        {
            return (int)(finalGrade * 100) + constants.Value.StudentLessonExpFloor;
        }

        public int CourseExpFormula()
        {
            return constants.Value.CourseGainExp;
        }

        public int TeacherExpFormula()
        {
            return constants.Value.TeacherGainExp;
        }
    }
}
