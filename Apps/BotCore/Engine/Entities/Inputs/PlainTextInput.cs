using BotCore.Entities;
using Engine;

namespace BotCore.Engine.Entities.Inputs
{
    public class PlainTextInput : CommandEngineInput
    {
        public PlainTextInput(int? userId, BotInput input)
        {
            EndpointNodeId = "text";
            Object = new DataBuffer(userId, new List<string> { input.Data }, input.Sign);
            UserId = userId;
        }
    }
}
