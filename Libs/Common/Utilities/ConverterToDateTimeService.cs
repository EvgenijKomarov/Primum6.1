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
    }
}
