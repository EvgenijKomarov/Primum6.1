using CoreConnection.DTOs;
using CoreConnection.DTOs.Inputs;
using CoreConnection.Enums;
using Microsoft.EntityFrameworkCore;
using PrimumCore.Models;

namespace PrimumCore.Services.Iterators
{
    public class TeacherIterator(IPrimumContext context)
    {
        public async Task<IEnumerable<LessonDto>> GetLessons(int userId, int subjectId)
        {
            var user = await context.Set<User>()
                .Include(u => u.TeacherProfile)
                .ThenInclude(t => t.Courses)
                .ThenInclude(s => s.Abonements)
                .ThenInclude(a => a.Lessons)
                .FirstOrDefaultAsync(x => x.Id == userId);
            if (user is null || user.TeacherProfile is null) { throw new Exception("Teacher not found"); }

            return user
                .TeacherProfile
                .Courses
                .SelectMany(x => x.Abonements)
                .SelectMany(x => x.Lessons)
                .Where(l => l.DateTime >= DateTime.Now)
                .Select(l => new LessonDto
                {
                    DateTime = l.DateTime,
                    CourseName = l.Abonement.Course.Name,
                    CourseId = l.Abonement.CourseId,
                    AbonementId = l.AbonementId,
                    Price = l.Price,
                    LessonLink = l.TeacherLink ?? string.Empty,
                    StudentDisplayName = l.Abonement.Student.User.DisplayName,
                    StudentId = l.Abonement.Student.User.Id,
                    TeacherDisplayName = user.DisplayName,
                    TeacherId = user.Id,
                    LessonStatus = l.Status.ToString()
                })
                .ToArray();
        }

        public async Task<IEnumerable<AbonementDto>> GetAbonements(int userId, int subjectId)
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
                .FirstOrDefaultAsync(x => x.Id == userId);
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
                    AbonementStatus = a.AbonementStatus.ToString()
                })
                .ToArray();
        }

        public async Task<int> EditCourse(int userId, int courseId, CourseInputDto courseDto)
        {
            var user = await context.Set<User>()
                .Include(u => u.TeacherProfile)
                .ThenInclude(a => a.Courses)
                .FirstOrDefaultAsync(x => x.Id == userId);
            if (user is null || user.TeacherProfile is null) { throw new Exception("Teacher not found"); }
            if (!user.IsActive) { throw new Exception("User is not active"); }
            if (user.TeacherProfile.IsAvailable) { throw new Exception("Teacher is not approved"); }

            var course = user
                .TeacherProfile
                .Courses
                .FirstOrDefault(c => c.CourseId == courseId);
            if (course is null) { throw new Exception("Course not found"); }

            course.Price = courseDto.Price;
            course.MaxLessons = courseDto.MaxLessons;
            course.FreeLessons = courseDto.FreeLessons;

            await context.SaveChangesAsync();
            return course.CourseId;
        }

        public async Task<int> CreateCourse(int userId, CourseInputDto courseDto)
        {
            var user = await context.Set<User>()
                .Include(u => u.TeacherProfile)
                .ThenInclude(a => a.Courses)
                .FirstOrDefaultAsync(x => x.Id == userId);
            if (user is null || user.TeacherProfile is null) { throw new Exception("Teacher not found"); }
            if (!user.IsActive) { throw new Exception("User is not active"); }
            if (user.TeacherProfile.IsAvailable) { throw new Exception("Teacher is not approved"); }

            var course = new Course
            {
                Name = courseDto.Name,
                Price = courseDto.Price,
                MaxLessons = courseDto.MaxLessons,
                FreeLessons = courseDto.FreeLessons,
                CourseThemeId = courseDto.CourseThemeId
            };

            user.TeacherProfile.Courses.Add(course);
            await context.SaveChangesAsync();
            return course.CourseId;
        }

        public async Task<int> DeactivateCourse(int userId, int courseId)
        {
            var user = await context.Set<User>()
                .Include(u => u.TeacherProfile)
                .ThenInclude(a => a.Courses)
                .FirstOrDefaultAsync(x => x.Id == userId);
            if (user is null || user.TeacherProfile is null) { throw new Exception("Teacher not found"); }
            if (!user.IsActive) { throw new Exception("User is not active"); }
            if (user.TeacherProfile.IsAvailable) { throw new Exception("Teacher is not approved"); }

            var course = user
                .TeacherProfile
                .Courses
                .FirstOrDefault(c => c.CourseId == courseId);
            if (course is null) { throw new Exception("Course not found"); }

            course.IsActive = false;
            await context.SaveChangesAsync();
            return course.CourseId;
        }

        public async Task<int> ActivateCourse(int userId, int courseId)
        {
            var user = await context.Set<User>()
                .Include(u => u.TeacherProfile)
                .ThenInclude(a => a.Courses)
                .FirstOrDefaultAsync(x => x.Id == userId);
            if (user is null || user.TeacherProfile is null) { throw new Exception("Teacher not found"); }
            if (!user.IsActive) { throw new Exception("User is not active"); }
            if (user.TeacherProfile.IsAvailable) { throw new Exception("Teacher is not approved"); }

            var course = user
                .TeacherProfile
                .Courses
                .FirstOrDefault(c => c.CourseId == courseId);
            if (course is null) { throw new Exception("Course not found"); }

            course.IsActive = true;
            await context.SaveChangesAsync();
            return course.CourseId;
        }

        public async Task<int> CreateShedule(int userId, TeacherSheduleInputDto sheduleDto)
        {
            var user = await context.Set<User>()
                .Include(u => u.TeacherProfile)
                .ThenInclude(a => a.TeacherShedules)
                .FirstOrDefaultAsync(x => x.Id == userId);
            if (user is null || user.TeacherProfile is null) { throw new Exception("Teacher not found"); }
            if (!user.IsActive) { throw new Exception("User is not active"); }
            if (user.TeacherProfile.IsAvailable) { throw new Exception("Teacher is not approved"); }

            if (Enum.TryParse(sheduleDto.DayOfWeek, out DayOfWeek dtoDayofWeek)) { throw new Exception("Can't parse day of week"); }
            if (user.TeacherProfile.TeacherShedules.Any(s => s.DayOfWeek == dtoDayofWeek && s.Time == sheduleDto.Time)) { throw new Exception("Shedule already exists"); }

            var shedule = new TeacherShedule
            {
                Time = sheduleDto.Time,
                DayOfWeek = dtoDayofWeek,
            };

            user.TeacherProfile.TeacherShedules.Add(shedule);
            await context.SaveChangesAsync();
            return shedule.TeacherSheduleId;
        }

        public async Task<int> DeleteShedule(int userId, int sheduleId)
        {
            var user = await context.Set<User>()
                .Include(u => u.TeacherProfile)
                .ThenInclude(a => a.TeacherShedules)
                .ThenInclude(e => e.AbonementShedule)
                .FirstOrDefaultAsync(x => x.Id == userId);
            if (user is null || user.TeacherProfile is null) { throw new Exception("Teacher not found"); }
            if (!user.IsActive) { throw new Exception("User is not active"); }
            if (user.TeacherProfile.IsAvailable) { throw new Exception("Teacher is not approved"); }

            var shedule = user.TeacherProfile.TeacherShedules.FirstOrDefault(s => s.TeacherSheduleId == sheduleId);
            if (shedule is null) { throw new Exception("Shedule not found"); }
            if (shedule.AbonementShedule is not null) { throw new Exception("Shedule already busy"); }

            user.TeacherProfile.TeacherShedules.Remove(shedule);
            await context.SaveChangesAsync();
            return shedule.TeacherSheduleId;
        }
    }
}
