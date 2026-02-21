using BotCore.Entities;
using BotCore.Exceptions;
using Engine;

namespace BotCore.Engine.Entities.Inputs
{
    public class CallbackQueryCommand: CommandInput
    {
        public CallbackQueryCommand(int? userId, BotInput input)
        {
            if (string.IsNullOrWhiteSpace(input.Data))
                throw new ArgumentException("Input string is null or empty", nameof(input.Data));

            var parts = input.Data.Split('_', StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length == 0)
                throw new ArgumentException("Invalid command format", nameof(input.Data));

            EndpointNodeId = parts.First();
            Object = new DataBuffer(userId, parts.Skip(1).ToList(), input.Sign);
            UserId = userId;
        }
    }
}
