using CoreConnection.DTOs;
using CoreConnection.Enums;
using Microsoft.EntityFrameworkCore;
using PrimumCore.Models;

namespace PrimumCore.Services
{
    public class CommonIterator(IPrimumContext context)
    {
        public async Task<IEnumerable<TeacherProfileDto>> GetTeachers()
        {
            return await context.Set<User>()
                .Include(x => x.TeacherProfile)
                .Where(x => x.TeacherProfile != null)
                .Select(x => new TeacherProfileDto
                {
                    DisplayName = x.DisplayName,
                    About = x.TeacherProfile.About,
                    TeacherId = x.Id,
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
                    TeacherId = x.Id,
                    IsAvailable = x.TeacherProfile.IsAvailable
                })
                .FirstOrDefaultAsync(x => x.TeacherId == userId);
            if (user is null) { throw new Exception("Teacher not found"); }

            return user;
        }

        public async Task<IEnumerable<CourseDto>> GetCoursesByTeacher(int userId)
        {
            var user = await context.Set<User>()
                .Include(x => x.TeacherProfile)
                .ThenInclude(x => x.Courses)
                .FirstOrDefaultAsync(x => x.Id == userId);
            if (user is null || user.TeacherProfile is null) { throw new Exception("Teacher not found"); }

            return user.TeacherProfile.Courses
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
                    ApproveStatus = (ApproveStatusDto)x.ApproveStatus
                })
                .ToArray();
        }

        public async Task<IEnumerable<TeacherSheduleDto>> GetTeacherShedules(int userId)
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
                .Select(x => new TeacherSheduleDto
                {
                    DayOfWeek = x.DayOfWeek,
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

        public async Task<IEnumerable<CourseThemeDto>> GetThemes()
        {
            return await context.Set<CourseTheme>()
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
                    ApproveStatus = (ApproveStatusDto)x.ApproveStatus
                })
                .FirstOrDefaultAsync(x => x.CourseId == courseId);

            if (course is null) { throw new Exception("Course not found"); }

            return course;
        }

        public async Task<IEnumerable<CourseDto>> GetCourses()
        {
            return await context.Set<Course>()
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
                    ApproveStatus = (ApproveStatusDto)x.ApproveStatus
                })
                .ToArrayAsync();
        }

        public async Task<IEnumerable<CourseDto>> GetCoursesByTheme(int themeId)
        {
            var theme = await GetTheme(themeId);

            return await context.Set<Course>()
                .Include(x => x.Teacher)
                .ThenInclude(x => x.User)
                .Include(x => x.CourseTheme)
                .Where(x => x.IsAvailable)
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
                    ApproveStatus = (ApproveStatusDto)x.ApproveStatus
                })
                .ToArrayAsync();
        }
    }
}
