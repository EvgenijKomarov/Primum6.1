using CoreConnection.DTOs;
using Microsoft.EntityFrameworkCore;
using PrimumCore.Models;
using PrimumCore.Utilities;
using PrimumPlatformModel.Models.Enums;

namespace PrimumCore.Services.Iterators
{
    public class UserIterator(IPrimumContext context, PasswordHasher passwordHasher)
    {
        public async Task<int> Login(string login, string password)
        {
            var user = await context.Set<User>()
                .FirstOrDefaultAsync(x => x.Login == login);
            if (user is null) { throw new Exception("Wrong login"); }

            if (!passwordHasher.VerifyPassword(password, user.Password)) { throw new Exception("Wrong password"); }
            return user.Id;
        }

        public async Task<int> RegUser(RegistrationDto dto)
        {
            if (await context.Set<User>().AnyAsync(x => x.Login == dto.Login)) { throw new Exception("User with the same login already exists"); }

            var user = new User
            {
                Name = dto.Name,
                Surname = dto.Surname,
                Patronymic = dto.Patronymic,
                Login = dto.Login,
                Password = passwordHasher.HashPassword(dto.Password)
            };

            context.Set<User>().Add(user);
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
    }
}
