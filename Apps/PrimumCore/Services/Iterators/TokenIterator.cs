using CoreDBModel.Models;
using CoreDBModel.Models.Enums;
using Microsoft.EntityFrameworkCore;
using PrimumCore.Exceptions;
using PrimumCore.Services.Utilities;
using PublishServiceConnection;
using PublishServiceConnection.Events;
using SolutionConfiguration;

namespace PrimumCore.Services.Iterators
{
    public class TokenIterator(PrimumContext context,
        RandomStringGenerator randomGenerator,
        PublisherService publisher,
        SolutionEnvironment environment)
    {
        public async Task<int> SendEmailVerification(int userId, string? correctiveMail)
        {
            var user = await context.Set<User>()
                .Include(x => x.VerificationTokens)
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(x => x.Id == userId);
            if (user is null) { throw new NotFoundException("User"); }
            if (user.IsMailChecked) { throw new BusinessLogicException("User already verified email"); }

            if (correctiveMail is not null && user.MailAdress != correctiveMail) { user.MailAdress = correctiveMail; }

            var token = new VerificationToken
            {
                User = user,
                Token = randomGenerator.GenerateRandomString(),
                LifeTime = DateTime.Now.AddHours(12),
                Meaning = TokenMeaning.EmailVerification
            };
            user.VerificationTokens.Add(token);

            await publisher.Push(new UserEmailVerificationEvent
            {
                EmailAdress = user.MailAdress,
                VerificationLink = $"{environment.GatewayURL}/api/user/confirm-email?token={token.Token}",
                UserId = user.Id
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
                    break;
            }
            await context.SaveChangesAsync();
            return user.Id;
        }
    }
}
