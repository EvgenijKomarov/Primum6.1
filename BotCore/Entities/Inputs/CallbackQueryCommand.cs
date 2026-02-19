using BotCore.Exceptions;
using BotCore.Nodes;
using Engine;

namespace BotCore.Entities.Inputs
{
    public class CallbackQueryCommand: CommandInput
    {
        public CallbackQueryCommand(Type endpointNode, List<string> arguments)
        {
            EndpointNode = endpointNode;
            Object = new DataBuffer(arguments ?? new List<string>());
        }

        public CallbackQueryCommand(int? userId, string command, IEnumerable<Type> endpointTypes)
        {
            if (string.IsNullOrWhiteSpace(command))
                throw new ArgumentException("Input string is null or empty", nameof(command));

            var parts = command.Split('_', StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length == 0)
                throw new ArgumentException("Invalid command format", nameof(command));

            var resCommand = endpointTypes.FirstOrDefault(x => x.Name == parts[0]);
            if (resCommand == null) throw new BadCommandNameException(parts[0]);
            EndpointNode = resCommand;
            Object = new DataBuffer(parts.Skip(1).ToList());
            UserId = userId;
        }

        public override string ToString()
        {
            string args = Object.ToString();
            if (args.Length == 0)
                return EndpointNode.Name;

            return $"{EndpointNode.Name}_{args}";
        }
    }
}
