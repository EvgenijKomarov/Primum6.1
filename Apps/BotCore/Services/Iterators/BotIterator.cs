using BotCore.Engine.Entities;
using BotCore.Engine.Entities.Inputs;
using BotCore.Engine.Entities.Outputs;
using BotCore.Entities;
using BotCore.Exceptions;
using ChatSigns;
using Engine;
using Microsoft.AspNetCore.Mvc;
using SignServiceConnection;

namespace BotCore.Services.Iterators
{
    public class BotIterator(
        EasyBotEngine<CommandEngineInput, DataBuffer, EngineOutputMessage> engine,
        SignServiceClient client,
        InOutConverter converter
        )
    {
        public async Task<BotOutput> ProcessTextMessage(BotInput input, CancellationToken token)
        {
            EngineOutputMessage? result = null;
            if (input.Data.StartsWith('/')) 
            { 
                result = await engine.Process(new TextCommand(await GetId(input.Sign), input)); 
            }
            else
            {
                result = await engine.Process(new PlainTextInput(await GetId(input.Sign), input), token);
            }
            if (result is null) { throw new ProcessFailureException(); }
            return converter.ConvertToOutput(result, engine.GetEndpointNodeTypes());
        }

        public async Task<BotOutput> ProcessCallBackQuery(BotInput input, CancellationToken token)
        {
            var result = await engine.Process(new CallbackQueryCommand(
                await GetId(input.Sign),
                input),
                token);
            if (result is null) { throw new ProcessFailureException(); }
            return converter.ConvertToOutput(result, engine.GetEndpointNodeTypes());
        }

        private async Task<int?> GetId(ChatSign sign)
        {
            return await client.GetUserIdAsync(sign.RealizationTag, sign.ChatId);
        }
    }
}
