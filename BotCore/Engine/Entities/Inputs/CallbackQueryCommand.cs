using BotCore.Entities.Engine;
using BotCore.Exceptions;
using BotCore.Nodes;
using Engine;

namespace BotCore.Engine.Entities.Inputs
{
    public class CallbackQueryCommand: CommandInput
    {
        public CallbackQueryCommand(string endpointNodeId, List<string> arguments)
        {
            EndpointNodeId = endpointNodeId;
            Object = new DataBuffer(arguments ?? new List<string>());
        }

        public CallbackQueryCommand(int? userId, string command)
        {
            if (string.IsNullOrWhiteSpace(command))
                throw new ArgumentException("Input string is null or empty", nameof(command));

            var parts = command.Split('_', StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length == 0)
                throw new ArgumentException("Invalid command format", nameof(command));

            EndpointNodeId = parts.First();
            Object = new DataBuffer(parts.Skip(1).ToList());
            UserId = userId;
        }
    }
}
