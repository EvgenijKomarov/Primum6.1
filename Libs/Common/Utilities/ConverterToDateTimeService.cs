using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Utilities
{
    public class ConverterToDateTimeService
    {
        private Dictionary<string, DayOfWeek> weekDays = new Dictionary<string, DayOfWeek>()
        {
            ["Понедельник"] = DayOfWeek.Monday,
            ["Вторник"] = DayOfWeek.Tuesday,
            ["Среда"] = DayOfWeek.Wednesday,
            ["Четверг"] = DayOfWeek.Thursday,
            ["Пятница"] = DayOfWeek.Friday,
            ["Суббота"] = DayOfWeek.Saturday,
            ["Воскресенье"] = DayOfWeek.Sunday
        };

        public virtual DayOfWeek GetDayOfWeek(string rusDOW)
        {
            var dow = weekDays[rusDOW];
            return dow;
        }

        public virtual string GetRusTranslation(DayOfWeek dow)
        {
            var translation = weekDays.First(x => x.Value == dow).Key;
            return translation;
        }



        private Dictionary<DayOfWeek, int> rusOrder = new Dictionary<DayOfWeek, int>()
        {
            [DayOfWeek.Monday] = 0,
            [DayOfWeek.Tuesday] = 1,
            [DayOfWeek.Wednesday] = 2,
            [DayOfWeek.Thursday] = 3,
            [DayOfWeek.Friday] = 4,
            [DayOfWeek.Saturday] = 5,
            [DayOfWeek.Sunday] = 6
        };

        protected virtual DateTime GetCurrentTime() => DateTime.UtcNow;

        public virtual DateTime GetNextSuitableDateNextWeek(DayOfWeek dayOfWeek, int hours)
        {
            DateTime now = GetCurrentTime();
            var date = now.Date.AddDays(rusOrder[dayOfWeek] - rusOrder[now.DayOfWeek]).AddDays(7).AddHours(hours);
            return date;
        }

        public virtual DateTime GetNextFreeSuitableDateThisWeek(DayOfWeek dayOfWeek, int hours, int blockedDays = 3)
        {
            DateTime now = GetCurrentTime();
            var date = now.Date.AddDays(rusOrder[dayOfWeek] - rusOrder[now.DayOfWeek]).AddHours(hours);
            date = (date - now).TotalDays > blockedDays ? date : date.AddDays(7);
            return date;
        }

        public virtual (DayOfWeek Day, int Hour) ApplyTimeZoneOffset(DayOfWeek day, int hour, TimeSpan offset)
        {
            if (hour < 0 || hour > 23)
                throw new ArgumentOutOfRangeException(nameof(hour), "Час должен быть в диапазоне 0-23");

            const int hoursInWeek = 7 * 24;

            // Переводим (день, час) в абсолютное количество часов от начала недели (Sunday = 0)
            int totalHours = (int)day * 24 + hour;

            // Добавляем смещение. Math.Floor гарантирует корректное округление
            // в меньшую сторону даже для отрицательных и дробных (например, +5:30) смещений.
            int offsetHours = (int)Math.Floor(offset.TotalHours);
            totalHours += offsetHours;

            // Приводим результат к диапазону [0, hoursInWeek) с корректной обработкой отрицательных значений
            int normalized = ((totalHours % hoursInWeek) + hoursInWeek) % hoursInWeek;

            DayOfWeek resultDay = (DayOfWeek)(normalized / 24);
            int resultHour = normalized % 24;

            return (resultDay, resultHour);
        }
    }
}
