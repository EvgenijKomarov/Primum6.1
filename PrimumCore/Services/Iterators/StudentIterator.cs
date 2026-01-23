using CoreConnection.DTOs;
using CoreConnection.Enums;
using CoreConnection.Notifications;
using Microsoft.EntityFrameworkCore;
using PrimumCore.Models;
using PrimumCore.Services.Connectors;
using PrimumCore.Services.Utilities;
using PrimumPlatformModel.Models.Enums;

namespace PrimumCore.Services.Iterators
{
    public class StudentIterator(IPrimumContext context, ConverterToDateTimeService dateTimeService, IPublisher publisher)
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
            if (!user.IsActive) { throw new Exception("User is not active"); }

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
                    Price = l.Price,
                    TeacherId = l.Abonement.Course.Teacher.User.Id,
                    LessonStatus = l.Status.ToString()
                })
                .ToArray();
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
            if (!user.IsActive) { throw new Exception("User is not active"); }

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
                    AbonementStatus = a.AbonementStatus.ToString()
                })
                .ToArray();
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
            if (!user.IsActive) { throw new Exception("User is not active"); }

            return user
                .StudentProfile
                .Abonements
                .SelectMany(a => a.AbonementShedules)
                .Select(x => new StudentSheduleDto
                {
                    DayOfWeek = x.TeacherShedule.DayOfWeek.ToString(),
                    Time = x.TeacherShedule.Time,
                    TeacherId = x.Abonement.Course.Teacher.User.Id,
                    CourseId = x.Abonement.Course.CourseId,
                    TeacherDisplayName = x.Abonement.Course.Teacher.User.DisplayName,
                    CourseName = x.Abonement.Course.Name
                })
                .ToArray();
        }

        public async Task<int> SubscribeToCourse(int userId, int courseId, int teacherSheduleId)
        {
            var user = await context.Set<User>()
                .Include(u => u.StudentProfile)
                .ThenInclude(s => s.Abonements)
                .ThenInclude(x => x.AbonementShedules)
                .ThenInclude(x => x.TeacherShedule)
                .Include(u => u.StudentProfile)
                .ThenInclude(s => s.Abonements)
                .ThenInclude(s => s.Course)
                .FirstOrDefaultAsync(x => x.Id == userId);
            if (user is null || user.StudentProfile is null) { throw new Exception("Student not found"); }
            if (!user.IsActive) { throw new Exception("User is not active"); }

            var course = await context.Set<Course>()
                .Where(x => x.IsAvailable)
                .FirstOrDefaultAsync(x => x.CourseId == courseId);
            if (course is null) { throw new Exception("Course not found"); }

            var teacherShedule = await context.Set<TeacherShedule>()
                .Include(x => x.Teacher)
                .ThenInclude(x => x.User)
                .Where(x => !x.IsBusy)
                .FirstOrDefaultAsync(x => x.TeacherSheduleId == teacherSheduleId);
            if (teacherShedule.Teacher.ApproveStatus != ApproveStatus.Approved) { throw new Exception("Teacher is not approved"); }
            if (teacherShedule is null) { throw new Exception("Shedule not found"); }
            if (teacherShedule.IsBusy) { throw new Exception("Shedule is busy"); }
            if (teacherShedule.Teacher.User.Id == userId) { throw new Exception("Student can't subscribe on himself"); }
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
            } else if (abonement.AbonementStatus == AbonementStatus.Deleted)
            {
                abonement.AbonementStatus = AbonementStatus.Active;
            }

            if (course.MaxLessons >= abonement.AbonementShedules.Count)
            {
                throw new Exception("Can't create more shedules than course's maximum shedules per week");
            }
            if (user
                .StudentProfile
                .Abonements
                .SelectMany(x => x.AbonementShedules)
                .Any(x => x.TeacherShedule.Time == teacherShedule.Time && x.TeacherShedule.DayOfWeek == teacherShedule.DayOfWeek))
            {
                throw new Exception("There is a same shedule in student profile");
            }

            var suitableDate = dateTimeService.GetNextFreeSuitableDateThisWeek(teacherShedule.DayOfWeek, teacherShedule.Time);
            var abonementShedule = new AbonementShedule
            {
                Abonement = abonement,
                TeacherShedule = teacherShedule,
                LastIteration = suitableDate,
            };

            await context.Set<AbonementShedule>().AddAsync(abonementShedule);
            await context.Set<Lesson>().AddAsync(new Lesson
            {
                Abonement = abonement,
                Price = abonement.Course.FreeLessons >= abonement.Lessons.Count() ? 0 : abonement.PricePerLesson,
                DateTime = suitableDate,
                Status = LessonStatus.Waiting
            });

            await context.SaveChangesAsync();

            await publisher.PublishAsync(new NewAbonementSheduleNotification
            {
                StudentName = user.DisplayName,
                StudentUserId = user.Id,
                TeacherName = teacherShedule.Teacher.User.DisplayName,
                TeacherUserId = teacherShedule.Teacher.User.Id,
                CourseName = course.Name,
                AbonementId = abonement.AbonementId,
                AbonementSheduleId = abonementShedule.AbonementSheduleId,
                DayOfWeek = teacherShedule.DayOfWeek.ToString(),
                Time = teacherShedule.Time
            });

            return abonementShedule.AbonementSheduleId;
        }

        public async Task<int> DeleteAbonement(int userId, int abonementId)
        {
            var user = await context.Set<User>()
                .Include(u => u.StudentProfile)
                .ThenInclude(s => s.Abonements)
                .FirstOrDefaultAsync(x => x.Id == userId);
            if (user is null || user.StudentProfile is null) { throw new Exception("Student not found"); }
            if (!user.IsActive) { throw new Exception("User is not active"); }

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
            if (!user.IsActive) { throw new Exception("User is not active"); }

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
                .ThenInclude(s => s.Course)
                .ThenInclude(s => s.Teacher)
                .ThenInclude(s => s.User)
                .FirstOrDefaultAsync(x => x.Id == userId);
            if (user is null || user.StudentProfile is null) { throw new Exception("Student not found"); }
            if (!user.IsActive) { throw new Exception("User is not active"); }

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

        public async Task<int> DeleteShedule(int userId, int abonementSheduleId)
        {
            var user = await context.Set<User>()
                .Include(u => u.StudentProfile)
                .ThenInclude(s => s.Abonements)
                .ThenInclude(s => s.Course)
                .Include(u => u.StudentProfile)
                .ThenInclude(s => s.Abonements)
                .ThenInclude(s => s.AbonementShedules)
                .ThenInclude(s => s.TeacherShedule)
                .ThenInclude(s => s.Teacher)
                .ThenInclude(s => s.User)
                .FirstOrDefaultAsync(x => x.Id == userId);
            if (user is null || user.StudentProfile is null) { throw new Exception("Student not found"); }
            if (!user.IsActive) { throw new Exception("User is not active"); }

            var abonementShedule = user
                .StudentProfile
                .Abonements
                .SelectMany(x => x.AbonementShedules)
                .FirstOrDefault(x => x.AbonementSheduleId == abonementSheduleId);

            abonementShedule.Abonement.AbonementShedules.Remove(abonementShedule);
            await context.SaveChangesAsync();

            await publisher.PublishAsync(new DeleteAbonementSheduleNotification
            {
                StudentName = user.DisplayName,
                StudentUserId = user.Id,
                TeacherName = abonementShedule.TeacherShedule.Teacher.User.DisplayName,
                TeacherUserId = abonementShedule.TeacherShedule.Teacher.User.Id,
                CourseName = abonementShedule.Abonement.Course.Name,
                AbonementId = abonementShedule.Abonement.AbonementId,
                AbonementSheduleId = abonementShedule.AbonementSheduleId,
                DayOfWeek = abonementShedule.TeacherShedule.DayOfWeek.ToString(),
                Time = abonementShedule.TeacherShedule.Time
            });

            return abonementShedule.AbonementSheduleId;
        }
    }
}
