using CoreConnection.DTOs.Inputs;
using Microsoft.EntityFrameworkCore;
using PrimumCore.Models;
using PrimumCore.Models.Enums;
using PrimumPlatformModel.Models.Enums;

namespace PrimumCore.Services.Iterators
{
    public class GradingIterator(IPrimumContext context)
    {
        public async Task<int> GradeLesson(int teacherId, int lessonId, GradingInputDto dto)
        {
            var lesson = await context.Set<Lesson>()
                .Include(x => x.Grading)
                .Include(x => x.Abonement)
                .ThenInclude(x => x.Course)
                .ThenInclude(x => x.Teacher)
                .ThenInclude(x => x.User)
                .Include(x => x.Abonement)
                .ThenInclude(x => x.Student)
                .FirstOrDefaultAsync(x => x.LessonId == lessonId);
            if (lesson == null) { throw new Exception("Lesson not found"); }
            if (lesson.Abonement.Course.Teacher.User.Id == teacherId) { throw new Exception("Teacher can't grade this lesson"); }
            if (lesson.Grading is not null) { throw new Exception("Lesson already gradet"); }
            if (lesson.Status != LessonStatus.Happened) { throw new Exception("Lesson doesn't happened"); }

            var lessonGrading = new StudentGrading
            {
                HomeworkGrade = (Grading)dto.HomeworkGrade,
                LessonActivityGrade = (Grading)dto.LessonActivityGrade,
                RepetitionOfMaterialGrade = (Grading)dto.RepetitionOfMaterialGrade,
                StudyInitiativeGrade = (Grading)dto.StudyInitiativeGrade
            };
            lesson.Grading = lessonGrading;
            lesson.Abonement.Student.Coins += CoinFormula(lessonGrading.GetFinalGrade(), lesson.Price);

            return lesson.LessonId;
        }

        private int CoinFormula(float finalGrade, int lessonCost)
        {
            const float maximumCashback = 0.1f;
            const int maximumGradeValue = 5;

            float cashBackIndex = (finalGrade / maximumGradeValue * maximumCashback);

            return (int)(lessonCost * cashBackIndex);
        }
    }
}
