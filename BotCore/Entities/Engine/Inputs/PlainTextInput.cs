using BotCore.Nodes.EndpointNodes;
using Engine;

namespace BotCore.Entities.Engine.Inputs
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
