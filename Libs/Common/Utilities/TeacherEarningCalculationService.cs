using Common.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Utilities
{
    public class EarningCalculationService
    {
        public const decimal EMBase = 0.3M;

        public const decimal EMByConvertionMax = 0.2M;
        public const decimal EMByConvertionMin = 0.0M;
        public const decimal EMByConvertionPerConvertionPercent = 0.004M;

        public const decimal EMByRankMax = 0.2M;
        public const decimal EMByRankMin = 0.0M;

        public const decimal EMBySeriesMax = 0.1M;
        public const decimal EMBySeriesMin = 0.0M;
        public const decimal EMBySeriesPerLesson = 0.025M;

        public const decimal TotalMax = 0.7M;
        public const decimal TotalMin = 0.3M;

        public TeacherEarningCalculation CalculateToTeacher(float? convertionIndex, float rankMultiplier)
        {
            decimal earningByConvertion;
            if (convertionIndex is not null)
            {
                earningByConvertion = EMByConvertionPerConvertionPercent * ((decimal)convertionIndex);
            }
            else
            {
                earningByConvertion = 0.1M;
            }

            var valueByRank = ValueBetween(EMByRankMin, EMByRankMax, (decimal)rankMultiplier);
            var valueByConvertion = ValueBetween(EMByConvertionMin, EMByConvertionMax, earningByConvertion);

            var valueTotal = ValueBetween(TotalMin, TotalMax, valueByRank + valueByConvertion + EMBase);

            return new TeacherEarningCalculation 
            { 
                TotalEarningMultiplier = valueTotal,
                EarningMultiplierByConvertion = valueByConvertion,
                EarningMultiplierByRank = valueByRank,
            };
        }

        public decimal CalculateTotalToLesson(float? convertionIndex, float rankMultiplier, int happenedLessonsCount)
        {
            var teacherEarnings = CalculateToTeacher(convertionIndex, rankMultiplier);

            var valueBySeries = ValueBetween(EMBySeriesMin, EMBySeriesMax, happenedLessonsCount * EMBySeriesPerLesson);
            return teacherEarnings.TotalEarningMultiplier + valueBySeries;
        }

        public decimal CalculateEarningsToLesson(decimal lessonPrice, float? convertionIndex, float rankMultiplier, int happenedPayedLessonsCount, bool isReferal)
        {
            if (isReferal) return lessonPrice * 0.8M; 
            return Math.Round(CalculateTotalToLesson(convertionIndex, rankMultiplier, happenedPayedLessonsCount) * lessonPrice, 2);
        }

        private decimal ValueBetween(decimal min, decimal max, decimal value)
        {
            return Math.Min(Math.Max(min, value), max);
        }
    }
}
