using Engine;

namespace BotCore.Entities.Inputs
{
    public abstract class CommandInput: IEngineInput<DataBuffer>
    {
        public Type EndpointNode { get; protected set; } = null!;
        public DataBuffer Object { get; protected set; } = null!;
        public int? UserId { get; protected set; } = null;
    }
}
