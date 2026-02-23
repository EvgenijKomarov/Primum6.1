using ChatSigns;
using CoreConnection.DTOs;

namespace BotCore.Engine.Entities
{
    public class DataBuffer
    {
        public int? UserId { get; private set; } = null;
        public List<string> Arguments = new List<string>();
        public ChatSign Sign { get; private set; }
        public DataBuffer(int? userId, List<string> arguments, ChatSign sign) 
        { 
            Arguments = arguments;
            UserId = userId;
            Sign = sign;
        }

        public UserDto? User { get; set; } = null;
    }
}
