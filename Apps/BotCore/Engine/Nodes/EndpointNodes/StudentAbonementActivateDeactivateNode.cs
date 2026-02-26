using BotCore.Engine.Entities;
using BotCore.Engine.Entities.Outputs;
using CoreConnection;
using CoreDBModel.Models.Enums;
using Engine;
using Engine.Nodes;

namespace BotCore.Engine.Nodes.EndpointNodes
{
    public class StudentAbonementActivateDeactivateNode(StudentClient client) : EndpointNode<DataBuffer, EngineOutputMessage>("stAbonActFr")
    {
        public async override Task<INodeResult<DataBuffer, EngineOutputMessage>> Invoke(DataBuffer input, CancellationToken? token = null)
        {
            var abonId = input.Arguments[0];

            var abon = await client.AbonementAsync(input.UserId!.Value, int.Parse(abonId));
            if (abon.AbonementStatus == AbonementStatus.Freezed) { await client.AbonementActivateAsync(input.UserId!.Value, int.Parse(abonId)); }
            else if (abon.AbonementStatus == AbonementStatus.Active) { await client.AbonementFreezeAsync(input.UserId!.Value, int.Parse(abonId)); }

            return Next<StudentAbonementNode>(input);
        }
    }
}
