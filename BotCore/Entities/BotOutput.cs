using BotCore.Entities.Engine.Outputs;

namespace BotCore.Entities
{
    public class BotOutput
    {
        public required string Message { get; set; } = null!;
        public Dictionary<string, string> Buttons = new Dictionary<string, string>();
    }
}
