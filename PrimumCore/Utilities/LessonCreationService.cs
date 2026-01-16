using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PrimumCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimumCore.Services
{
    public class LessonCreationService(
        IPrimumContext context, 
        ConverterToDateTimeService _service,
        ILogger<LessonCreationService>? _logger = null
        )
    {
        private Timer _timer;

        public void Run()
        {
            _logger?.LogInformation("LessonCreationService is running...");
            _timer = new Timer(async _ => await IterateAsync(), null, TimeSpan.Zero, TimeSpan.FromHours(1));
        }

        public async Task IterateAsync()
        {
            _logger?.LogInformation("LessonCreationService is iterating database...");
            try
            {
                var availableForProlongation = await context.Set<AbonementShedule>()
                    .Where(s => s.LastIteration.AddDays(7) <= DateTime.Now)
                    .ToArrayAsync();

                if (availableForProlongation.Length > 0)
                {
                    _logger?.LogInformation($"LessonCheckerService found AbonementShedules for prolongation: " +
                        $"{availableForProlongation.Select(x => x.AbonementSheduleId).ToArray()}");
                }
                else
                {
                    _logger?.LogInformation($"LessonCheckerService found no AbonementShedules for prolongation");
                }

                foreach (var s in availableForProlongation)
                {
                    s.LastIteration = DateTime.Now;
                    _logger?.LogInformation($"Set LastIterationTime of {s.AbonementSheduleId} for {DateTime.Now}");
                    var lesson = new Lesson()
                    {
                        AbonementId = s.AbonementId,
                        //todo DateTime = _service.GetNextFreeSuitableDate(s.TeacherShedule.DayOfWeek, s.TeacherShedule.Time)
                    };
                    context.Set<Lesson>()
                        .Add(lesson);
                    _logger?.LogInformation($"Created lesson with Id: {lesson.LessonId} for {lesson.AbonementId} at {lesson.DateTime}");
                }

                await context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                _logger?.LogError(ex, $"LessonCreationService failed to iterate");
            }
        }
    }
}
