using CoreConnection.DTOs;

namespace BotCore.Engine.Entities
{
    public class DataBuffer
    {
        public DataBuffer(int? userId, List<string> arguments) 
        { 
            Arguments = arguments;
            UserId = userId;
        }
        public UserDto? User { get; set; } = null;
        public int? UserId { get; private set; } = null;
        public List<string> Arguments = new List<string>();

        public override string ToString()
        {
            return string.Join("_", Arguments);
        }
    }
}
