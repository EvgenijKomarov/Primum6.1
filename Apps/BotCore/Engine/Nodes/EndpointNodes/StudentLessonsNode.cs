using BotCore.Engine.Entities;
using BotCore.Engine.Entities.Outputs;
using BotCoreAPI.Resourses;
using CoreConnection;
using Engine;
using Engine.Nodes;
using System.Text;

namespace BotCore.Engine.Nodes.EndpointNodes
{
    public class StudentLessonsNode(StudentClient client) : EndpointNode<DataBuffer, EngineOutputMessage>("stLessons")
    {
        public async override Task<INodeResult<DataBuffer, EngineOutputMessage>> Invoke(DataBuffer input, CancellationToken? token = null) 
        { 
            var lessons = (await client.LessonsAsync(input.UserId!.Value))
                .Where(x => x.DateTime > DateTime.Now)
                .OrderBy(x => x.DateTime);
            StringBuilder sb = new StringBuilder();
            foreach (var lesson in lessons) 
            {
                sb.AppendLine($"[{lesson.DateTime.ToString("dd.MM HH:mm")}] {lesson.CourseName} - " +
                    $"{LessonStatusRes.ResourceManager.GetString(lesson.LessonStatus.ToString()) ?? string.Empty}\n");
            }
            return Finish(new EngineOutputMessage
            {
                Message = lessons.Count() == 0 ? "Занятий нет" : sb.ToString(),
                Buttons = new EngineOutputButton[]
                {
                    new EngineOutputButton
                    {
                        Text = $"{Emoticons.Back}Назад",
                        EndpointNode = typeof(StudentProfileNode)
                    }
                }
            });
        }
    }
}
