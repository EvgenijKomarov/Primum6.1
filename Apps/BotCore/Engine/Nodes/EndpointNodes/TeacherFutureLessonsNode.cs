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
            var lessons = (await client.FutureLessonsAsync(input.UserId!.Value)).Items ?? new List<LessonDto>();
            StringBuilder sb = new StringBuilder();
            foreach (var lesson in lessons)
            {
                sb.AppendLine($"[{lesson.DateTime.ToString("dd.MM HH:mm")}] {lesson.StudentDisplayName} ({lesson.CourseName}) - " +
                    $"{LessonStatusRes.ResourceManager.GetString(lesson.LessonStatus.ToString()) ?? string.Empty}\n");
            }
            return Finish(new EngineOutputMessage
            {
                Message = lessons.Count() == 0 ? $"{Emoticons.Lesson}Занятий в ближайшее время не запланировано" : sb.ToString(),
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
