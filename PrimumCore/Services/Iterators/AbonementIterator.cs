using CoreConnection.DTOs;
using CoreConnection.Enums;
using CoreConnection.Notifications;
using Microsoft.EntityFrameworkCore;
using PrimumCore.Models;
using PrimumCore.Services.Connectors;
using PrimumPlatformModel.Models.Enums;

namespace PrimumCore.Services.Iterators
{
    public class AbonementIterator(IPrimumContext context, IPublisher publisher)
    {
        public async Task<IEnumerable<AbonementDto>> GetTeacherAbonements(int teacherId)
        {
            var user = await context.Set<User>()
                .Include(u => u.TeacherProfile)
                .ThenInclude(a => a.Courses)
                .ThenInclude(a => a.Abonements)
                .ThenInclude(a => a.Student)
                .ThenInclude(a => a.User)
                .Include(u => u.TeacherProfile)
                .ThenInclude(a => a.Courses)
                .ThenInclude(a => a.CourseTheme)
                .FirstOrDefaultAsync(x => x.Id == teacherId);
            if (user is null || user.TeacherProfile is null) { throw new Exception("Teacher not found"); }

            return user
                .TeacherProfile
                .Courses
                .SelectMany(x => x.Abonements)
                .Select(a => new AbonementDto
                {
                    StudentId = a.Student.User.Id,
                    StudentDisplayName = a.Student.User.DisplayName,
                    TeacherId = user.Id,
                    TeacherDisplayName = user.DisplayName,
                    AbonementId = a.AbonementId,
                    CourseName = a.Course.Name,
                    CourseId = a.Course.CourseId,
                    CourseThemeName = a.Course.CourseTheme.ThemeName,
                    CourseThemeId = a.Course.CourseTheme.CourseThemeId,
                    PricePerLesson = a.PricePerLesson,
                    AbonementStatus = (StatusAbonement)a.AbonementStatus
                })
                .ToArray();
        }

        public async Task<IEnumerable<AbonementDto>> GetStudentAbonements(int studentId)
        {
            var user = await context.Set<User>()
                .Include(u => u.StudentProfile)
                .ThenInclude(s => s.Abonements)
                .ThenInclude(a => a.Course)
                .ThenInclude(c => c.Teacher)
                .ThenInclude(c => c.User)
                .Include(u => u.StudentProfile)
                .ThenInclude(s => s.Abonements)
                .ThenInclude(a => a.Course)
                .ThenInclude(a => a.CourseTheme)
                .FirstOrDefaultAsync(x => x.Id == studentId);
            if (user is null || user.StudentProfile is null) { throw new Exception("Student not found"); }

            return user
                .StudentProfile
                .Abonements
                .Select(a => new AbonementDto
                {
                    StudentDisplayName = user.DisplayName,
                    StudentId = user.Id,
                    TeacherDisplayName = a.Course.Teacher.User.DisplayName,
                    TeacherId = a.Course.Teacher.User.Id,
                    CourseName = a.Course.Name,
                    CourseId = a.CourseId,
                    CourseThemeName = a.Course.CourseTheme.ThemeName,
                    CourseThemeId = a.Course.CourseTheme.CourseThemeId,
                    AbonementId = a.AbonementId,
                    PricePerLesson = a.PricePerLesson,
                    AbonementStatus = (StatusAbonement)a.AbonementStatus
                })
                .ToArray();
        }

        public async Task<int> DeleteAbonement(int studentId, int abonementId)
        {
            var user = await context.Set<User>()
                .Include(u => u.StudentProfile)
                .ThenInclude(s => s.Abonements)
                .FirstOrDefaultAsync(x => x.Id == studentId);
            if (user is null || user.StudentProfile is null) { throw new Exception("Student not found"); }

            var abonement = user
                .StudentProfile
                .Abonements
                .FirstOrDefault(x => x.AbonementId == abonementId);
            if (abonement is null) { throw new Exception("Abonement not found"); }

            abonement.AbonementStatus = AbonementStatus.Deleted;
            await context.SaveChangesAsync();
            return abonement.AbonementId;
        }

        public async Task<int> FreezeAbonement(int studentId, int abonementId)
        {
            var user = await context.Set<User>()
                .Include(u => u.StudentProfile)
                .ThenInclude(s => s.Abonements)
                .FirstOrDefaultAsync(x => x.Id == studentId);
            if (user is null || user.StudentProfile is null) { throw new Exception("Student not found"); }

            var abonement = user
                .StudentProfile
                .Abonements
                .FirstOrDefault(x => x.AbonementId == abonementId);
            if (abonement is null) { throw new Exception("Abonement not found"); }

            abonement.AbonementStatus = AbonementStatus.Freezed;
            await context.SaveChangesAsync();
            return abonement.AbonementId;
        }

        public async Task<int> ActivateAbonement(int studentId, int abonementId)
        {
            var user = await context.Set<User>()
                .Include(u => u.StudentProfile)
                .ThenInclude(s => s.Abonements)
                .ThenInclude(s => s.Course)
                .ThenInclude(s => s.Teacher)
                .ThenInclude(s => s.User)
                .FirstOrDefaultAsync(x => x.Id == studentId);
            if (user is null || user.StudentProfile is null) { throw new Exception("Student not found"); }

            var abonement = user
                .StudentProfile
                .Abonements
                .FirstOrDefault(x => x.AbonementId == abonementId);
            if (abonement is null) { throw new Exception("Abonement not found"); }

            abonement.AbonementStatus = AbonementStatus.Active;
            await context.SaveChangesAsync();
            await publisher.PublishAsync(new AbonementChangeStatusNotification
            {
                StudentName = user.DisplayName,
                StudentUserId = user.Id,
                TeacherName = abonement.Course.Teacher.User.DisplayName,
                TeacherUserId = abonement.Course.Teacher.User.Id,
                CourseName = abonement.Course.Name,
                AbonementId = abonement.AbonementId,
                AbonementStatus = abonement.AbonementStatus.ToString()
            });

            return abonement.AbonementId;
        }
    }
}
