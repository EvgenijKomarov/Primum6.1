using AnonimousTokenProducer;
using CoreConnection.Notifications;
using Microsoft.EntityFrameworkCore;
using PrimumCore.Exceptions;
using PrimumCore.Models;
using PrimumCore.Services.Connectors;

namespace PrimumCore.Services.Iterators
{
    public class AnonymousTokenIterator(IPrimumContext context, IPublisher publisher, ChatSignTokenWorker tokenWorker)
    {
        public async Task<int> AddChat(int userId, string token)
        {
            var user = await context.Set<User>()
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(x => x.Id == userId);
            if (user is null) { throw new NotFoundException("User"); }

            var decryptedToken = tokenWorker.DecryptSign(token);
            if (decryptedToken is null) { throw new BusinessLogicException("Invalid token"); }
            await publisher.PublishAsync(new UserVerifiedChatNotification
            {
                UserId = user.Id,
                ChatId = decryptedToken.ChatId,
                RealizationTag = decryptedToken.RealizationTag
            });
            return userId;
        }
    }
}
