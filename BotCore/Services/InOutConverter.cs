using BotCore.Engine.Entities.Outputs;
using BotCore.Entities;
using BotCore.Entities.Engine;
using BotCore.Entities.Engine.Inputs;
using Engine.Nodes;

namespace BotCore.Services
{
    public class InOutConverter
    {
        public BotOutput ConvertToOutput(OutputMessage outputMessage, Dictionary<string, Type> availableEndpoints)
        {
            return new BotOutput
            {
                Message = outputMessage.Message,
                Buttons = outputMessage.Buttons.ToDictionary(
                    b => b.Text, 
                    b => GenerateCallbackData(availableEndpoints.FirstOrDefault(x => x.Value == b.EndpointNode).Key, b.Args))
            };
        }

        private string GenerateCallbackData(string endpointNodeId, List<string> args)
        {
            if (args.Count == 0) { return endpointNodeId; }
            return $"{endpointNodeId}_{string.Join("_", args)}";
        }
    }
}
