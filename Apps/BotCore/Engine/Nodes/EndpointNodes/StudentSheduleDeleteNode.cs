using BotCore.Engine.Entities;
using BotCore.Engine.Entities.Outputs;
using CoreConnection;
using Engine;
using Engine.Nodes;

namespace BotCore.Engine.Nodes.EndpointNodes
{
    public class StudentSheduleDeleteNode(StudentClient client) : EndpointNode<DataBuffer, EngineOutputMessage>("stSheduleDelete")
    {
        public async override Task<INodeResult<DataBuffer, EngineOutputMessage>> Invoke(DataBuffer input, CancellationToken? token = null)
        {
            var abonId = input.Arguments[0];
            var sheduleId = input.Arguments[1];

            await client.SheduleDeleteAsync(input.UserId!.Value, int.Parse(sheduleId));

            input.Arguments = new List<string> { abonId };
            return Next<StudentAbonementNode>(input);
        }
    }
}
