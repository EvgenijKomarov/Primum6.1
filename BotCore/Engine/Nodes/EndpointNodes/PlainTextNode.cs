using BotCore.Engine.Entities;
using BotCore.Engine.Entities.Outputs;
using Engine;
using Engine.Nodes;

namespace BotCore.Engine.Nodes.EndpointNodes
{
    public class PlainTextNode(): EndpointNode<DataBuffer, EngineOutputMessage>("text")
    {
        public async override Task<INodeResult<DataBuffer, EngineOutputMessage>> Invoke(DataBuffer input, CancellationToken? token = null)
        {
            return Finish(new EngineOutputMessage
            {
                Message = "Hello"
            });
        }
    }
}
