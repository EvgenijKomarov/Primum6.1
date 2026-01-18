using CoreConnection.DTOs;
using DTO.DTOs;
using DTO.Enums;
using Microsoft.EntityFrameworkCore;
using PrimumCore.Models;
using PrimumPlatformModel.Models.Enums;

namespace PrimumCore.Services
{
    public class StudentIterator(IPrimumContext context)
    {
        public async Task<IEnumerable<LessonDto>> GetLessons(int userId)
        {
            var user = await context.Set<User>()
                .Include(u => u.StudentProfile)
                .ThenInclude(s => s.Abonements)
                .ThenInclude(a => a.Lessons)
                .Include(u => u.StudentProfile)
                .ThenInclude(s => s.Abonements)
                .ThenInclude(a => a.Course)
                .ThenInclude(c => c.Teacher)
                .FirstOrDefaultAsync(x => x.Id == userId);

            if (user is null || user.StudentProfile is null) { throw new Exception("Student not found"); }

            return user
                .StudentProfile
                .Abonements
                .SelectMany(x => x.Lessons)
                .Select(l => new LessonDto
                {
                    DateTime = l.DateTime,
                    CourseName = l.Abonement.Course.Name,
                    CourseId = l.Abonement.CourseId,
                    AbonementId = l.AbonementId,
                    LessonLink = l.StudentLink ?? string.Empty,
                    TeacherDisplayName = l.Abonement.Course.Teacher.User.DisplayName,
                    StudentDisplayName = user.DisplayName,
                    StudentId = user.Id,
                    TeacherId = l.Abonement.Course.Teacher.User.Id,
                    LessonStatus = (LessonStatusDto)l.Status
                });
        }

        public async Task<IEnumerable<AbonementDto>> GetAbonements(int userId)
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
                .FirstOrDefaultAsync(x => x.Id == userId);
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
                    AbonementStatus = (AbonementStatusDto)a.AbonementStatus
                });
        }

        public async Task<IEnumerable<StudentSheduleDto>> GetShedules(int userId)
        {
            var user = await context.Set<User>()
                .Include(u => u.StudentProfile)
                .ThenInclude(s => s.Abonements)
                .ThenInclude(x => x.AbonementShedules)
                .ThenInclude(x => x.TeacherShedule)
                .Include(u => u.StudentProfile)
                .ThenInclude(s => s.Abonements)
                .ThenInclude(s => s.Course)
                .ThenInclude(s => s.Teacher)
                .ThenInclude(s => s.User)
                .FirstOrDefaultAsync(x => x.Id == userId);
            if (user is null || user.StudentProfile is null) { throw new Exception("Student not found"); }

            return user
                .StudentProfile
                .Abonements
                .SelectMany(a => a.AbonementShedules)
                .Select(x => new StudentSheduleDto
                {
                    DayOfWeek = x.TeacherShedule.DayOfWeek,
                    Time = x.TeacherShedule.Time,
                    TeacherId = x.Abonement.Course.Teacher.User.Id,
                    CourseId = x.Abonement.Course.CourseId,
                    TeacherDisplayName = x.Abonement.Course.Teacher.User.DisplayName,
                    CourseName = x.Abonement.Course.Name
                });
        }

        public async Task<int> SubscribeToCourse(int userId, int courseId, int teacherSheduleId)
        {
            var user = await context.Set<User>()
                .Include(u => u.StudentProfile)
                .ThenInclude(s => s.Abonements)
                .ThenInclude(x => x.AbonementShedules)
                .ThenInclude(x => x.TeacherShedule)
                .FirstOrDefaultAsync(x => x.Id == userId);
            if (user is null || user.StudentProfile is null) { throw new Exception("Student not found"); }

            var course = await context.Set<Course>()
                .Where(x => x.IsAvailable)
                .FirstOrDefaultAsync(x => x.CourseId == courseId);
            if (course is null) { throw new Exception("Course not found"); }

            var teacherShedule = await context.Set<TeacherShedule>()
                .Include(x => x.Teacher)
                .ThenInclude(x => x.User)
                .Where(x => !x.IsBusy)
                .FirstOrDefaultAsync(x => x.TeacherSheduleId == teacherSheduleId);
            if (teacherShedule is null) { throw new Exception("Shedule not found"); }
            if (teacherShedule.IsBusy) { throw new Exception("Shedule is busy"); }
            if (teacherShedule.Teacher.User.Id == userId) { throw new Exception("Student cant subscribe on himself"); }
            if (user
                .StudentProfile
                .Abonements
                .SelectMany(x => x.AbonementShedules)
                .Any(x => x.TeacherShedule.DayOfWeek == teacherShedule.DayOfWeek && x.TeacherShedule.Time == teacherShedule.Time))
            {
                throw new Exception("Same shedule already exists");
            }

            var abonement = user.StudentProfile.Abonements.FirstOrDefault(a => a.CourseId == courseId);

            if (abonement is null)
            {
                abonement = new Abonement
                {
                    CourseId = courseId,
                    StudentId = userId,
                    PricePerLesson = course.Price
                };
                await context.Set<Abonement>().AddAsync(abonement);
            }

            if (course.MaxLessons >= abonement.AbonementShedules.Count) { throw new Exception("Can't create more shedules than course's maximum shedules per week"); }

            var abonementShedule = new AbonementShedule
            {
                AbonementId = abonement.AbonementId,
                TeacherSheduleId = teacherSheduleId
            };

            await context.Set<AbonementShedule>().AddAsync(abonementShedule);
            await context.SaveChangesAsync();

            return abonementShedule.AbonementSheduleId;
        }

        public async Task<int> DeleteAbonement(int userId, int abonementId)
        {
            var user = await context.Set<User>()
                .Include(u => u.StudentProfile)
                .ThenInclude(s => s.Abonements)
                .FirstOrDefaultAsync(x => x.Id == userId);
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

        public async Task<int> FreezeAbonement(int userId, int abonementId)
        {
            var user = await context.Set<User>()
                .Include(u => u.StudentProfile)
                .ThenInclude(s => s.Abonements)
                .FirstOrDefaultAsync(x => x.Id == userId);
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

        public async Task<int> ActivateAbonement(int userId, int abonementId)
        {
            var user = await context.Set<User>()
                .Include(u => u.StudentProfile)
                .ThenInclude(s => s.Abonements)
                .FirstOrDefaultAsync(x => x.Id == userId);
            if (user is null || user.StudentProfile is null) { throw new Exception("Student not found"); }

            var abonement = user
                .StudentProfile
                .Abonements
                .FirstOrDefault(x => x.AbonementId == abonementId);
            if (abonement is null) { throw new Exception("Abonement not found"); }

            abonement.AbonementStatus = AbonementStatus.Active;
            await context.SaveChangesAsync();
            return abonement.AbonementId;
        }

        public async Task<int> DeleteShedule(int userId, int abonementSheduleId)
        {
            var user = await context.Set<User>()
                .Include(u => u.StudentProfile)
                .ThenInclude(s => s.Abonements)
                .ThenInclude(s => s.AbonementShedules)
                .ThenInclude(s => s.Abonement)
                .FirstOrDefaultAsync(x => x.Id == userId);
            if (user is null || user.StudentProfile is null) { throw new Exception("Student not found"); }

            var abonementShedule = user
                .StudentProfile
                .Abonements
                .SelectMany(x => x.AbonementShedules)
                .FirstOrDefault(x => x.AbonementSheduleId == abonementSheduleId);

            abonementShedule.Abonement.AbonementShedules.Remove(abonementShedule);
            await context.SaveChangesAsync();
            return abonementShedule.AbonementSheduleId;
        }
    }
}
