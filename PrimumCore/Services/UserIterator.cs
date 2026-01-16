using DTO.DTOs;
using Microsoft.EntityFrameworkCore;
using PrimumCore.Models;
using PrimumPlatformModel.Models.Enums;

namespace PrimumCore.Services
{
    public class UserIterator(IPrimumContext context)
    {
        public UserDTO GetUser(int id)
        {
            var user = context.Set<User>()
                .Include(u => u.StudentProfile)
                .Include(u => u.TeacherProfile)
                .Include(u => u.AdminProfile)
                .FirstOrDefault(x => x.Id == id);
            if (user is null) { throw new Exception("User not found"); }

            return new UserDTO
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Patronymic = user.Patronymic,
                Password = user.Password,
                IsApprovedStudent = user.StudentProfile is not null ?
                    user.StudentProfile.ApproveStatus == ApproveStatus.Approved : null,
                IsApprovedTeacher = user.TeacherProfile is not null ?
                    user.TeacherProfile.ApproveStatus == ApproveStatus.Approved : null,
                IsApprovedAdmin = user.AdminProfile is not null
            };
        }

        public async Task<int> RegStudent(UserDTO dto)
        {
            var user = new User
            {
                Name = dto.Name,
                Surname = dto.Surname,
                Patronymic = dto.Patronymic,
                Password = dto.Password,
                StudentProfile = new StudentProfile()
            };

            context.Set<User>().Add(user);
            await context.SaveChangesAsync();

            return user.Id;
        }

        public async Task<int> RegTeacher(UserDTO dto, string about)
        {
            var user = new User
            {
                Name = dto.Name,
                Surname = dto.Surname,
                Patronymic = dto.Patronymic,
                Password = dto.Password,
                TeacherProfile = new TeacherProfile
                {
                    About = about
                }
            };

            context.Set<User>().Add(user);
            await context.SaveChangesAsync();

            return user.Id;
        }
    }
}
