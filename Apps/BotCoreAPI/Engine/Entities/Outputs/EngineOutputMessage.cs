namespace BotCore.Engine.Entities.Outputs
{
    public class EngineOutputMessage
    {
        public required string Message { get; set; } = null!;
        public IEnumerable<EngineOutputButton> Buttons = new List<EngineOutputButton>();
    }
}
