using CoreConnection.DTOs;
using CoreConnection.Enums;
using Microsoft.EntityFrameworkCore;
using PrimumCore.Models;
using PrimumPlatformModel.Models.Enums;

namespace PrimumCore.Services
{
    public class TeacherIterator(IPrimumContext context)
    {
        public async Task<IEnumerable<LessonDto>> GetLessons(int userId)
        {
            var user = await context.Set<User>()
                .Include(u => u.TeacherProfile)
                .ThenInclude(t => t.Courses)
                .ThenInclude(s => s.Abonements)
                .ThenInclude(a => a.Lessons)
                .FirstOrDefaultAsync(x => x.Id == userId);
            if (user is null || user.TeacherProfile is null) { throw new Exception("Teacher not found"); }
            if (!user.IsActive) { throw new Exception("User is not active"); }

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
                    LessonLink = l.TeacherLink ?? string.Empty,
                    StudentDisplayName = l.Abonement.Student.User.DisplayName,
                    StudentId = l.Abonement.Student.User.Id,
                    TeacherDisplayName = user.DisplayName,
                    TeacherId = user.Id,
                    LessonStatus = (LessonStatusDto)l.Status
                });
        }

        public async Task<IEnumerable<CourseDto>> GetCourses(int userId)
        {
            var user = await context.Set<User>()
                .Include(u => u.TeacherProfile)
                .ThenInclude(a => a.Courses)
                .ThenInclude(a => a.Teacher)
                .FirstOrDefaultAsync(x => x.Id == userId);
            if (user is null || user.TeacherProfile is null) { throw new Exception("Teacher not found"); }
            if (!user.IsActive) { throw new Exception("User is not active"); }

            return user
                .TeacherProfile
                .Courses
                .Select(c => new CourseDto
                {
                    CourseId = c.CourseId,
                    Name = c.Name,
                    TeacherName = c.Teacher.User.DisplayName,
                    Price = c.Price,
                    CourseThemeName = c.CourseTheme.ThemeName,
                    CourseThemeId = c.CourseTheme.CourseThemeId,
                    MaxLessons = c.MaxLessons,
                    FreeLessons = c.FreeLessons,
                    TeacherAbout = c.Teacher.About,
                    ApproveStatus = (ApproveStatusDto)c.ApproveStatus,
                    IsActive = c.IsActive
                });
        }

        public async Task<int> EditCourse(int userId, CourseDto courseDto)
        {
            var user = await context.Set<User>()
                .Include(u => u.TeacherProfile)
                .ThenInclude(a => a.Courses)
                .FirstOrDefaultAsync(x => x.Id == userId);
            if (user is null || user.TeacherProfile is null) { throw new Exception("Teacher not found"); }
            if (!user.IsActive) { throw new Exception("User is not active"); }
            if (user.TeacherProfile.ApproveStatus != ApproveStatus.Approved) { throw new Exception("Teacher is not approved"); }

            var course = user
                .TeacherProfile
                .Courses
                .FirstOrDefault(c => c.CourseId == courseDto.CourseId);
            if (course is null) { throw new Exception("Course not found"); }

            course.Name = courseDto.Name;
            course.Price = courseDto.Price;
            course.MaxLessons = courseDto.MaxLessons;
            course.FreeLessons = courseDto.FreeLessons;

            await context.SaveChangesAsync();
            return course.CourseId;
        }

        public async Task<int> CreateCourse(int userId, CourseDto courseDto)
        {
            var user = await context.Set<User>()
                .Include(u => u.TeacherProfile)
                .ThenInclude(a => a.Courses)
                .FirstOrDefaultAsync(x => x.Id == userId);
            if (user is null || user.TeacherProfile is null) { throw new Exception("Teacher not found"); }
            if (!user.IsActive) { throw new Exception("User is not active"); }
            if (user.TeacherProfile.ApproveStatus != ApproveStatus.Approved) { throw new Exception("Teacher is not approved"); }

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
            if (user.TeacherProfile.ApproveStatus != ApproveStatus.Approved) { throw new Exception("Teacher is not approved"); }

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
            if (user.TeacherProfile.ApproveStatus != ApproveStatus.Approved) { throw new Exception("Teacher is not approved"); }

            var course = user
                .TeacherProfile
                .Courses
                .FirstOrDefault(c => c.CourseId == courseId);
            if (course is null) { throw new Exception("Course not found"); }

            course.IsActive = true;
            await context.SaveChangesAsync();
            return course.CourseId;
        }

        public async Task<int> CreateShedule(int userId, TeacherSheduleDto sheduleDto)
        {
            var user = await context.Set<User>()
                .Include(u => u.TeacherProfile)
                .ThenInclude(a => a.TeacherShedules)
                .FirstOrDefaultAsync(x => x.Id == userId);
            if (user is null || user.TeacherProfile is null) { throw new Exception("Teacher not found"); }
            if (!user.IsActive) { throw new Exception("User is not active"); }
            if (user.TeacherProfile.ApproveStatus != ApproveStatus.Approved) { throw new Exception("Teacher is not approved"); }

            if (user.TeacherProfile.TeacherShedules.Any(s => s.DayOfWeek == sheduleDto.DayOfWeek && s.Time == sheduleDto.Time)) { throw new Exception("Shedule already exists"); }

            var shedule = new TeacherShedule
            {
                Time = sheduleDto.Time,
                DayOfWeek = sheduleDto.DayOfWeek,
            };

            user.TeacherProfile.TeacherShedules.Add(shedule);
            await context.SaveChangesAsync();
            return shedule.TeacherSheduleId;
        }

        public async Task<IEnumerable<TeacherSheduleDto>> GetShedules(int userId)
        {
            var user = await context.Set<User>()
                .Include(u => u.TeacherProfile)
                .ThenInclude(a => a.TeacherShedules)
                .ThenInclude(x => x.AbonementShedule)
                .ThenInclude(x => x.Abonement)
                .ThenInclude(x => x.Student)
                .ThenInclude(x => x.User)
                .Include(u => u.TeacherProfile)
                .ThenInclude(a => a.TeacherShedules)
                .ThenInclude(x => x.AbonementShedule)
                .ThenInclude(x => x.Abonement)
                .ThenInclude(x => x.Course)
                .FirstOrDefaultAsync(x => x.Id == userId);
            if (user is null || user.TeacherProfile is null) { throw new Exception("Teacher not found"); }
            if (!user.IsActive) { throw new Exception("User is not active"); }

            return user
                .TeacherProfile
                .TeacherShedules
                .Select(s => new TeacherSheduleDto
                {
                    DayOfWeek = s.DayOfWeek,
                    Time = s.Time,
                    IsBusy = s.AbonementShedule is not null,
                    StudentName = s.AbonementShedule is null ? null : s.AbonementShedule.Abonement.Student.User.DisplayName,
                    StudentId = s.AbonementShedule is null ? null : s.AbonementShedule.Abonement.Student.User.Id,
                    CourseName = s.AbonementShedule is null ? null : s.AbonementShedule.Abonement.Course.Name,
                    CourseId = s.AbonementShedule is null ? null : s.AbonementShedule.Abonement.Course.CourseId
                });
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
            if (user.TeacherProfile.ApproveStatus != ApproveStatus.Approved) { throw new Exception("Teacher is not approved"); }

            var shedule = user.TeacherProfile.TeacherShedules.FirstOrDefault(s => s.TeacherSheduleId == sheduleId);
            if (shedule is null) { throw new Exception("Shedule not found"); }
            if (shedule.AbonementShedule is not null) { throw new Exception("Shedule already busy"); }

            user.TeacherProfile.TeacherShedules.Remove(shedule);
            await context.SaveChangesAsync();
            return shedule.TeacherSheduleId;
        }

        public async Task<IEnumerable<AbonementDto>> GetAbonements(int userId)
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
            if (!user.IsActive) { throw new Exception("User is not active"); }

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
                    AbonementStatus = (AbonementStatusDto)a.AbonementStatus
                });
        }
    }
}
