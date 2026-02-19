using Engine;

namespace BotCore.Engine.Entities.Inputs
{
    public abstract class CommandInput: IEngineInput<DataBuffer>
    {
        public string EndpointNodeId { get; protected set; } = null!;
        public DataBuffer Object { get; protected set; } = null!;
        public int? UserId { get; protected set; } = null;
    }
}
