using BotCore.Engine.Entities;
using BotCore.Engine.Entities.Outputs;
using CoreConnection;
using Engine;
using Engine.Nodes;

namespace BotCore.Engine.Nodes.EndpointNodes
{
    public class StudentAbonementDeleteNode(StudentClient client) : EndpointNode<DataBuffer, EngineOutputMessage>("stAbonDelete")
    {
        public async override Task<INodeResult<DataBuffer, EngineOutputMessage>> Invoke(DataBuffer input, CancellationToken? token = null) 
        {
            var abonId = input.Arguments[0];
            await client.AbonementDeleteAsync(input.UserId!.Value, int.Parse(abonId));

            return Next<StudentAbonementNode>(input);
        }
    }
}
