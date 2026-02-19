using BotCore.Entities.Engine;
using BotCore.Nodes.EndpointNodes;
using Engine;

namespace BotCore.Engine.Entities.Inputs
{
    public class PlainTextInput : CommandInput
    {
        public PlainTextInput(int? userId, string input)
        {
            EndpointNodeId = "text";
            Object = new DataBuffer(new List<string> { input });
            UserId = userId;
        }
    }
}
