using CoreDBModel.Models;
using CoreDBModel.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreDBIterator.Workers
{
    public class TeacherProfileRefreshExecutor(IServiceScopeFactory _serviceScopeFactory, ILogger<TeacherProfileRefreshExecutor> logger): BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                logger.LogInformation("Teacher profiles refresh running at: {time}", DateTimeOffset.Now);
                await Action();
                await Task.Delay(TimeSpan.FromMinutes(30), stoppingToken);
            }
        }

        public async Task Action()
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<PrimumContext>();

            var teacherProfiles = await context.Set<TeacherProfile>()
                .Include(x => x.User)
                .Where(x => !x.User.IsBanned)
                .Include(x => x.Courses)
                .ThenInclude(x => x.Abonements)
                .ThenInclude(x => x.Lessons)
                .ToArrayAsync();

            foreach (var profile in teacherProfiles) 
            {
                const int requiredAbonements = 10;

                var freePeriodCompleteAbonements = profile //выборка из свежих абонементов, прошедших бесплатный период
                    .Courses
                    .SelectMany(x => x.Abonements)
                    .Where(x => x.Lessons.Count >= x.FreeLessons)
                    .OrderByDescending(x => x.CreatedAt)
                    .Take(requiredAbonements)
                    .ToArray();

                if (freePeriodCompleteAbonements.Length < requiredAbonements) //если нет нужного количества - неопределен
                {
                    profile.ConvertionIndex = null;
                    break;
                }

                int allAbons = freePeriodCompleteAbonements.Length;
                int convertedAbons = freePeriodCompleteAbonements //считаем абонементы, где ученики оплатили занятие
                    .Count(x => 
                        x
                        .Lessons
                        .Any(x => 
                            x.Status == LessonStatus.Happened && x.Price != 0
                        )
                    );

                profile.ConvertionIndex = (float)convertedAbons / allAbons;
            }

            await context.SaveChangesAsync();
        }
    }
}
