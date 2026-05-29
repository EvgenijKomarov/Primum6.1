using CoreDBModel.Constants;
using Microsoft.EntityFrameworkCore;
using PrimumCore.Entities;
using PrimumCore.Exceptions;
using PrimumCore.Extentions;
using SignServiceConnection;
using SignServiceConnection.Models;

namespace PrimumCore.Services.Iterators
{
    public class ChatSignTokenIterator(DatabaseIterator dbIterator, SignServiceClient client, ChatSignTokenWorker tokenWorker)
    {
        public async Task<int> AddChat(int userId, string token)
        {
            var user = await dbIterator.Users(true)
                .IgnoreQueryFilters()
                .One(x => x.Id == userId);
            if (!AvailabilityExpressions.IsUserAvailable.Compile()(user)) { throw new BusinessLogicException("User should be available"); }

            var decryptedToken = tokenWorker.DecryptSign(token);
            if (decryptedToken is null) { throw new BusinessLogicException("Invalid token"); }
            await client.AddUserAsync(new UserCreate
            {
                UserId = user.Id,
                RealizationTag = decryptedToken.RealizationTag,
                ChatId = decryptedToken.ChatId,
                Username = decryptedToken.UserName
            });
            return userId;
        }

        public async Task<PageResult<ChatSign>> GetChatSigns(int userId, int page, int pageSize)
        {
            var user = await dbIterator.Users(true)
                .IgnoreQueryFilters()
                .One(x => x.Id == userId);
            return await (await client.GetSignsAsync(userId)).ToPageResult(page, pageSize);
        }
    }
}
