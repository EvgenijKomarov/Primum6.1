namespace BotCore.Entities
{
    public class BotOutput
    {
        public required string Message { get; set; } = null!;
        public Dictionary<string, string> Buttons { get; set; } = new Dictionary<string, string>();
    }
}
