using CoreConnection.DTOs;
using Microsoft.EntityFrameworkCore;
using PrimumCore.Models;
using PrimumCore.Utilities;
using PrimumPlatformModel.Models.Enums;

namespace PrimumCore.Services
{
    public class UserIterator(IPrimumContext context, PasswordHasher passwordHasher)
    {
        public async Task<object> GetUser(int id)
        {
            var user = await context.Set<User>()
                .Include(u => u.StudentProfile)
                .Include(u => u.TeacherProfile)
                .Include(u => u.AdminProfile)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (user is null) { throw new Exception("User not found"); }

            return new
            {
                UserDTO = new UserDto
                {
                    Id = user.Id,
                    Name = user.Name,
                    Surname = user.Surname,
                    Patronymic = user.Patronymic
                },
                IsApprovedStudent = user.StudentProfile is not null ?
                    user.StudentProfile.ApproveStatus == ApproveStatus.Approved : (bool?)null,
                IsApprovedTeacher = user.TeacherProfile is not null ?
                    user.TeacherProfile.ApproveStatus == ApproveStatus.Approved : (bool?)null,
                IsAdmin = user.AdminProfile is not null
            };
        }

        public async Task<int> Login(string login, string password)
        {
            var user = await context.Set<User>()
                .FirstOrDefaultAsync(x => x.Login == login);
            if (user is null) { throw new Exception("Wrong login"); }

            if (!passwordHasher.VerifyPassword(password, user.Password)) { throw new Exception("Wrong password"); }
            return user.Id;
        }

        public async Task<int> RegStudent(RegistrationDto dto)
        {
            if (await context.Set<User>().AnyAsync(x => x.Login == dto.Login)) { throw new Exception("User with the same login already exists"); }

            var user = new User
            {
                Name = dto.Name,
                Surname = dto.Surname,
                Patronymic = dto.Patronymic,
                Login = dto.Login,
                Password = passwordHasher.HashPassword(dto.Password),
                StudentProfile = new StudentProfile()
            };

            context.Set<User>().Add(user);
            await context.SaveChangesAsync();

            return user.Id;
        }

        public async Task<int> RegTeacher(RegistrationDto dto)
        {
            if (await context.Set<User>().AnyAsync(x => x.Login == dto.Login)) { throw new Exception("User with the same login already exists"); }

            var user = new User
            {
                Name = dto.Name,
                Surname = dto.Surname,
                Patronymic = dto.Patronymic,
                Login = dto.Login,
                Password = passwordHasher.HashPassword(dto.Password),
                TeacherProfile = new TeacherProfile
                {
                    About = dto.About
                }
            };

            context.Set<User>().Add(user);
            await context.SaveChangesAsync();

            return user.Id;
        }
    }
}
