using Engine;

namespace BotCore.Engine.Entities.Inputs
{
    public class PlainTextInput : CommandInput
    {
        public PlainTextInput(int? userId, string input)
        {
            EndpointNodeId = "text";
            Object = new DataBuffer(userId, new List<string> { input });
            UserId = userId;
        }
    }
}
