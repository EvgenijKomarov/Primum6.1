using CoreConnection.DTOs;
using CoreConnection.Entities;
using CoreDBModel.Constants;
using CoreDBModel.Models;
using Microsoft.EntityFrameworkCore;
using PrimumCore.Exceptions;
using PrimumCore.Extentions;
using PrimumCore.Services.Utilities;
using System.ComponentModel.DataAnnotations;

namespace PrimumCore.Services.Iterators
{
    public class UserIterator(DatabaseIterator dbIterator, PasswordHasher passwordHasher)
    {
        public async Task<int> Login(string mailAdress, string password)
        {
            var user = await dbIterator.Users(false)
                .FirstOrDefaultAsync(x => x.MailAdress == mailAdress) ?? throw new NotFoundException("User");

            if (user.IsBanned) { throw new BusinessLogicException("User is banned"); }

            if (!passwordHasher.VerifyPassword(password, user.Password)) { throw new BusinessLogicException("Wrong password"); }
            return user.Id;
        }

        public async Task<long> AddMoney(int userId, long cash)
        {
            var user = await dbIterator.Users(true)
                .FirstOrDefaultAsync(x => x.Id == userId) ?? throw new NotFoundException("User");

            user.Cash += cash;

            await dbIterator.SaveChangesAsync();
            return user.Cash;
        }

        public async Task<int> RegUser(RegistrationInputDto dto)
        {
            if (!new EmailAddressAttribute().IsValid(dto.MailAdress))
            { throw new BusinessLogicException("Adress not valid"); }

            if (await dbIterator.Users(false)
                .AnyAsync(x => x.MailAdress == dto.MailAdress))
            { throw new BusinessLogicException("User with the same adress already exists"); }

            var user = new User
            {
                Name = dto.Name,
                Surname = dto.Surname,
                Patronymic = dto.Patronymic,
                MailAdress = dto.MailAdress,
                Password = passwordHasher.HashPassword(dto.Password),
                Cash = 0
            };
            await dbIterator.AddAsync(user);
            await dbIterator.SaveChangesAsync();

            return user.Id;
        }

        public async Task<int> CreateTeacherProfile(int userId, string about)
        {
            var user = await dbIterator.Users(false)
                .FirstOrDefaultAsync(x => x.Id == userId) ?? throw new NotFoundException("User");
            if (user.TeacherProfile is not null) { throw new BusinessLogicException("User is already teacher"); }
            if (!AvailabilityExpressions.IsUserAvailable.Compile()(user)) { throw new NotAvailableException("User"); }

            user.TeacherProfile = new TeacherProfile
            {
                About = about
            };

            await dbIterator.SaveChangesAsync();

            return user.Id;
        }

        public async Task<int> CreateStudentProfile(int userId)
        {
            var user = await dbIterator.Users(false)
                .FirstOrDefaultAsync(x => x.Id == userId) ?? throw new NotFoundException("User");
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

        public async Task<PageResult<UserDto>> GetUsers(bool isOnlyAvailable, int _page, int _pageSize)
        {
            return await dbIterator.Users(isOnlyAvailable).ToDto().ToPageResult(_page, _pageSize);
        }

        public async Task<string> GetMail(int userId)
        {
            return (await dbIterator.Users(false).One(x => x.Id == userId)).MailAdress;
        }
    }
}
