using CoreConnection.Notifications;
using Microsoft.EntityFrameworkCore;
using PrimumCore.Exceptions;
using PrimumCore.Models;
using PrimumCore.Models.Enums;
using PrimumCore.Services.Connectors;
using PrimumCore.Services.Utilities;

namespace PrimumCore.Services.Iterators
{
    public class TokenIterator(IPrimumContext context,
        RandomStringGenerator randomGenerator,
        IPublisher publisher)
    {
        public async Task<int> SendEmailVerification(int userId, string? correctiveMail)
        {
            var user = await context.Set<User>()
                .Include(x => x.VerificationTokens)
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(x => x.Id == userId);
            if (user is null) { throw new NotFoundException("User"); }

            if (correctiveMail is not null && user.MailAdress != correctiveMail) { user.MailAdress = correctiveMail; }

            var token = new VerificationToken
            {
                User = user,
                Token = randomGenerator.GenerateRandomString(),
                LifeTime = DateTime.Now.AddHours(12),
                Meaning = TokenMeaning.EmailVerification
            };
            user.VerificationTokens.Add(token);

            await publisher.PublishAsync(new UserVerificationNotification
            {
                EmailAdress = user.MailAdress,
                VerificationHash = token.Token,
                Userid = user.Id
            });

            await context.SaveChangesAsync();
            return user.Id;
        }

        public async Task<int> ConfirmToken(int userId, string inputToken)
        {
            var user = await context.Set<User>()
                .Include(x => x.VerificationTokens)
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(x => x.Id == userId);
            if (user is null) { throw new NotFoundException("User"); }

            var token = user.VerificationTokens.FirstOrDefault(x => x.Token == inputToken);
            if (token is null) { throw new NotFoundException("Token"); }
            if (token.LifeTime < DateTime.Now) { throw new BusinessLogicException("Token expired"); }
            if (token.IsUsed) { throw new BusinessLogicException("Token is used"); }

            token.IsUsed = true;
            switch (token.Meaning)
            {
                case TokenMeaning.EmailVerification:
                    user.IsMailChecked = true;
                    await publisher.PublishAsync(new UserVerifiedEmailNotification
                    {
                        EmailAdress = user.MailAdress,
                        Userid = user.Id
                    });
                    break;
            }
            await context.SaveChangesAsync();
            return user.Id;
        }
    }
}
