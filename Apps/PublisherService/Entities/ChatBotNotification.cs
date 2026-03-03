using SignServiceConnection.Models;

namespace PublisherService.Entities
{
    public class ChatBotNotification
    {
        public required ChatSign ChatSign { get; set; }
        public required string Text { get; set; }
    }
}
