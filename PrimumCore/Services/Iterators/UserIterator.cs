using CoreConnection.DTOs;
using CoreConnection.Notifications;
using Microsoft.EntityFrameworkCore;
using PrimumCore.Constants;
using PrimumCore.Extentions;
using PrimumCore.Models;
using PrimumCore.Models.Enums;
using PrimumCore.Services.Utilities;
using PrimumPlatformModel.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace PrimumCore.Services.Iterators
{
    public class UserIterator(IPrimumContext context, 
        PasswordHasher passwordHasher)
    {
        public async Task<(int?, string)> Login(string mailAdress, string password)
        {
            var user = await context.Set<User>()
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(x => x.MailAdress == mailAdress);
            if (user is null) { return (null, "Unknown login"); }
            if (user.IsBanned) { return (null, "User is banned"); }

            if (!passwordHasher.VerifyPassword(password, user.Password)) { return (null, "Wrong password"); }
            return (user.Id, string.Empty);
        }

        public async Task<long> AddMoney(int userId, long cash)
        {
            var user = await context.Set<User>()
                .FirstOrDefaultAsync(x => x.Id == userId);
            if (user is null) { throw new Exception("User not found"); }

            user.Cash += cash;

            await context.SaveChangesAsync();
            return user.Cash;
        }

        public async Task<long> GetMoney(int userId, long cash)
        {
            var user = await context.Set<User>()
                .FirstOrDefaultAsync(x => x.Id == userId);
            if (user is null) { throw new Exception("User not found"); }

            if (user.Cash < cash) { cash = user.Cash; }
            user.Cash -= cash;

            await context.SaveChangesAsync();
            return cash;
        }

        public async Task<int> RegUser(RegistrationInputDto dto)
        {
            if (!new EmailAddressAttribute().IsValid(dto.MailAdress)) 
            { throw new Exception("Adress not valid"); }

            if (await context.Set<User>()
                .IgnoreQueryFilters()
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

        public async Task<int> CreateTeacherProfile(int userId, string about)
        {
            var user = await context.Set<User>()
                .Include(u => u.TeacherProfile)
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(x => x.Id == userId);
            if (user is null) { throw new Exception("User not found"); }
            if (user.TeacherProfile is not null) { throw new Exception("User is already teacher"); }
            if (!AvailabilityExpressions.IsUserAvailable.Compile()(user)) { throw new Exception("User is not enabled"); }

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
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(x => x.Id == userId);
            if (user is null) { throw new Exception("User not found"); }
            if (user.StudentProfile is not null) { throw new Exception("User is already student"); }
            if (!AvailabilityExpressions.IsUserAvailable.Compile()(user)) { throw new Exception("User is not enabled"); }

            user.StudentProfile = new StudentProfile();

            await context.SaveChangesAsync();

            return user.Id;
        }

        public async Task<UserDto> GetUser(int id, bool isOnlyAvailable)
        {
            var user = (await GetUsers(isOnlyAvailable))
                .FirstOrDefault(x => x.Id == id);
            if (user is null) { throw new Exception("User not found"); }

            return user;
        }

        public async Task<IEnumerable<UserDto>> GetUsers(bool isOnlyAvailable)
        {
            return await context.Set<User>()
                .Include(u => u.StudentProfile)
                .Include(u => u.TeacherProfile)
                .Include(u => u.AdminProfile)
                .IgnoreQueryFilters()
                .WhereIf(isOnlyAvailable, AvailabilityExpressions.IsUserAvailable)
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
                    MailConfirmed = user.IsMailChecked
                })
                .ToArrayAsync();
        }
    }
}
