using BotCore.Engine.Entities;
using BotCore.Engine.Entities.Outputs;
using CoreConnection;
using CoreConnection.DTOs;
using Engine;
using Engine.Nodes;
using Resourses;
using System.Text;

namespace BotCore.Engine.Nodes.EndpointNodes
{
    public class TeacherFutureLessonsNode(TeacherClient client) : EndpointNode<DataBuffer, EngineOutputMessage>("tchFutureLessons")
    {
        public async override Task<INodeResult<DataBuffer, EngineOutputMessage>> Invoke(DataBuffer input, CancellationToken? token = null)
        {
            var lessonsByDate = (await client.FutureLessonsAsync(input.UserId!.Value)).Items ?? new List<LessonsByDateDto>();
            StringBuilder sb = new StringBuilder();
            foreach (var date in lessonsByDate)
            {
                sb.AppendLine($"{Emoticons.Date}{DayOfWeekRes.ResourceManager.GetString(date.DayOfWeek.ToString())} ({date.Date.ToString("dd.MM")})");
                foreach (var lesson in date.Lessons)
                {
                    sb.AppendLine($"[{lesson.Time.ToString("HH:mm")}] {lesson.StudentDisplayName} ({lesson.CourseName}) - " +
                        $"{LessonStatusRes.ResourceManager.GetString(lesson.LessonStatus.ToString()) ?? string.Empty}\n");
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
