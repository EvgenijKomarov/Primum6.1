using BotCore.Nodes.EndpointNodes;
using Engine;

namespace BotCore.Entities.Inputs
{
    public class PlainTextInput : CommandInput
    {
        public PlainTextInput(int? userId, string input)
        {
            EndpointNode = typeof(PlainTextNode);
            Object = new DataBuffer(new List<string> { input });
            UserId = userId;
        }
    }
}
