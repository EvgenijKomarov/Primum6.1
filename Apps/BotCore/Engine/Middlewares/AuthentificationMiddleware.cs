using BotCore.Engine.Entities;
using BotCore.Engine.Entities.Outputs;
using CoreConnection;
using CoreConnection.DTOs;
using Engine;
using SolutionConfiguration;
using Resourses;
using SignServiceConnection.Models;

namespace BotCore.Engine.Middlewares
{
    public class AuthentificationMiddleware(UserClient client, ChatSignTokenWorker tokenWorker, ConfigurationClient configClient) : Middleware<DataBuffer, EngineOutputMessage>
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
                var encryptedToken = tokenWorker.EncryptSign(input.Sign);
                return Finish(new EngineOutputMessage
                {
                    Message = $"Привет!{Emoticons.Hello}\n" +
                    $"Я - {Emoticons.Bot}Primum bot\n" +
                    $"{Emoticons.Spark}Для начала работы тебе нужно войти, и тогда я дам тебе удобный доступ к своему профилю\n" +
                    $"Для быстрой авторизации перейди по этой ссылке:\n" +
                    $"{await configClient.GetGatewayUrl()}/confirm-chat?token={encryptedToken}\n" +
                    $"Или используй этот токен для ручной авторизации в личном кабинете:\n" +
                    $"{encryptedToken}"
                });
            }
            return Complete(input);
        }   
    }
}
