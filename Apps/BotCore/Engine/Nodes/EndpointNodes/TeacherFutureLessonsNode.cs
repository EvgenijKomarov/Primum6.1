using BotCore.Engine.Entities;
using BotCore.Engine.Entities.Outputs;
using Common.Utilities;
using CoreConnection;
using CoreConnection.DTOs;
using Engine;
using Engine.Nodes;
using Resourses;
using System.ComponentModel;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BotCore.Engine.Nodes.EndpointNodes
{
    public class TeacherFutureLessonsNode(
        TeacherClient client, 
        UserClient userClient,
        ConverterToDateTimeService datetimeService) : EndpointNode<DataBuffer, EngineOutputMessage>("tchFutureLessons")
    {
        private record AdjustedLesson(DateOnly Date, int Hour, FutureLessonDto Lesson);

        public async override Task<INodeResult<DataBuffer, EngineOutputMessage>> Invoke(DataBuffer input, CancellationToken? token = null)
        {
            var user = await userClient.ProfileAsync(input.UserId!.Value);
            var lessonsByDate = (await client.FutureLessonsAsync(input.UserId!.Value)).Items ?? new List<LessonsByDateDto>();

            // 1. Переводим все уроки в часовой пояс пользователя (минуты не трогаем, берём как есть)
            var adjustedLessons = lessonsByDate
                .SelectMany(date => date.Lessons.Select(lesson =>
                {
                    var (adjDate, adjHour) = datetimeService.ApplyTimeZoneOffset(date.Date, lesson.Time.Hours, user.TimezoneOffset);
                    return new AdjustedLesson(adjDate, adjHour, lesson);
                }))
                .ToList();

            StringBuilder sb = new StringBuilder();

            // 2. Перегруппировываем по новой (скорректированной) дате и сортируем
            var groupedByDate = adjustedLessons
                .GroupBy(x => x.Date)
                .OrderBy(g => g.Key);

            foreach (var group in groupedByDate)
            {
                sb.AppendLine($"{Emoticons.Date}{datetimeService.GetRusTranslation(group.Key.DayOfWeek)} ({group.Key:dd.MM})");

                foreach (var item in group.OrderBy(x => x.Hour).ThenBy(x => x.Lesson.Time.Minutes))
                {
                    var adjustedTime = new TimeSpan(item.Hour, item.Lesson.Time.Minutes, 0);
                    sb.AppendLine($"{Emoticons.Lesson}[{adjustedTime.ToString(@"hh\:mm")}] {item.Lesson.StudentDisplayName} ({item.Lesson.CourseName}) - " +
                        $"{LessonStatusRes.ResourceManager.GetString(item.Lesson.LessonStatus.ToString()) ?? string.Empty}\n");
                }
                sb.AppendLine("\n");
            }

            return Finish(new EngineOutputMessage
            {
                Message = lessonsByDate.Count() == 0 ? $"{Emoticons.Lesson}Занятий в ближайшее время не запланировано" : sb.ToString(),
                Buttons = new EngineOutputButton[]
                {
                new EngineOutputButton
                {
                    Text = $"{Emoticons.Back}Назад",
                    EndpointNode = typeof(TeacherProfileNode)
                }
                }
            });
        }
    }
}
