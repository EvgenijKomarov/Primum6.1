using BotCore.Exceptions;

namespace BotCore.Entities.Inputs
{
    public class TextCommand: CommandInput
    {
        public TextCommand(int? userId, string command, IEnumerable<Type> endpointTypes)
        {
            if (string.IsNullOrWhiteSpace(command))
                throw new ArgumentException("Input string is null or empty", nameof(command));

            var parts = command.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length == 0)
                throw new ArgumentException("Invalid command format", nameof(command));

            var resCommand = endpointTypes.FirstOrDefault(x => x.Name == parts[0]);
            if (resCommand == null) throw new BadCommandNameException(parts[0]);
            EndpointNode = resCommand;
            Object = new DataBuffer(parts.Skip(1).ToList());
            UserId = userId;
        }
    }
}
