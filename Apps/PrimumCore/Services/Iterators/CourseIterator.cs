using CoreConnection.DTOs;
using CoreConnection.DTOs.Inputs;
using CoreConnection.Enums;
using CoreDBModel.Constants;
using CoreDBModel.Models;
using Microsoft.EntityFrameworkCore;
using PrimumCore.Exceptions;
using PrimumCore.Extentions;
using System.Linq;

namespace PrimumCore.Services.Iterators
{
    public class CourseIterator(PrimumContext context)
    {
        public async Task<IEnumerable<CourseDto>> GetCoursesByTeacher(int userId, bool isOnlyAvailable)
        {
            var user = await context.Set<User>()
                .Include(x => x.TeacherProfile)
                .ThenInclude(x => x.Courses)
                .ThenInclude(x => x.CourseTheme)
                .FirstOrDefaultAsync(x => x.Id == userId);
            if (user is null || user.TeacherProfile is null) { throw new NotFoundException("Teacher"); }

            return user.TeacherProfile.Courses
                .WhereIf(isOnlyAvailable, AvailabilityExpressions.IsCourseAvailable)
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
                    ApproveStatus = (StatusApprove)x.ApproveStatus
                })
                .ToArray();
        }

        public async Task<CourseDto> GetCourse(int courseId, bool isOnlyAvailable)
        {
            var course = (await GetCourses(isOnlyAvailable)).FirstOrDefault(x => x.CourseId == courseId);

            if (course is null) { throw new NotFoundException("Course"); }

            return course;
        }

        public async Task<IEnumerable<CourseDto>> GetCourses(bool isOnlyAvailable)
        {
            return await context.Set<Course>()
                .Include(x => x.Teacher)
                .ThenInclude(x => x.User)
                .Include(x => x.CourseTheme)
                .WhereIf(isOnlyAvailable, AvailabilityExpressions.IsCourseAvailable)
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
                    ApproveStatus = (StatusApprove)x.ApproveStatus
                })
                .ToArrayAsync();
        }

        public async Task<IEnumerable<CourseDto>> GetCoursesByTheme(int themeId, bool isOnlyAvailable)
        {
            return await context.Set<CourseTheme>()
                .Include(x => x.Courses)
                .ThenInclude(x => x.Teacher)
                .ThenInclude(x => x.User)
                .SelectMany(x => x.Courses)
                .WhereIf(isOnlyAvailable, AvailabilityExpressions.IsCourseAvailable)
                .Where(x => x.CourseThemeId == themeId)
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
                    ApproveStatus = (StatusApprove)x.ApproveStatus
                })
                .ToArrayAsync();
        }

        public async Task<int> EditCourse(int teacherId, int courseId, CourseInputDto courseDto)
        {
            var user = await context.Set<User>()
                .Include(u => u.TeacherProfile)
                .ThenInclude(a => a.Courses)
                .FirstOrDefaultAsync(x => x.Id == teacherId);
            if (user is null || user.TeacherProfile is null) { throw new NotFoundException("Teacher"); }
            if (!AvailabilityExpressions.IsTeacherAvailable.Compile()(user)) 
                { throw new NotAvailableException("Teacher"); }

            var course = user
                .TeacherProfile
                .Courses
                .FirstOrDefault(c => c.CourseId == courseId);
            if (course is null) { throw new NotFoundException("Course"); }

            course.Price = courseDto.Price;
            course.MaxLessons = courseDto.MaxLessons;
            course.FreeLessons = courseDto.FreeLessons;

            await context.SaveChangesAsync();
            return course.CourseId;
        }

        public async Task<int> CreateCourse(int teacherId, CourseInputDto courseDto)
        {
            var user = await context.Set<User>()
                .Include(u => u.TeacherProfile)
                .ThenInclude(a => a.Courses)
                .FirstOrDefaultAsync(x => x.Id == teacherId);
            if (user is null || user.TeacherProfile is null) { throw new NotFoundException("Teacher"); }
            if (!AvailabilityExpressions.IsTeacherAvailable.Compile()(user)) 
                { throw new NotAvailableException("Teacher"); }

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

        public async Task<int> DeactivateCourse(int teacherId, int courseId)
        {
            var user = await context.Set<User>()
                .Include(u => u.TeacherProfile)
                .ThenInclude(a => a.Courses)
                .FirstOrDefaultAsync(x => x.Id == teacherId);
            if (user is null || user.TeacherProfile is null) { throw new NotFoundException("Teacher"); }
            if (!AvailabilityExpressions.IsTeacherAvailable.Compile()(user)) 
                { throw new NotAvailableException("Teacher"); }

            var course = user
                .TeacherProfile
                .Courses
                .FirstOrDefault(c => c.CourseId == courseId);
            if (course is null) { throw new NotFoundException("Course"); }

            course.IsActive = false;
            await context.SaveChangesAsync();
            return course.CourseId;
        }

        public async Task<int> ActivateCourse(int teacherId, int courseId)
        {
            var user = await context.Set<User>()
                .Include(u => u.TeacherProfile)
                .ThenInclude(a => a.Courses)
                .FirstOrDefaultAsync(x => x.Id == teacherId);
            if (user is null || user.TeacherProfile is null) { throw new NotFoundException("Teacher"); }
            if (!AvailabilityExpressions.IsTeacherAvailable.Compile()(user)) 
                { throw new NotAvailableException("Teacher"); }

            var course = user
                .TeacherProfile
                .Courses
                .FirstOrDefault(c => c.CourseId == courseId);
            if (course is null) { throw new NotFoundException("Course"); }

            course.IsActive = true;
            await context.SaveChangesAsync();
            return course.CourseId;
        }


    }
}
