using BotCore.Entities;
using BotCore.Entities.Outputs;
using Engine;

namespace BotCore.Nodes.EndpointNodes
{
    public class PlainTextNode: EndpointNode<DataBuffer, OutputMessage>
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
