using BotCore.Engine.Entities;
using BotCore.Engine.Entities.Outputs;
using CoreConnection;
using Engine;
using Engine.Nodes;

namespace BotCore.Engine.Nodes.EndpointNodes
{
    public class StudentSubscribeCourseNode(StudentClient client) : EndpointNode<DataBuffer, EngineOutputMessage>("stCoursSubscr")
    {
        public async override Task<INodeResult<DataBuffer, EngineOutputMessage>> Invoke(DataBuffer input, CancellationToken? token = null)
        {
            var courseId = input.Arguments[0];
            var teacherId = input.Arguments[1];
            var sheduleId = input.Arguments[2];

            await client.CourseSubscribeAsync(input.UserId!.Value, int.Parse(courseId), int.Parse(sheduleId));

            input.Arguments = new List<string> { courseId, teacherId };
            return Next<StudentExploreTeacherShedulesNode>(input);
        }
    }
}
