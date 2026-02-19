using BotCore.Engine.Entities;
using BotCore.Engine.Entities.Outputs;
using Engine;
using Engine.Nodes;

namespace BotCore.Engine.Nodes.EndpointNodes
{
    public class ProfileNode(): EndpointNode<DataBuffer, OutputMessage>("start")
    {
        public async override Task<INodeResult<DataBuffer, OutputMessage>> Invoke(DataBuffer input, CancellationToken? token = null)
        {
            return Finish(new OutputMessage
            {
                Message = $"Привет, {input.User?.DisplayName}!"
            });
        }
    }
}
