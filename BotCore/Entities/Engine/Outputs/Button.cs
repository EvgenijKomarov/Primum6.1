using BotCore.Entities.Engine.Inputs;

namespace BotCore.Entities.Engine.Outputs
{
    public class Button
    {
        public string Text { get; set; } = null!;
        public Type EndpointNode { get; set; } = null!;

        public List<string> Args { get; set; } = new List<string>();
    }
}
