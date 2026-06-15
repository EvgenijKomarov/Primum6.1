using CoreConnection.DTOs.Inputs;
using CoreDBModel.Models;
using CoreDBModel.Models.Enums;
using Microsoft.EntityFrameworkCore;
using PrimumCore.Constants;
using PrimumCore.Exceptions;
using PrimumCore.Extentions;
using PublishServiceConnection;
using PublishServiceConnection.Events;

namespace PrimumCore.Services.Iterators
{
    public class GradingIterator(DatabaseIterator dbIterator, PublisherService publisherService, MathFormulas formulas)
    {
        public async Task<int> GradeLesson(int teacherId, int lessonId, GradingInputDto dto)
        {
            var lesson = await dbIterator.Lessons()
                .One(x => x.Id == lessonId);
            if (lesson.Abonement.Student.User.Id == teacherId) { throw new BusinessLogicException("Teacher can't grade this lesson"); }
            if (lesson.Grading is not null) { throw new BusinessLogicException("Lesson already gradet"); }
            if (lesson.Status != LessonStatus.Happened) { throw new BusinessLogicException("Lesson doesn't happened"); }

            var lessonGrading = new StudentGrading
            {
                HomeworkGrade = dto.HomeworkGrade,
                LessonActivityGrade = dto.LessonActivityGrade,
                RepetitionOfMaterialGrade = dto.RepetitionOfMaterialGrade,
                StudyInitiativeGrade = dto.StudyInitiativeGrade
            };
            lesson.Grading = lessonGrading;

            var avgGrade = lessonGrading.GetFinalGrade();
            var courseExp = formulas.CourseExpFormula();
            var studentExp = formulas.StudentExpFormula(avgGrade);
            var teacherExp = formulas.TeacherExpFormula();

            //if lesson is free -> x2 exp for teacher
            if (lesson.Price == 0)
            {
                courseExp = courseExp * 2;
                teacherExp = teacherExp * 2;
            }

            var addedCoins = formulas.CoinFormula(avgGrade, lesson.Price);

            lesson.Abonement.Student.Coins += addedCoins;
            lesson.Abonement.Student.Experience += studentExp;
            lesson.Abonement.Course.Experience += courseExp;
            lesson.Abonement.Course.Teacher.Experience += teacherExp;

            await dbIterator.AddAsync(lessonGrading);
            await dbIterator.SaveChangesAsync();

            await publisherService.Push(new LessonGradedEvent
            {
                CourseId = lesson.Abonement.Course.Id,
                CourseName = lesson.Abonement.Course.Name,
                StudentDisplayName = lesson.Abonement.Student.User.DisplayName,
                StudentUserId = lesson.Abonement.Student.User.Id,
                DateTime = lesson.DateTime,
                TeacherDisplayName = lesson.Abonement.Course.Teacher.User.DisplayName,
                TeacherUserId = lesson.Abonement.Course.Teacher.User.Id,
                Grade = avgGrade,
                EarnedCoins = addedCoins,
                CourseExp = courseExp,
                TeacherExp = teacherExp,
                StudentExp = studentExp
            });

            return lesson.Id;
        }
    }
}
