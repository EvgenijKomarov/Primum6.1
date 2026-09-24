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
    public class StudentFutureLessonsNode(StudentClient client, ConverterToDateTimeService dateTimeConverter) : EndpointNode<DataBuffer, EngineOutputMessage>("stLessons")
    {
        public async override Task<INodeResult<DataBuffer, EngineOutputMessage>> Invoke(DataBuffer input, CancellationToken? token = null) 
        {
            var lessonsByDate = (await client.FutureLessonsAsync(input.UserId!.Value)).Items ?? new List<LessonsByDateDto>();
            StringBuilder sb = new StringBuilder();
            foreach (var date in lessonsByDate)
            {
                sb.AppendLine($"{Emoticons.Date}{dateTimeConverter.GetRusTranslation(date.DayOfWeek)} ({date.Date.ToString("dd.MM")})");
                foreach (var lesson in date.Lessons)
                {
                    sb.AppendLine($"{Emoticons.Lesson}[{lesson.Time.ToString(@"hh\:mm")}] {lesson.CourseName} - " +
                    $"{LessonStatusRes.ResourceManager.GetString(lesson.LessonStatus.ToString()) ?? string.Empty}\n");
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
