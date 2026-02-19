using BotCore.Entities.Engine;
using BotCore.Entities.Engine.Outputs;
using Engine;
using Engine.Nodes;

namespace BotCore.Nodes.EndpointNodes
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
