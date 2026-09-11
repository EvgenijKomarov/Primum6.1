using Common.Utilities;
using CoreDBModel.Models;
using CoreDBModel.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace SharedCoreBusinessLogic
{
    public class LessonBuilder(IQueryable<Lesson> lessons, ConverterToDateTimeService convertionService)
    {
        public decimal CalculatePrice(Abonement abonement) =>
        abonement.Course.FreeLessons > abonement.FreeLessonsSpent()
            ? 0m
            : abonement.PricePerLesson;

        /// <summary>
        /// Проверяет занятость слота. Если занят — либо кидает исключение,
        /// либо сдвигает на неделю вперёд (поведение задаётся вызывающим).
        /// </summary>
        protected virtual async Task<DateTime> ResolveSlot(int abonementId, DateTime desiredDateTime, SlotConflictPolicy policy)
        {
            var sameLesson = await lessons
                .Include(x => x.Abonement)
                .FirstOrDefaultAsync(x => x.DateTime == desiredDateTime && x.Abonement.Id == abonementId);

            if (sameLesson is null || !sameLesson.IsNormal())
                return desiredDateTime;

            return policy switch
            {
                SlotConflictPolicy.Throw => throw new ArgumentException("Invalid datetime"),
                SlotConflictPolicy.SkipWeek => desiredDateTime.AddDays(7),
                _ => throw new ArgumentOutOfRangeException(nameof(policy))
            };
        }

        /// <summary>
        /// Для создания занятий-отработок
        /// </summary>
        /// <param name="abonement"></param>
        /// <param name="dateTime"></param>
        /// <param name="policy"></param>
        /// <param name="isWorkoff"></param>
        /// <returns></returns>
        public virtual async Task<Lesson> Build(
            Abonement abonement, 
            DateTime dateTime, 
            SlotConflictPolicy policy, 
            bool isWorkoff = false)
        {
            var date = await ResolveSlot(abonement.Id, dateTime, policy);

            return new Lesson
            {
                Abonement = abonement,
                AbonementId = abonement.Id,
                Price = CalculatePrice(abonement),
                DateTime = date,
                Status = LessonStatus.Waiting,
                IsReferal = abonement.IsReferal,
                IsWorkoff = isWorkoff
            };
        }

        protected virtual DateTime GetCurrentTime() => DateTime.UtcNow;

        /// <summary>
        /// Должны быть подгружены абонемент и расписание препода
        /// </summary>
        /// <param name="abonementSchedule"></param>
        /// <param name="pickPolicy"></param>
        /// <param name="conflictPolicy"></param>
        /// <param name="isWorkoff"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public virtual async Task<Lesson> Build(
            AbonementShedule abonementSchedule, 
            SlotPickPolicy pickPolicy,
            SlotConflictPolicy conflictPolicy, 
            bool isWorkoff = false)
        {
            var date = pickPolicy switch
            {
                SlotPickPolicy.Nearest => convertionService.GetNextFreeSuitableDateThisWeek(abonementSchedule.TeacherShedule.DayOfWeek, abonementSchedule.TeacherShedule.Time),
                SlotPickPolicy.NextWeek => convertionService.GetNextSuitableDateNextWeek(abonementSchedule.TeacherShedule.DayOfWeek, abonementSchedule.TeacherShedule.Time),
                _ => throw new ArgumentOutOfRangeException(nameof(pickPolicy))
            };
            date = await ResolveSlot(abonementSchedule.Abonement.Id, date, conflictPolicy);

            abonementSchedule.LastIteration = GetCurrentTime();

            return new Lesson
            {
                Abonement = abonementSchedule.Abonement,
                AbonementId = abonementSchedule.Abonement.Id,
                Price = CalculatePrice(abonementSchedule.Abonement),
                DateTime = date,
                Status = LessonStatus.Waiting,
                IsReferal = abonementSchedule.Abonement.IsReferal,
                IsWorkoff = isWorkoff
            };
        }
    }

    public enum SlotConflictPolicy { Throw, SkipWeek }
    public enum SlotPickPolicy { Nearest, NextWeek }
}
