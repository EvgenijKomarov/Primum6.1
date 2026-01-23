using CoreConnection.DTOs;
using CoreConnection.Enums;
using Microsoft.EntityFrameworkCore;
using PrimumCore.Extentions;
using PrimumCore.Models;
using PrimumPlatformModel.Models.Enums;

namespace PrimumCore.Services.Iterators
{
    public class CommonIterator(IPrimumContext context)
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
                    Patronymic = user.Patronymic,
                    DisplayName = user.DisplayName,
                    Cash = user.Cash,
                },
                IsApprovedStudent = user.StudentProfile is not null ?
                    user.StudentProfile.ApproveStatus == ApproveStatus.Approved : (bool?)null,
                IsApprovedTeacher = user.TeacherProfile is not null ?
                    user.TeacherProfile.ApproveStatus == ApproveStatus.Approved : (bool?)null,
                IsAdmin = user.AdminProfile is not null
            };
        }

        public async Task<IEnumerable<TeacherProfileDto>> GetTeachers(bool isOnlyAvailable)
        {
            return await context.Set<User>()
                .Include(x => x.TeacherProfile)
                .Where(x => x.TeacherProfile != null)
                .WhereIf(isOnlyAvailable, x => x.TeacherProfile.IsAvailable)
                .Select(x => new TeacherProfileDto
                {
                    DisplayName = x.DisplayName,
                    About = x.TeacherProfile.About,
                    UserId = x.Id,
                    IsAvailable = x.TeacherProfile.IsAvailable
                })
                .ToArrayAsync();
        }

        public async Task<TeacherProfileDto> GetTeacher(int userId)
        {
            var user = await context.Set<User>()
                .Include(x => x.TeacherProfile)
                .Where(x => x.TeacherProfile != null)
                .Select(x => new TeacherProfileDto
                {
                    DisplayName = x.DisplayName,
                    About = x.TeacherProfile.About,
                    UserId = x.Id,
                    IsAvailable = x.TeacherProfile.IsAvailable
                })
                .FirstOrDefaultAsync(x => x.UserId == userId);
            if (user is null) { throw new Exception("Teacher not found"); }

            return user;
        }

        public async Task<IEnumerable<CourseDto>> GetCoursesByTeacher(int userId, bool isOnlyAvailable)
        {
            var user = await context.Set<User>()
                .Include(x => x.TeacherProfile)
                .ThenInclude(x => x.Courses)
                .ThenInclude(x => x.Teacher)
                .FirstOrDefaultAsync(x => x.Id == userId);
            if (user is null || user.TeacherProfile is null) { throw new Exception("Teacher not found"); }

            return user.TeacherProfile.Courses
                .WhereIf(isOnlyAvailable, x => x.IsAvailable)
                .Select(x => new CourseDto
                {
                    CourseId = x.CourseId,
                    Name = x.Name,
                    TeacherName = x.Teacher.User.DisplayName,
                    CourseThemeName = x.CourseTheme.ThemeName,
                    CourseThemeId = x.CourseThemeId,
                    TeacherId = x.Teacher.User.Id,
                    Price = x.Price,
                    MaxLessons = x.MaxLessons,
                    FreeLessons = x.FreeLessons,
                    TeacherAbout = x.Teacher.About,
                    IsActive = x.IsActive,
                    ApproveStatus = x.ApproveStatus.ToString()
                })
                .ToArray();
        }

        public async Task<IEnumerable<TeacherSheduleDto>> GetTeacherShedules(int userId, bool isOnlyAvailable)
        {
            var user = await context.Set<User>()
                .Include(x => x.TeacherProfile)
                .ThenInclude(x => x.TeacherShedules)
                .ThenInclude(x => x.AbonementShedule)
                .ThenInclude(x => x.Abonement)
                .ThenInclude(x => x.Student)
                .ThenInclude(x => x.User)
                .Include(x => x.TeacherProfile)
                .ThenInclude(x => x.TeacherShedules)
                .ThenInclude(x => x.AbonementShedule)
                .ThenInclude(x => x.Abonement)
                .ThenInclude(x => x.Course)
                .FirstOrDefaultAsync(x => x.Id == userId);
            if (user is null || user.TeacherProfile is null) { throw new Exception("Teacher not found"); }

            return user.TeacherProfile
                .TeacherShedules
                .WhereIf(isOnlyAvailable, x => !x.IsBusy)
                .Select(x => new TeacherSheduleDto
                {
                    DayOfWeek = x.DayOfWeek.ToString(),
                    Time = x.Time,
                    IsBusy = x.IsBusy,
                    StudentName = x.AbonementShedule is not null ? x.AbonementShedule.Abonement.Student.User.DisplayName : null,
                    StudentId = x.AbonementShedule is not null ? x.AbonementShedule.Abonement.Student.User.Id : null,
                    CourseName = x.AbonementShedule is not null ? x.AbonementShedule.Abonement.Course.Name : null,
                    CourseId = x.AbonementShedule is not null ? x.AbonementShedule.Abonement.Course.CourseId : null,
                }
                )
                .ToArray();
        }

        public async Task<IEnumerable<CourseThemeDto>> GetThemes(bool isOnlyAvailable)
        {
            return await context.Set<CourseTheme>()
                .Include(x => x.Courses)
                .ThenInclude(x => x.Teacher)
                .ThenInclude(x => x.User)
                .WhereIf(isOnlyAvailable,x => x.IsActive)
                .Select(x => new CourseThemeDto
                {
                    CourseThemeId = x.CourseThemeId,
                    ThemeName = x.ThemeName,
                    IsActive = x.IsActive
                })
                .ToArrayAsync();
        }

        public async Task<CourseThemeDto> GetTheme(int themeId)
        {
            var theme = await context.Set<CourseTheme>()
                .Include(x => x.Courses)
                .ThenInclude(x => x.Teacher)
                .ThenInclude(x => x.User)
                .Where(x => x.IsActive)
                .Select(x => new CourseThemeDto
                {
                    CourseThemeId = x.CourseThemeId,
                    ThemeName = x.ThemeName,
                    IsActive = x.IsActive
                })
                .FirstOrDefaultAsync(x => x.CourseThemeId == themeId);
            if (theme is null) { throw new Exception("Theme not found"); }
            return theme;
        }

        public async Task<CourseDto> GetCourse(int courseId)
        {
            var course = await context.Set<Course>()
                .Include(x => x.Teacher)
                .ThenInclude(x => x.User)
                .Include(x => x.CourseTheme)
                .Where(x => x.IsAvailable)
                .Select(x => new CourseDto
                {
                    CourseId = x.CourseId,
                    Name = x.Name,
                    TeacherName = x.Teacher.User.DisplayName,
                    CourseThemeName = x.CourseTheme.ThemeName,
                    CourseThemeId = x.CourseThemeId,
                    TeacherId = x.Teacher.User.Id,
                    Price = x.Price,
                    MaxLessons = x.MaxLessons,
                    FreeLessons = x.FreeLessons,
                    TeacherAbout = x.Teacher.About,
                    IsActive = x.IsActive,
                    ApproveStatus = x.ApproveStatus.ToString()
                })
                .FirstOrDefaultAsync(x => x.CourseId == courseId);

            if (course is null) { throw new Exception("Course not found"); }

            return course;
        }

        public async Task<IEnumerable<CourseDto>> GetCourses(bool isOnlyAvailable)
        {
            return await context.Set<Course>()
                .Include(x => x.Teacher)
                .ThenInclude(x => x.User)
                .Include(x => x.CourseTheme)
                .WhereIf(isOnlyAvailable, x => x.IsAvailable)
                .Select(x => new CourseDto
                {
                    CourseId = x.CourseId,
                    Name = x.Name,
                    TeacherName = x.Teacher.User.DisplayName,
                    CourseThemeName = x.CourseTheme.ThemeName,
                    CourseThemeId = x.CourseThemeId,
                    TeacherId = x.Teacher.User.Id,
                    Price = x.Price,
                    MaxLessons = x.MaxLessons,
                    FreeLessons = x.FreeLessons,
                    TeacherAbout = x.Teacher.About,
                    IsActive = x.IsActive,
                    ApproveStatus = x.ApproveStatus.ToString()
                })
                .ToArrayAsync();
        }

        public async Task<IEnumerable<CourseDto>> GetCoursesByTheme(int themeId, bool isOnlyAvailable)
        {
            var theme = await GetTheme(themeId);

            return await context.Set<Course>()
                .Include(x => x.Teacher)
                .ThenInclude(x => x.User)
                .Include(x => x.CourseTheme)
                .WhereIf(isOnlyAvailable, x => x.IsAvailable)
                .Where(x => x.CourseThemeId == theme.CourseThemeId)
                .Select(x => new CourseDto
                {
                    CourseId = x.CourseId,
                    Name = x.Name,
                    TeacherName = x.Teacher.User.DisplayName,
                    CourseThemeName = x.CourseTheme.ThemeName,
                    CourseThemeId = x.CourseThemeId,
                    TeacherId = x.Teacher.User.Id,
                    Price = x.Price,
                    MaxLessons = x.MaxLessons,
                    FreeLessons = x.FreeLessons,
                    TeacherAbout = x.Teacher.About,
                    IsActive = x.IsActive,
                    ApproveStatus = x.ApproveStatus.ToString()
                })
                .ToArrayAsync();
        }

        public async Task<IEnumerable<StudentSheduleDto>> GetAbonementShedules(int abonementId)
        {
            var abonement = await context.Set<Abonement>()
                .Include(x => x.AbonementShedules)
                .ThenInclude(x => x.TeacherShedule)
                .Include(x => x.Course)
                .ThenInclude(x => x.Teacher)
                .ThenInclude(x => x.User)
                .FirstOrDefaultAsync(x => x.AbonementId == abonementId);
            if (abonement is null) { throw new Exception("Abonement not found"); }

            return abonement.AbonementShedules.Select(x => new StudentSheduleDto
            {
                DayOfWeek = x.TeacherShedule.DayOfWeek.ToString(),
                Time = x.TeacherShedule.Time,
                CourseName = x.Abonement.Course.Name,
                CourseId = x.Abonement.Course.CourseId,
                TeacherDisplayName = x.Abonement.Course.Teacher.User.DisplayName,
                TeacherId = x.Abonement.Course.Teacher.User.Id
            });
        }

        public async Task<IEnumerable<LessonDto>> GetAbonementLessons(int abonementId, bool isStudent)
        {
            var abonement = await context.Set<Abonement>()
                .Include(x => x.Lessons)
                .Include(x => x.Student)
                .ThenInclude(x => x.User)
                .Include(x => x.Course)
                .ThenInclude(x => x.Teacher)
                .ThenInclude(x => x.User)
                .FirstOrDefaultAsync(x => x.AbonementId == abonementId);
            if (abonement is null) { throw new Exception("Abonement not found"); }

            return abonement.Lessons.Select(x => new LessonDto
            {
                DateTime = x.DateTime,
                CourseName = x.Abonement.Course.Name,
                CourseId = x.Abonement.Course.CourseId,
                TeacherDisplayName = x.Abonement.Course.Teacher.User.DisplayName,
                TeacherId = x.Abonement.Course.Teacher.User.Id,
                StudentDisplayName = x.Abonement.Student.User.DisplayName,
                StudentId = x.Abonement.Student.User.Id,
                LessonLink = isStudent ? x.StudentLink : x.TeacherLink,
                AbonementId = x.Abonement.AbonementId,
                Price = x.Price,
                LessonStatus = x.Status.ToString()
            });
        }
    }
}
