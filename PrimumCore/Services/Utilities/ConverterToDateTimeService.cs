using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimumCore.Services.Utilities
{
    public class ConverterToDateTimeService(IConfiguration _configuration)
    {
        private int blockedDays = _configuration.GetValue<int>("Constants:BlockedDaysForLessonCreation");
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

        protected virtual DateTime GetCurrentTime() => DateTime.Now;

        public DayOfWeek GetDayOfWeek(string rusDOW)
        {
            var dow = weekDays[rusDOW];
            return dow;
        }

        public string GetRusTranslation(DayOfWeek dow)
        {
            var translation = weekDays.First(x => x.Value == dow).Key;
            return translation;
        }

        public DateTime GetNextSuitableDateThisWeek(DayOfWeek dayOfWeek, int hours)
        {
            DateTime now = GetCurrentTime();
            var date = now.Date.AddDays(rusOrder[dayOfWeek] - rusOrder[now.DayOfWeek]).AddHours(hours);
            return date;
        }

        public DateTime GetNextFreeSuitableDateThisWeek(DayOfWeek dayOfWeek, int hours)
        {
            DateTime now = GetCurrentTime();
            var date = now.Date.AddDays(rusOrder[dayOfWeek] - rusOrder[now.DayOfWeek]).AddHours(hours);
            date = (date - GetCurrentTime()).Days > blockedDays ? date : date.AddDays(7);
            return date;
        }
    }
}
