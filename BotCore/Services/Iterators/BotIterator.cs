using BotCore.Engine.Entities;
using BotCore.Engine.Entities.Inputs;
using BotCore.Engine.Entities.Outputs;
using BotCore.Entities;
using BotCore.Exceptions;
using Engine;
using Microsoft.AspNetCore.Mvc;

namespace BotCore.Services.Iterators
{
    public class BotIterator(
        EasyBotEngine<CommandInput, DataBuffer, OutputMessage> engine,
        InOutConverter converter
        )
    {
        public async Task<BotOutput> ProcessTextMessage(int? userId, string message, CancellationToken token)
        {
            OutputMessage? result = null;
            if (message.StartsWith('/')) 
            { 
                result = await engine.Process(new TextCommand(userId, message.Substring(1))); 
            }
            else
            {
                result = await engine.Process(new PlainTextInput(userId, message), token);
            }
            if (result is null) { throw new ProcessFailureException(); }
            return converter.ConvertToOutput(result, engine.GetEndpointNodeTypes());
        }

        public async Task<BotOutput> ProcessCallBackQuery(int? userId, string message, CancellationToken token)
        {
            var result = await engine.Process(new CallbackQueryCommand(
                userId,
                message),
                token);
            if (result is null) { throw new ProcessFailureException(); }
            return converter.ConvertToOutput(result, engine.GetEndpointNodeTypes());
        }
    }
}
