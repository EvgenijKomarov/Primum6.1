using BotCore.Exceptions;

namespace BotCore.Engine.Entities.Inputs
{
    public class TextCommand: CommandInput
    {
        public TextCommand(int? userId, string command)
        {
            if (string.IsNullOrWhiteSpace(command))
                throw new ArgumentException("Input string is null or empty", nameof(command));

            var parts = command.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length == 0)
                throw new ArgumentException("Invalid command format", nameof(command));

            EndpointNodeId = command;
            Object = new DataBuffer(userId, parts.Skip(1).ToList());
            UserId = userId;
        }
    }
}
