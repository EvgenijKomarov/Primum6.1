using CoreConnection.DTOs;
using SignServiceConnection.Models;

namespace BotCore.Engine.Entities
{
    public class DataBuffer
    {
        public int? UserId { get; private set; } = null;
        public List<string> Arguments = new List<string>();
        public int? PageIndex {  get; private set; } = null;
        public ChatSign Sign { get; private set; }
        public DataBuffer(int? userId, List<string> arguments, ChatSign sign, int? pageIndex = null) 
        { 
            Arguments = arguments;
            UserId = userId;
            Sign = sign;
            PageIndex = pageIndex;
        }

        public UserDto? User { get; set; } = null;
    }
}
