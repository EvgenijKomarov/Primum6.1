using ChatSigns;
using BotCore.Engine.Entities;
using BotCore.Engine.Entities.Outputs;
using CoreConnection;
using CoreConnection.DTOs;
using Engine;
using SolutionConfiguration;
using Resourses;

namespace BotCore.Engine.Middlewares
{
    public class AuthentificationMiddleware(UserClient client, ChatSignTokenWorker tokenWorker, SolutionEnvironment configuration) : Middleware<DataBuffer, EngineOutputMessage>
    {
        public async override Task<INodeResult<DataBuffer, EngineOutputMessage>> Invoke(DataBuffer input, CancellationToken? token = null)
        {
            bool isAuthenticated = false;
            if (input.UserId != null)
            {
                UserDto userDto = await client.ProfileAsync(input.UserId.Value);
                if (userDto != null) 
                {
                    input.User = userDto;
                    isAuthenticated = true;
                }
            }

            if (!isAuthenticated) 
            {
                return Finish(new EngineOutputMessage
                {
                    Message = $"Привет!{Emoticons.Hello}\n" +
                    $"Я - {Emoticons.Bot}Primum bot\n" +
                    $"{Emoticons.Spark}Для начала работы тебе нужно войти, и тогда я дам тебе удобный доступ к своему профилю\n" +
                    $"Для быстрой авторизации перейди по этой ссылке:\n" +
                    $"{configuration.GatewayURL}/api/user/confirm-chat?token={tokenWorker.EncryptSign(input.Sign)}"
                });
            }
            return Complete(input);
        }   
    }
}
