using AnonimousTokenProducer;

namespace BotCore.Entities
{
    public class BotInput
    {
        public ChatBotSign Sign { get; set; } = null!;
        public string Data { get; set; } = null!;
    }
}
