using BotCore.Entities;
using BotCore.Entities.Inputs;
using BotCore.Entities.Outputs;
using BotCore.Exceptions;
using Engine;
using Microsoft.AspNetCore.Mvc;

namespace BotCore.Services.Iterators
{
    public class BotIterator(EasyBotEngine<CommandInput, DataBuffer, OutputMessage> engine)
    {
        public async Task<OutputMessage> ProcessTextMessage(int? userId, string message, CancellationToken token)
        {
            OutputMessage? result = null;
            if (message.StartsWith('/')) 
            { 
                result = await engine.Process(new TextCommand(userId, message.Substring(1), engine.GetEndpointNodes().Select(x => x.GetType()).ToArray())); 
            }
            else
            {
                result = await engine.Process(new PlainTextInput(userId, message), token);
            }
            if (result is null) { throw new ProcessFailureException(); }
            return result;
        }

        public async Task<OutputMessage> ProcessCallBackQuery(int? userId, string message, CancellationToken token)
        {
            var result = await engine.Process(new CallbackQueryCommand(
                userId,
                message, 
                engine.GetEndpointNodes().Select(x => x.GetType()).ToArray()), 
                token);
            if (result is null) { throw new ProcessFailureException(); }
            return result;
        }
    }
}
