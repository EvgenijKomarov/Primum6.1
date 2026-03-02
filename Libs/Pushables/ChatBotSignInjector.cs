using SignServiceConnection;
using SignServiceConnection.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pushables
{
    public class ChatBotSignInjector(SignServiceClient client)
    {
        public async Task<IEnumerable<ChatSign>> InjectSign(int userId)
        {
            return await client.GetSignsAsync(userId);
        }
    }
}
