using CoreDBModel.Models;
using CoreDBModel.Models.Enums;
using Microsoft.EntityFrameworkCore;
using PrimumCore.Exceptions;
using PrimumCore.Extentions;
using PrimumCore.Services.Utilities;
using PublishServiceConnection;
using PublishServiceConnection.Events;
using SolutionConfiguration;
using System.ComponentModel.DataAnnotations;

namespace PrimumCore.Services.Iterators
{
    public class TokenIterator(DatabaseIterator dbIterator,
        RandomStringGenerator randomGenerator,
        PublisherService publisher,
        ConfigurationClient configClient)
    {
        public async Task<int> SendEmailVerification(int userId, string? correctiveMail)
        {
            var user = await dbIterator.Users(false)
                .Include(x => x.VerificationTokens)
                .IgnoreQueryFilters()
                .One(x => x.Id == userId);
            //if (user.IsMailChecked) { throw new BusinessLogicException("User already verified email"); }

            if (!new EmailAddressAttribute().IsValid(correctiveMail))
            { throw new BusinessLogicException("Adress not valid"); }

            if (await dbIterator.Users(false)
                .Where(x => x.Id != userId)
                .AnyAsync(x => x.MailAdress == correctiveMail))
            { throw new BusinessLogicException("User with the same adress already exists"); }

            if (correctiveMail is not null && user.MailAdress != correctiveMail) { user.MailAdress = correctiveMail; }

            var token = new VerificationToken
            {
                User = user,
                Token = randomGenerator.GenerateRandomString(),
                LifeTime = DateTime.UtcNow.AddHours(12),
                Meaning = TokenMeaning.EmailVerification
            };
            user.VerificationTokens.Add(token);

            await publisher.Push(new UserEmailVerificationEvent
            {
                EmailAdress = user.MailAdress,
                Token = token.Token,
                AuthUrl = await configClient.GetGatewayUrl(),
                UserId = user.Id
            });

            user.IsMailChecked = false;
            await dbIterator.SaveChangesAsync();
            return user.Id;
        }

        public async Task<int> ConfirmToken(int userId, string inputToken)
        {
            var user = await dbIterator.Users(false)
                .Include(x => x.VerificationTokens)
                .IgnoreQueryFilters()
                .One(x => x.Id == userId);

            var token = user.VerificationTokens.FirstOrDefault(x => x.Token == inputToken);
            if (token is null) { throw new NotFoundException("Token"); }
            if (token.LifeTime < DateTime.UtcNow) { throw new BusinessLogicException("Token expired"); }
            if (token.IsUsed) { throw new BusinessLogicException("Token is used"); }

            token.IsUsed = true;
            switch (token.Meaning)
            {
                case TokenMeaning.EmailVerification:
                    user.IsMailChecked = true;
                    break;
            }
            await dbIterator.SaveChangesAsync();
            return user.Id;
        }
    }
}
