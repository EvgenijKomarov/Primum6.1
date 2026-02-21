namespace BotCore.Engine.Entities.Outputs
{
    public class EngineOutputButton
    {
        public required string Text { get; set; } = null!;
        public required Type EndpointNode { get; set; } = null!;

        public List<string> Args { get; set; } = new List<string>();
    }
}
