using CoreConnection.DTOs;
using CoreConnection.Notifications;
using Microsoft.EntityFrameworkCore;
using PrimumCore.Models;
using PrimumCore.Models.Enums;
using PrimumCore.Services.Connectors;
using PrimumCore.Services.Utilities;
using PrimumPlatformModel.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace PrimumCore.Services.Iterators
{
    public class UserIterator(IPrimumContext context, 
        PasswordHasher passwordHasher, 
        IPublisher publisher,
        RandomStringGenerator randomGenerator)
    {
        public async Task<(int?, string)> Login(string mailAdress, string password)
        {
            var user = await context.Set<User>()
                .FirstOrDefaultAsync(x => x.MailAdress == mailAdress);
            if (user is null) { return (null, "Unknown login"); }

            if (!passwordHasher.VerifyPassword(password, user.Password)) { return (null, "Wrong password"); }
            return (user.Id, string.Empty);
        }

        public async Task<int> RegUser(RegistrationInputDto dto)
        {
            if (!new EmailAddressAttribute().IsValid(dto.MailAdress)) 
            { throw new Exception("Adress not valid"); }

            if (await context.Set<User>()
                .AnyAsync(x => x.MailAdress == dto.MailAdress)) 
            { throw new Exception("User with the same adress already exists"); }

            var user = new User
            {
                Name = dto.Name,
                Surname = dto.Surname,
                Patronymic = dto.Patronymic,
                MailAdress = dto.MailAdress,
                Password = passwordHasher.HashPassword(dto.Password),
                Cash = 0
            };
            context.Set<User>().Add(user);
            await context.SaveChangesAsync();

            return user.Id;
        }

        public async Task<int> SendEmailVerification(int userId, string? correctiveMail)
        {
            var user = await context.Set<User>()
                .Include(x => x.VerificationTokens)
                .FirstOrDefaultAsync(x => x.Id == userId);
            if (user is null) { throw new Exception("User not found"); }

            if(correctiveMail is not null && user.MailAdress != correctiveMail) { user.MailAdress = correctiveMail; }

            var token = new VerificationToken
            {
                User = user,
                Token = randomGenerator.GenerateRandomString(),
                LifeTime = DateTime.Now.AddHours(12),
                Meaning = TokenMeaning.EmailVerification
            };
            context.Set<VerificationToken>().Add(token);

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
                .FirstOrDefaultAsync(x => x.Id == userId);
            if (user is null) { throw new Exception("User not found"); }

            var token = user.VerificationTokens.FirstOrDefault(x => x.Token == inputToken);
            if (token is null) { throw new Exception("Token not found"); }
            if (token.LifeTime < DateTime.Now) { throw new Exception("Token expired"); }
            if (token.IsUsed) { throw new Exception("Token is used"); }

            token.IsUsed = true;
            switch (token.Meaning) {
                case TokenMeaning.EmailVerification:
                    user.IsMailChecked = true;
                    break;
            }
            await context.SaveChangesAsync();
            return user.Id;
        }

        public async Task<int> CreateTeacherProfile(int userId, string about)
        {
            var user = await context.Set<User>()
                .Include(u => u.TeacherProfile)
                .FirstOrDefaultAsync(x => x.Id == userId);
            if (user is null) { throw new Exception("User not found"); }
            if (user.TeacherProfile is not null) { throw new Exception("User is already teacher"); }

            user.TeacherProfile = new TeacherProfile
            {
                About = about
            };

            await context.SaveChangesAsync();

            return user.Id;
        }

        public async Task<int> CreateStudentProfile(int userId)
        {
            var user = await context.Set<User>()
                .Include(u => u.StudentProfile)
                .FirstOrDefaultAsync(x => x.Id == userId);
            if (user is null) { throw new Exception("User not found"); }
            if (user.StudentProfile is not null) { throw new Exception("User is already student"); }

            user.StudentProfile = new StudentProfile();

            await context.SaveChangesAsync();

            return user.Id;
        }

        public async Task<object> GetUser(int id)
        {
            var user = await context.Set<User>()
                .Include(u => u.StudentProfile)
                .Include(u => u.TeacherProfile)
                .Include(u => u.AdminProfile)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (user is null) { throw new Exception("User not found"); }

            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Patronymic = user.Patronymic,
                DisplayName = user.DisplayName,
                Cash = user.Cash,
                IsApprovedStudent = user.StudentProfile is not null ?
                        user.StudentProfile.ApproveStatus == ApproveStatus.Approved : (bool?)null,
                IsApprovedTeacher = user.TeacherProfile is not null ?
                        user.TeacherProfile.ApproveStatus == ApproveStatus.Approved : (bool?)null,
                IsAdmin = user.AdminProfile is not null
            };
        }
    }
}
