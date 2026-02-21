using AnonimousTokenProducer;
using CoreConnection.DTOs;

namespace BotCore.Engine.Entities
{
    public class DataBuffer
    {
        public int? UserId { get; private set; } = null;
        public List<string> Arguments = new List<string>();
        public ChatBotSign Sign { get; private set; }
        public DataBuffer(int? userId, List<string> arguments, ChatBotSign sign) 
        { 
            Arguments = arguments;
            UserId = userId;
            Sign = sign;
        }

        public UserDto? User { get; set; } = null;
    }
}
