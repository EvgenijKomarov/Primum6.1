using CoreConnection.DTOs;
using CoreDBModel.Constants;
using CoreDBModel.Models;
using CoreDBModel.Models.Enums;
using Microsoft.EntityFrameworkCore;
using PrimumCore.Exceptions;
using PrimumCore.Extentions;
using PrimumCore.Services.Utilities;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace PrimumCore.Services.Iterators
{
    public class UserIterator(PrimumContext context, PasswordHasher passwordHasher)
    {
        private IQueryable<User> Users(bool isOnlyAvailable, Expression<Func<User, bool>>? predicate) => context
            .Set<User>()
            .WhereIf(isOnlyAvailable, AvailabilityExpressions.IsUserAvailable)
            .WhereIf(predicate is not null, predicate!)
            .Include(u => u.TeacherProfile)
            .Include(u => u.StudentProfile)
            .Include(u => u.AdminProfile)
            .IgnoreQueryFilters();

        private IQueryable<UserDto> ToDto(IQueryable<User> queryable) => queryable
            .Select(user => new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Patronymic = user.Patronymic,
                DisplayName = user.DisplayName,
                Cash = user.Cash,
                IsApprovedStudent = user.StudentProfile != null ?
                        user.StudentProfile.ApproveStatus == ApproveStatus.Approved : (bool?)null,
                IsApprovedTeacher = user.TeacherProfile != null ?
                        user.TeacherProfile.ApproveStatus == ApproveStatus.Approved : (bool?)null,
                IsAdmin = user.AdminProfile != null,
                IsBanned = user.IsBanned,
                MailConfirmed = user.IsMailChecked,
                IsAvailable = AvailabilityExpressions.IsUserAvailable.Compile()(user)
            });

        private IQueryable<UserDtoLite> ToDtoLite(IQueryable<User> queryable) => queryable
            .Select(user => new UserDtoLite
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Patronymic = user.Patronymic,
                DisplayName = user.DisplayName,
                IsApprovedStudent = user.StudentProfile != null ?
                        user.StudentProfile.ApproveStatus == ApproveStatus.Approved : (bool?)null,
                IsApprovedTeacher = user.TeacherProfile != null ?
                        user.TeacherProfile.ApproveStatus == ApproveStatus.Approved : (bool?)null,
                IsAdmin = user.AdminProfile != null,
                IsAvailable = AvailabilityExpressions.IsUserAvailable.Compile()(user)
            });

        public async Task<int> Login(string mailAdress, string password)
        {
            var user = await Users(false, null).FirstOrDefaultAsync(x => x.MailAdress == mailAdress) ?? throw new NotFoundException("User");

            if (user.IsBanned) { throw new BusinessLogicException("User is banned"); }

            if (!passwordHasher.VerifyPassword(password, user.Password)) { throw new BusinessLogicException("Wrong password"); }
            return user.Id;
        }

        public async Task<long> AddMoney(int userId, long cash)
        {
            var user = await Users(true, null)
                .FirstOrDefaultAsync(x => x.Id == userId) ?? throw new NotFoundException("User");

            user.Cash += cash;

            await context.SaveChangesAsync();
            return user.Cash;
        }

        public async Task<int> RegUser(RegistrationInputDto dto)
        {
            if (!new EmailAddressAttribute().IsValid(dto.MailAdress))
            { throw new BusinessLogicException("Adress not valid"); }

            if (await Users(false, null)
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
            context.Set<User>().Add(user);
            await context.SaveChangesAsync();

            return user.Id;
        }

        public async Task<int> CreateTeacherProfile(int userId, string about)
        {
            var user = await Users(false, null)
                .FirstOrDefaultAsync(x => x.Id == userId) ?? throw new NotFoundException("User");
            if (user.TeacherProfile is not null) { throw new BusinessLogicException("User is already teacher"); }
            if (!AvailabilityExpressions.IsUserAvailable.Compile()(user)) { throw new NotAvailableException("User"); }

            user.TeacherProfile = new TeacherProfile
            {
                About = about
            };

            await context.SaveChangesAsync();

            return user.Id;
        }

        public async Task<int> CreateStudentProfile(int userId)
        {
            var user = await Users(false, null)
                .FirstOrDefaultAsync(x => x.Id == userId) ?? throw new NotFoundException("User");
            if (user.StudentProfile is not null) { throw new BusinessLogicException("User is already student"); }
            if (!AvailabilityExpressions.IsUserAvailable.Compile()(user)) { throw new NotAvailableException("User"); }

            user.StudentProfile = new StudentProfile();

            await context.SaveChangesAsync();

            return user.Id;
        }

        public async Task<UserDto> GetUser(int id, bool isOnlyAvailable)
        {
            var user = await ToDto(
                    Users(isOnlyAvailable, null)
                ).FirstOrDefaultAsync(x => x.Id == id) ?? throw new NotFoundException("User");

            return user;
        }

        public async Task<UserDtoLite> GetLiteUser(int id, bool isOnlyAvailable)
        {
            var user = await ToDtoLite(
                    Users(isOnlyAvailable, null)
                ).FirstOrDefaultAsync(x => x.Id == id) ?? throw new NotFoundException("User");

            return user;
        }

        public async Task<IEnumerable<UserDto>> GetUsers(bool isOnlyAvailable)
        {
            var user = await ToDto(
                    Users(isOnlyAvailable, null)
                ).ToArrayAsync();

            return user;
        }

        public async Task<string> GetMail(int userId)
        {
            var user = await Users(true, null)
                .FirstOrDefaultAsync(x => x.Id == userId);
            if (user is null) { return string.Empty; }
            return user.MailAdress;
        }
    }
}
