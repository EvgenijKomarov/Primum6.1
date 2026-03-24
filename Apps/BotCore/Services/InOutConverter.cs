using BotCore.Engine.Entities.Outputs;
using BotCore.Entities;
using Engine.Nodes;

namespace BotCore.Services
{
    public class InOutConverter
    {
        public BotOutput ConvertToOutput(EngineOutputMessage outputMessage, Dictionary<string, Type> availableEndpoints)
        {
            return new BotOutput
            {
                Message = outputMessage.Message,
                Buttons = outputMessage.Buttons.ToDictionary(
                    b => b.Text, 
                    b => GenerateCallbackData(availableEndpoints.FirstOrDefault(x => x.Value == b.EndpointNode).Key, b.Args, b.PageIndex))
            };
        }

        private string GenerateCallbackData(string endpointNodeId, List<string> args, int? pageindex = null)
        {
            if (args.Count == 0) { return endpointNodeId; }
            return $"{endpointNodeId}_{string.Join("_", args)}" + pageindex is not null ? $"&{pageindex}" : string.Empty;
        }
    }
}
