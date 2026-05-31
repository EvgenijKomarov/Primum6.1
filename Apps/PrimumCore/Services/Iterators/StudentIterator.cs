using Common.Utilities;
using CoreConnection.DTOs;
using CoreDBModel.Constants;
using CoreDBModel.Models;
using CoreDBModel.Models.Enums;
using Microsoft.EntityFrameworkCore;
using PrimumCore.Exceptions;
using PrimumCore.Extentions;
using PublishServiceConnection;
using PublishServiceConnection.Events;

namespace PrimumCore.Services.Iterators
{
    public class StudentIterator(DatabaseIterator dbIterator, ConverterToDateTimeService dateTimeService, PublisherService publisher)
    {
        public async Task<StudentProfileDto> GetStudentProfile(int studentId)
        {
            var student = await dbIterator.Students()
                .One(x => x.User.Id == studentId);

            return new StudentProfileDto 
            { 
                DisplayName = student.User.DisplayName,
                UserId = student.User.Id,
                Coins = student.Coins,
                Rating = student.Rating,
                Level = student.Rank.Level,
                Rank = student.Rank.Rank,
                Cash = student.Cash,
                Experience = student.Experience,
            };
        }

        public async Task<decimal> AddCash(int studentId, decimal amount)
        {
            var student = await dbIterator.Students()
                .One(x => x.User.Id == studentId);

            student.Cash += amount;

            await dbIterator.SaveChangesAsync();
            return student.Cash;
        }

        public async Task<int> SubscribeToCourse(int studentId, int courseId, int teacherSheduleId)
        {
            var student = await dbIterator.Students()
                .Include(x => x.Abonements)
                .ThenInclude(x => x.AbonementShedules)
                .ThenInclude(x => x.TeacherShedule)
                .One(x => x.User.Id == studentId);

            var course = await dbIterator.Courses(false)
                .One(x => x.Id == courseId);

            // strict availability check executed in-memory
            if (!AvailabilityExpressions.IsCourseAvailable.Compile()(course)) { throw new NotFoundException("Course"); }

            var teacherShedule = await dbIterator.TeacherShedules(true)
                .One(x => x.Id == teacherSheduleId);
            if (teacherShedule.Teacher.ApproveStatus != ApproveStatus.Approved) { throw new NotAvailableException("Teacher is not approved"); }
            if (!AvailabilityExpressions.IsTeacherSheduleAvailable.Compile()(teacherShedule)) { throw new BusinessLogicException("Shedule is busy"); }
            if (teacherShedule.Teacher.User?.Id == studentId) { throw new BusinessLogicException("Student can't subscribe on himself"); }
            if (student
                .Abonements
                .SelectMany(x => x.AbonementShedules)
                .Any(x => x.TeacherShedule.DayOfWeek == teacherShedule.DayOfWeek && x.TeacherShedule.Time == teacherShedule.Time))
            {
                throw new BusinessLogicException("Same shedule already exists");
            }

            var abonement = student.Abonements.FirstOrDefault(a => a.CourseId == courseId);

            if (abonement is null)
            {
                abonement = new Abonement
                {
                    Course = course,
                    PricePerLesson = course.Price,
                    FreeLessons = course.FreeLessons
                };
                student.Abonements.Add(abonement);
            } else if (abonement.AbonementStatus == AbonementStatus.Deleted)
            {
                abonement.AbonementStatus = AbonementStatus.Active;
            }

            if (course.MaxLessons <= abonement.AbonementShedules.Count)
            {
                throw new BusinessLogicException("Can't create more shedules than course's maximum shedules per week");
            }

            var suitableDate = dateTimeService.GetNextFreeSuitableDateThisWeek(teacherShedule.DayOfWeek, teacherShedule.Time);
            var abonementShedule = new AbonementShedule
            {
                TeacherShedule = teacherShedule,
                LastIteration = suitableDate,
            };
            abonement.AbonementShedules.Add(abonementShedule);

            await dbIterator.AddAsync(abonementShedule);

            if (!dbIterator.Lessons()
                .Any(x => x.DateTime == suitableDate && x.Abonement.Id == abonement.Id))
            {
                await dbIterator.AddAsync(new Lesson
                {
                    Abonement = abonement,
                    Price = abonement.Course.FreeLessons >= abonement.Lessons.Count() ? 0 : abonement.PricePerLesson,
                    DateTime = suitableDate,
                    Status = LessonStatus.Waiting
                });
            }

            await dbIterator.SaveChangesAsync();

            await publisher.Push(new NewAbonementSheduleEvent
            {
                StudentName = student.User.DisplayName,
                StudentUserId = student.User.Id,
                TeacherName = teacherShedule.Teacher.User.DisplayName,
                TeacherUserId = teacherShedule.Teacher.User.Id,
                CourseName = course.Name,
                AbonementId = abonement.Id,
                AbonementSheduleId = abonementShedule.Id,
                DayOfWeek = teacherShedule.DayOfWeek.ToString(),
                Time = teacherShedule.Time
            });

            return abonementShedule.Id;
        }
    }
}
