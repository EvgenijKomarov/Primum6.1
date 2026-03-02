using SignServiceConnection.Models;

namespace BotCore.Entities
{
    public class BotInput
    {
        public ChatSign Sign { get; set; } = null!;
        public string Data { get; set; } = null!;
    }
}
