namespace BotCore.Engine.Entities.Outputs
{
    public class Button
    {
        public string Text { get; set; } = null!;
        public Type EndpointNode { get; set; } = null!;

        public List<string> Args { get; set; } = new List<string>();
    }
}
