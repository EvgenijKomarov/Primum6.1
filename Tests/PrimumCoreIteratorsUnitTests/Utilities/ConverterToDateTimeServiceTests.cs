using Moq;
using PrimumCore.Services.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Common.Utilities;

namespace UnitTests.Utilities
{
    public class ConverterToDateTimeServiceTests
    {
        private TestableConverterToDateTimeService _service;

        // Наследник для тестов
        private class TestableConverterToDateTimeService : ConverterToDateTimeService
        {
            public DateTime FixedNow { get; set; }
            public TestableConverterToDateTimeService(IConfiguration config, DateTime fixedNow)
                : base()
            {
                FixedNow = fixedNow;
            }
            protected override DateTime GetCurrentTime() => FixedNow;
        }

        [SetUp]
        public void Setup()
        {
            var config = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                    {
                        { "Constants:BlockedDaysForLessonCreation", "3" }
                    })
                .Build();

            _service = new TestableConverterToDateTimeService(config, new DateTime(2026, 1, 28, 10, 0, 0));
        }

        #region GetDayOfWeek

        [Test]
        public void GetDayOfWeek_ValidRussianName_ReturnsCorrectDayOfWeek()
        {
            Assert.That(_service.GetDayOfWeek("Понедельник"), Is.EqualTo(DayOfWeek.Monday));
            Assert.That(_service.GetDayOfWeek("Воскресенье"), Is.EqualTo(DayOfWeek.Sunday));
        }

        [Test]
        public void GetDayOfWeek_InvalidName_ThrowsKeyNotFoundException()
        {
            Assert.Throws<KeyNotFoundException>(() => _service.GetDayOfWeek("Неделя"));
        }

        #endregion

        #region GetRusTranslation

        [Test]
        public void GetRusTranslation_ValidDayOfWeek_ReturnsCorrectTranslation()
        {
            Assert.That(_service.GetRusTranslation(DayOfWeek.Monday), Is.EqualTo("Понедельник"));
            Assert.That(_service.GetRusTranslation(DayOfWeek.Sunday), Is.EqualTo("Воскресенье"));
        }

        #endregion

        #region GetNextSuitableDateThisWeek

        [Test]
        public void GetNextSuitableDateThisWeek_SameDay_ReturnsTodayAtGivenHour()
        {
            // Сегодня — среда
            _service.FixedNow = new DateTime(2026, 1, 28, 10, 0, 0); // среда

            var result = _service.GetNextSuitableDateThisWeek(DayOfWeek.Wednesday, 15);

            Assert.That(result, Is.EqualTo(new DateTime(2026, 1, 28, 15, 0, 0)));
        }

        [Test]
        public void GetNextSuitableDateThisWeek_DifferentDay_ReturnsThisWeekDate()
        {
            // Сегодня — среда (28 янв 2026)
            _service.FixedNow = new DateTime(2026, 1, 28, 10, 0, 0);

            var result = _service.GetNextSuitableDateThisWeek(DayOfWeek.Friday, 9);

            Assert.That(result, Is.EqualTo(new DateTime(2026, 1, 30, 9, 0, 0))); // пятница той же недели
        }

        [Test]
        public void GetNextSuitableDateThisWeek_PastDayInWeek_ReturnsPreviousWeek()
        {
            // Сегодня — среда
            _service.FixedNow = new DateTime(2026, 1, 28, 10, 0, 0);

            var result = _service.GetNextSuitableDateThisWeek(DayOfWeek.Monday, 14);

            // Понедельник "этой недели" — это 26 января (прошедший)
            Assert.That(result, Is.EqualTo(new DateTime(2026, 1, 26, 14, 0, 0)));
        }

        #endregion

        #region GetNextFreeSuitableDateThisWeek

        [Test]
        public void GetNextFreeSuitableDateThisWeek_DateWithinBlockedDays_AddsOneWeek()
        {
            // BlockedDays = 3
            // Сегодня — вторник, 27 января 2026
            _service.FixedNow = new DateTime(2026, 1, 27, 10, 0, 0);

            // Ищем четверг (29 янв) — это через 2 дня → меньше 3 → заблокировано
            var result = _service.GetNextFreeSuitableDateThisWeek(DayOfWeek.Thursday, 10);

            // Должен вернуть четверг следующей недели: 5 февраля
            Assert.That(result, Is.EqualTo(new DateTime(2026, 2, 5, 10, 0, 0)));
        }

        [Test]
        public void GetNextFreeSuitableDateThisWeek_DateBeyondBlockedDays_ReturnsThisWeek()
        {
            // BlockedDays = 3
            _service.FixedNow = new DateTime(2026, 1, 27, 10, 0, 0); // вторник

            // Ищем субботу (31 янв) — через 4 дня → больше 3 → разрешено
            var result = _service.GetNextFreeSuitableDateThisWeek(DayOfWeek.Saturday, 10);

            Assert.That(result, Is.EqualTo(new DateTime(2026, 1, 31, 10, 0, 0)));
        }

        [Test]
        public void GetNextFreeSuitableDateThisWeek_PastDayInWeek_AlwaysAddsWeek()
        {
            // Сегодня — среда
            _service.FixedNow = new DateTime(2026, 1, 28, 10, 0, 0);

            // Понедельник уже прошёл → дата в прошлом → (date - now).Days < 0 → точно <= blockedDays
            var result = _service.GetNextFreeSuitableDateThisWeek(DayOfWeek.Monday, 14);

            // Должен вернуть понедельник СЛЕДУЮЩЕЙ недели: 2 февраля
            Assert.That(result, Is.EqualTo(new DateTime(2026, 2, 2, 14, 0, 0)));
        }

        #endregion
    }
}
