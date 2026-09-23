using CoreConnection.DTOs;
using PrimumCore.Entities;
using CoreDBModel.Constants;
using CoreDBModel.Models;
using Microsoft.EntityFrameworkCore;
using PrimumCore.Exceptions;
using PrimumCore.Extentions;
using PrimumCore.Services.Utilities;
using System.ComponentModel.DataAnnotations;
using PaymentServiceConnection;
using CoreDBModel.Services;
using CoreDBModel.Extensions;

namespace PrimumCore.Services.Iterators
{
    public class UserIterator(DatabaseIterator dbIterator, PasswordHasher passwordHasher, PaymentServiceClient paymentServiceClient)
    {
        public const int MinPasswordLength = 8;
        public const int MaxPasswordLength = 64;

        // Хэш для сравнения, когда пользователь не найден: время ответа не должно выдавать, есть ли такой email
        private static readonly string DummyPasswordHash = new PasswordHasher().HashPassword(Guid.NewGuid().ToString());

        public async Task<int> Login(string mailAdress, string password)
        {
            var mail = EmailNormalizer.Normalize(mailAdress);
            var user = await dbIterator.Users(false)
                .FirstOrDefaultAsync(x => x.MailAdress.ToLower() == mail);

            // Одинаковый ответ на неизвестный email и неверный пароль, чтобы нельзя было перебирать аккаунты
            var isPasswordValid = passwordHasher.VerifyPassword(password, user?.Password ?? DummyPasswordHash);
            if (user is null || !isPasswordValid) { throw new InvalidCredentialsException(); }

            if (user.IsBanned) { throw new BusinessLogicException("User is banned"); }
            return user.Id;
        }

        public async Task<int> RegUser(RegistrationInputDto dto)
        {
            var mail = EmailNormalizer.Normalize(dto.MailAdress);

            if (string.IsNullOrWhiteSpace(dto.Name) || string.IsNullOrWhiteSpace(dto.Surname))
            { throw new BusinessLogicException("Name and surname are required"); }

            if (!new EmailAddressAttribute().IsValid(mail))
            { throw new BusinessLogicException("Address not valid"); }

            if (await dbIterator.Users(false)
                .AnyAsync(x => x.MailAdress.ToLower() == mail))
            { throw new BusinessLogicException("User with the same adress already exists"); }

            if (string.IsNullOrEmpty(dto.Password) || dto.Password.Length < MinPasswordLength)
            { throw new BusinessLogicException($"Password too short. Minimum {MinPasswordLength} chars"); }
            if (dto.Password.Length > MaxPasswordLength)
            { throw new BusinessLogicException($"Password too long. Maximum {MaxPasswordLength} chars"); }

            var user = new User
            {
                Name = dto.Name.Trim(),
                Surname = dto.Surname.Trim(),
                Patronymic = dto.Patronymic?.Trim() ?? string.Empty,
                MailAdress = mail,
                TimeZoneOffset = TimeSpan.FromMinutes(dto.TimeZoneOffset),
                Password = passwordHasher.HashPassword(dto.Password)
            };
            await dbIterator.AddAsync(user);
            await dbIterator.SaveChangesAsync();

            return user.Id;
        }

        public async Task<int> CreateTeacherProfile(int userId, TeacherRegistrationInputDto dto)
        {
            var user = await dbIterator.Users(false)
                .Include(x => x.TeacherProfile)
                .One(x => x.Id == userId);
            if (user.TeacherProfile is not null) { throw new BusinessLogicException("User is already teacher"); }
            if (!AvailabilityExpressions.IsUserAvailable.Compile()(user)) { throw new NotAvailableException("User"); }

            user.TeacherProfile = new TeacherProfile
            {
                About = dto.About
            };

            if (!(await paymentServiceClient.RegTeacherAsync(userId, user.DisplayName, dto.INN, dto.Phone, dto.AccountNumber, dto.BankBIC)))
            {
                throw new BusinessLogicException("Failed to cache teacher payment credits");
            }

            await dbIterator.SaveChangesAsync();

            return user.Id;
        }

        public async Task<int> CreateStudentProfile(int userId)
        {
            var user = await dbIterator.Users(false)
                .Include(x => x.StudentProfile)
                .One(x => x.Id == userId);
            if (user.StudentProfile is not null) { throw new BusinessLogicException("User is already student"); }
            if (!AvailabilityExpressions.IsUserAvailable.Compile()(user)) { throw new NotAvailableException("User"); }

            user.StudentProfile = new StudentProfile();

            await dbIterator.SaveChangesAsync();

            return user.Id;
        }

        public async Task<UserDto> GetUser(int id, bool isOnlyAvailable)
        {
            return await dbIterator.Users(isOnlyAvailable).ToDto().One(x => x.Id == id);
        }

        public async Task<UserDtoLite> GetUserLite(int id, bool isOnlyAvailable)
        {
            return await dbIterator.Users(isOnlyAvailable).ToDtoLite().One(x => x.Id == id);
        }

        public async Task<PageResult<UserDto>> GetUsers(string? displayName, bool isOnlyAvailable, int _page, int _pageSize)
        {
            return await dbIterator
                .Users(isOnlyAvailable)
                .WhereIf(!string.IsNullOrEmpty(displayName), e => EF.Functions.Like(
                    (
                         (e.Surname ?? "") + " " +
                         (e.Name ?? "") + " " +
                         (e.Patronymic ?? "") + " " +
                         e.Id).ToLower(),
                    $"%{displayName.ToLower()}%"))
                .ToDto()
                .ToPageResult(_page, _pageSize);
        }

        public async Task<string> GetMail(int userId)
        {
            return (await dbIterator.Users(false).One(x => x.Id == userId)).MailAdress;
        }
    }
}
