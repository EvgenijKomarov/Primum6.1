using AnonimousTokenProducer;
using BotCore.Engine.Entities;
using BotCore.Engine.Entities.Outputs;
using CoreConnection;
using CoreConnection.DTOs;
using Engine;

namespace BotCore.Engine.Middlewares
{
    public class AuthentificationMiddleware(UserClient client, ChatSignTokenWorker tokenWorker, IConfiguration configuration) : Middleware<DataBuffer, EngineOutputMessage>
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
                    Message = "Привет! Для быстрой авторизации перейди по этой ссылке:\n" +
                    $"{configuration.GetValue<string>("WebUrl")}/api/user/confirm-chat?token={tokenWorker.EncryptSign(input.Sign)}"
                });
            }
            return Complete(input);
        }   
    }
}
