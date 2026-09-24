using BotCore.Engine.Entities;
using BotCore.Engine.Entities.Outputs;
using CoreConnection;
using Engine;
using Engine.Nodes;

namespace BotCore.Engine.Nodes.EndpointNodes
{
    public class StudentCancelLessonNode(StudentClient client) : EndpointNode<DataBuffer, EngineOutputMessage>("stCancelLesson")
    {
        public async override Task<INodeResult<DataBuffer, EngineOutputMessage>> Invoke(DataBuffer input, CancellationToken? token = null)
        {
            var lessonId = input.Arguments[0];
            await client.LessonCancelAsync(input.UserId!.Value, int.Parse(lessonId));

            return Next<StudentFutureLessonsNode>(input);
        }
    }
}
