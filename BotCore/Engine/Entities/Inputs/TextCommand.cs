using BotCore.Entities;
using BotCore.Exceptions;

namespace BotCore.Engine.Entities.Inputs
{
    public class TextCommand: CommandEngineInput
    {
        public TextCommand(int? userId, BotInput input)
        {
            var command = input.Data.Substring(1);
            if (string.IsNullOrWhiteSpace(command))
                throw new ArgumentException("Input string is null or empty", nameof(command));

            var parts = command.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length == 0)
                throw new ArgumentException("Invalid command format", nameof(command));

            EndpointNodeId = parts.First();
            Object = new DataBuffer(userId, parts.Skip(1).ToList(), input.Sign);
            UserId = userId;
        }
    }
}
