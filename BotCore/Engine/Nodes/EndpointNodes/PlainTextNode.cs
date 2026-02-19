using BotCore.Engine.Entities;
using BotCore.Engine.Entities.Outputs;
using Engine;
using Engine.Nodes;

namespace BotCore.Engine.Nodes.EndpointNodes
{
    public class PlainTextNode(): EndpointNode<DataBuffer, OutputMessage>("text")
    {
        public async override Task<INodeResult<DataBuffer, OutputMessage>> Invoke(DataBuffer input, CancellationToken? token = null)
        {
            return Complete(new OutputMessage
            {
                Message = "Hello"
            });
        }
    }
}
