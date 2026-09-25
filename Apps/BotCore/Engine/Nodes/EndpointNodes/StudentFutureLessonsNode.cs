using BotCore.Engine.Entities;
using BotCore.Engine.Entities.Outputs;
using Common.Utilities;
using CoreConnection;
using CoreConnection.DTOs;
using Engine;
using Engine.Nodes;
using Resourses;
using System.Text;

namespace BotCore.Engine.Nodes.EndpointNodes
{
    public class StudentFutureLessonsNode(
        StudentClient client, 
        ConverterToDateTimeService dateTimeConverter,
        UserClient userClient) : EndpointNode<DataBuffer, EngineOutputMessage>("stLessons")
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
                    var (adjDate, adjHour) = dateTimeConverter.ApplyTimeZoneOffset(date.Date, lesson.Time.Hours, user.TimezoneOffset);
                    return new AdjustedLesson(adjDate, adjHour, lesson);
                }))
                .ToList();

            StringBuilder sb = new StringBuilder();

            var groupedByDate = adjustedLessons
                .GroupBy(x => x.Date)
                .OrderBy(g => g.Key);

            foreach (var group in groupedByDate)
            {
                sb.AppendLine($"{Emoticons.Date}{dateTimeConverter.GetRusTranslation(group.Key.DayOfWeek)} ({group.Key:dd.MM})");
                foreach (var item in group.OrderBy(x => x.Hour).ThenBy(x => x.Lesson.Time.Minutes))
                {
                    var adjustedTime = new TimeSpan(item.Hour, item.Lesson.Time.Minutes, 0);
                    sb.AppendLine($"{Emoticons.Lesson}[{adjustedTime.ToString(@"hh\:mm")}] {item.Lesson.CourseName} - " +
                    $"{LessonStatusRes.ResourceManager.GetString(item.Lesson.LessonStatus.ToString()) ?? string.Empty}\n");
                }
                sb.AppendLine("\n");
            }
            return Finish(new EngineOutputMessage
            {
                Message = lessonsByDate.Count() == 0 ? $"{Emoticons.Lesson}Занятий в ближайшее время не запланировано" : sb.ToString(),
                Buttons = [
                    ..lessonsByDate.SelectMany(day => day.Lessons.Select(lesson => new EngineOutputButton
                    {
                        Text = $"{Emoticons.Cancel}Отменить {day.Date:dd.MM}({lesson.Time.ToString(@"hh\:mm")})",
                        EndpointNode = typeof(StudentCancelLessonNode),
                        Args = [lesson.Id.ToString()]
                    })),
                    new EngineOutputButton
                    {
                        Text = $"{Emoticons.Back}Назад",
                        EndpointNode = typeof(StudentProfileNode)
                    }
                ]
            });
        }
    }
}
