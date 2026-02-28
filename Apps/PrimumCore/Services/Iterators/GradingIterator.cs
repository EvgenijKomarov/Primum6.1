using CoreConnection.DTOs.Inputs;
using CoreDBModel.Models;
using CoreDBModel.Models.Enums;
using Microsoft.EntityFrameworkCore;
using PrimumCore.Exceptions;
using Pushables;
using Pushables.Notifications;

namespace PrimumCore.Services.Iterators
{
    public class GradingIterator(PrimumContext context, PublisherService publisherService)
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
                .ThenInclude(x => x.User)
                .FirstOrDefaultAsync(x => x.LessonId == lessonId);
            if (lesson == null) { throw new NotFoundException("Lesson"); }
            if (lesson.Abonement.Student.User.Id == teacherId) { throw new BusinessLogicException("Teacher can't grade this lesson"); }
            if (lesson.Grading is not null) { throw new BusinessLogicException("Lesson already gradet"); }
            if (lesson.Status != LessonStatus.Happened) { throw new BusinessLogicException("Lesson doesn't happened"); }

            var lessonGrading = new StudentGrading
            {
                HomeworkGrade = (Grading)dto.HomeworkGrade,
                LessonActivityGrade = (Grading)dto.LessonActivityGrade,
                RepetitionOfMaterialGrade = (Grading)dto.RepetitionOfMaterialGrade,
                StudyInitiativeGrade = (Grading)dto.StudyInitiativeGrade
            };
            lesson.Grading = lessonGrading;

            var avgGrade = lessonGrading.GetFinalGrade();
            var addedCoins = CoinFormula(avgGrade, lesson.Price);

            lesson.Abonement.Student.Coins += addedCoins;

            context.Set<StudentGrading>().Add(lessonGrading);
            await context.SaveChangesAsync();

            await publisherService.Push(new LessonGradedNotification
            {
                CourseId = lesson.Abonement.Course.CourseId,
                CourseName = lesson.Abonement.Course.Name,
                StudentDisplayName = lesson.Abonement.Student.User.DisplayName,
                StudentUserId = lesson.Abonement.Student.User.Id,
                DateTime = lesson.DateTime,
                TeacherDisplayName = lesson.Abonement.Course.Teacher.User.DisplayName,
                TeacherUserId = lesson.Abonement.Course.Teacher.User.Id,
                Grade = avgGrade,
                EarnedCoins = addedCoins
            });

            return lesson.LessonId;
        }

        public int CoinFormula(float finalGrade, int lessonCost)
        {
            const float maximumCashback = 0.1f;
            const int maximumGradeValue = 5;

            float cashBackIndex = (finalGrade / maximumGradeValue * maximumCashback);

            return (int)(lessonCost * cashBackIndex);
        }
    }
}
