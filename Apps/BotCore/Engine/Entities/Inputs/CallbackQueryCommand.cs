using BotCore.Entities;
using BotCore.Exceptions;
using Engine;

namespace BotCore.Engine.Entities.Inputs
{
    public class CallbackQueryCommand: CommandEngineInput
    {
        public CallbackQueryCommand(int? userId, BotInput input)
        {
            if (string.IsNullOrWhiteSpace(input.Data))
                throw new ArgumentException("Input string is null or empty", nameof(input.Data));

            var partsRaw = input.Data.Split('_', StringSplitOptions.RemoveEmptyEntries);

            var parts = partsRaw.Where(x => !x.StartsWith("&"));
            if (parts.Count() == 0)
                throw new ArgumentException("Invalid command format", nameof(input.Data));
            var pageIndex = partsRaw.FirstOrDefault(x => x.StartsWith("&"))?.Substring(1) ?? null;

            EndpointNodeId = parts.First();
            Object = new DataBuffer(userId, parts.Skip(1).ToList(), input.Sign, pageIndex is not null ? int.Parse(pageIndex) : null);
            UserId = userId;
        }
    }
}
