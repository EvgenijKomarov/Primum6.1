using BotCore.Engine.Entities;
using BotCore.Engine.Entities.Outputs;
using CoreConnection;
using CoreConnection.DTOs;
using Engine;

namespace BotCore.Engine.Middlewares
{
    public class AuthentificationMiddleware(UserClient client): Middleware<DataBuffer, OutputMessage>
    {
        public async override Task<INodeResult<DataBuffer, OutputMessage>> Invoke(DataBuffer input, CancellationToken? token = null)
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
                return Finish(new OutputMessage
                {
                    Message = "Привет! Это примум бот, тебе надо сперва зарегистрироваться!"
                });
            }
            return Complete(input);
        }   
    }
}
