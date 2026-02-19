namespace BotCore.Engine.Entities.Outputs
{
    public class OutputMessage
    {
        public required string Message { get; set; } = null!;
        public IEnumerable<Button> Buttons = new List<Button>();
    }
}
