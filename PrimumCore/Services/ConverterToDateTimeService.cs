using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimumCore.Services
{
    public class ConverterToDateTimeService(int blockedDays = 0, ILogger<ConverterToDateTimeService>? _logger = null)
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

        public DayOfWeek GetDayOfWeek(string rusDOW)
        {
            var dow = weekDays[rusDOW];
            _logger?.LogInformation($"Converted {rusDOW} to {dow}");
            return dow;
        }

        public string GetRusTranslation(DayOfWeek dow)
        {
            var translation = weekDays.First(x => x.Value == dow).Key;
            _logger?.LogInformation($"Translated {dow} to {translation}");
            return translation;
        }

        public DateTime GetNextSuitableDateThisWeek(string rusDOW, int hours)
        {
            DayOfWeek dow = weekDays[rusDOW];
            DateTime now = DateTime.Now;
            var date = now.Date.AddDays(rusOrder[dow] - rusOrder[now.DayOfWeek]).AddHours(hours);
            _logger?.LogInformation($"Next suitable date in this week for {rusDOW}, {hours} is {date.ToString("HH.mm dd.MM.yyyy")}");
            return date;
        }

        public virtual DateTime GetNextFreeSuitableDate(string rusDOW, int hours)
        {
            var date = GetNextSuitableDateThisWeek(rusDOW, hours);
            var foundDate = (date - DateTime.Now).Days > blockedDays ? date : date.AddDays(7);
            _logger?.LogInformation($"Next suitable date in this week for {rusDOW}, {hours} is {foundDate.ToString("HH.mm dd.MM.yyyy")}");
            return foundDate;
        }
    }
}
