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
        EasyBotEngine<CommandEngineInput, DataBuffer, EngineOutputMessage> engine,
        InOutConverter converter
        )
    {
        public async Task<BotOutput> ProcessTextMessage(int? userId, BotInput input, CancellationToken token)
        {
            EngineOutputMessage? result = null;
            if (input.Data.StartsWith('/')) 
            { 
                result = await engine.Process(new TextCommand(userId, input)); 
            }
            else
            {
                result = await engine.Process(new PlainTextInput(userId, input), token);
            }
            if (result is null) { throw new ProcessFailureException(); }
            return converter.ConvertToOutput(result, engine.GetEndpointNodeTypes());
        }

        public async Task<BotOutput> ProcessCallBackQuery(int? userId, BotInput input, CancellationToken token)
        {
            var result = await engine.Process(new CallbackQueryCommand(
                userId,
                input),
                token);
            if (result is null) { throw new ProcessFailureException(); }
            return converter.ConvertToOutput(result, engine.GetEndpointNodeTypes());
        }
    }
}
