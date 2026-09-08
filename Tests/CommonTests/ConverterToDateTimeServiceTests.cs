using Common.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommonTests
{
    public class TestableConverter : ConverterToDateTimeService
    {
        public DateTime FixedNow { get; set; }
        protected override DateTime GetCurrentTime() => FixedNow;
    }

    [TestFixture]
    public class ConverterToDateTimeServiceTests
    {
        // 2024-01-01 — понедельник, удобная точка отсчёта

        [Test]
        public void NextWeek_SameDay_AddsSevenDays()
        {
            var sut = new TestableConverter { FixedNow = new DateTime(2024, 1, 1, 10, 0, 0, DateTimeKind.Utc) };
            var result = sut.GetNextSuitableDateNextWeek(DayOfWeek.Monday, 14);
            Assert.That(result, Is.EqualTo(new DateTime(2024, 1, 8, 14, 0, 0)));
        }

        [Test]
        public void NextWeek_TargetEarlierInWeek_ComputesCorrectDate()
        {
            // сегодня среда, хотим понедельник
            var sut = new TestableConverter { FixedNow = new DateTime(2024, 1, 3, 10, 0, 0, DateTimeKind.Utc) };
            var result = sut.GetNextSuitableDateNextWeek(DayOfWeek.Monday, 9);
            Assert.That(result, Is.EqualTo(new DateTime(2024, 1, 8, 9, 0, 0)));
        }

        [Test]
        public void NextWeek_FromSunday_TargetMonday_IsTomorrow()
        {
            // граничный случай: воскресенье -> понедельник это "завтра", но по факту следующая неделя
            var sut = new TestableConverter { FixedNow = new DateTime(2024, 1, 7, 8, 0, 0, DateTimeKind.Utc) }; // воскресенье
            var result = sut.GetNextSuitableDateNextWeek(DayOfWeek.Monday, 8);
            Assert.That(result, Is.EqualTo(new DateTime(2024, 1, 8, 8, 0, 0)));
        }

        [Test]
        public void ThisWeek_MoreThanBlockedDays_KeepsThisWeek()
        {
            var sut = new TestableConverter { FixedNow = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) };
            // до пятницы 4 дня — точно больше blockedDays=3
            var result = sut.GetNextFreeSuitableDateThisWeek(DayOfWeek.Friday, 9);
            Assert.That(result, Is.EqualTo(new DateTime(2024, 1, 5, 9, 0, 0)));
        }

        [Test]
        public void ThisWeek_LessThanBlockedDays_PushesToNextWeek()
        {
            var sut = new TestableConverter { FixedNow = new DateTime(2024, 1, 1, 10, 0, 0, DateTimeKind.Utc) };
            // до четверга ~2 дня 23 часа — меньше 3 суток
            var result = sut.GetNextFreeSuitableDateThisWeek(DayOfWeek.Thursday, 9);
            Assert.That(result, Is.EqualTo(new DateTime(2024, 1, 11, 9, 0, 0)));
        }

        [Test]
        public void ThisWeek_TruncationBug_WronglyPushesToNextWeek()
        {
            var sut = new TestableConverter { FixedNow = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) };
            var result = sut.GetNextFreeSuitableDateThisWeek(DayOfWeek.Thursday, 9);

            Assert.That(result, Is.EqualTo(new DateTime(2024, 1, 4, 9, 0, 0)));
        }

        [Test]
        public void ThisWeek_TargetAlreadyPassed_PushesToNextWeek()
        {
            var sut = new TestableConverter { FixedNow = new DateTime(2024, 1, 5, 10, 0, 0, DateTimeKind.Utc) }; // пятница
            var result = sut.GetNextFreeSuitableDateThisWeek(DayOfWeek.Monday, 9);
            Assert.That(result, Is.EqualTo(new DateTime(2024, 1, 8, 9, 0, 0)));
        }

        [TestCase("Понедельник", DayOfWeek.Monday)]
        [TestCase("Воскресенье", DayOfWeek.Sunday)]
        public void GetDayOfWeek_And_Translation_RoundTrip(string rus, DayOfWeek dow)
        {
            var sut = new ConverterToDateTimeService();
            Assert.That(sut.GetDayOfWeek(rus), Is.EqualTo(dow));
            Assert.That(sut.GetRusTranslation(dow), Is.EqualTo(rus));
        }
    }
}
