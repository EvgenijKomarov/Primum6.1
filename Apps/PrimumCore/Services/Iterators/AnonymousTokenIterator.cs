using CoreDBModel.Models;
using Microsoft.EntityFrameworkCore;
using PrimumCore.Exceptions;
using Pushables;
using Pushables.Events;
using SignServiceConnection;
using SignServiceConnection.Models;

namespace PrimumCore.Services.Iterators
{
    public class AnonymousTokenIterator(PrimumContext context, SignServiceClient client, ChatSignTokenWorker tokenWorker)
    {
        public async Task<int> AddChat(int userId, string token)
        {
            var user = await context.Set<User>()
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(x => x.Id == userId);
            if (user is null) { throw new NotFoundException("User"); }

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
    }
}
