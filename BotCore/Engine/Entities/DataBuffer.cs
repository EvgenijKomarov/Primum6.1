using CoreConnection.DTOs;

namespace BotCore.Engine.Entities
{
    public class DataBuffer
    {
        public DataBuffer(List<string> arguments) 
        { 
            this.Arguments = arguments;
        }
        public UserDto? User { get; set; } = null;
        public List<string> Arguments = new List<string>();

        public override string ToString()
        {
            return string.Join("_", Arguments);
        }
    }
}
