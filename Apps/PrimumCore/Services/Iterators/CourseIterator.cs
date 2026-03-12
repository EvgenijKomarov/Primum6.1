using CoreConnection.DTOs;
using CoreConnection.DTOs.Inputs;
using CoreDBModel.Constants;
using CoreDBModel.Models;
using Microsoft.EntityFrameworkCore;
using PrimumCore.Exceptions;
using PrimumCore.Extentions;
using System.Linq;
using System.Linq.Expressions;

namespace PrimumCore.Services.Iterators
{
    public class CourseIterator(PrimumContext context)
    {
        private IQueryable<Course> Courses(bool isOnlyAvailable, Expression<Func<Course, bool>>? predicate) => context
            .Set<Course>()
            .WhereIf(isOnlyAvailable, AvailabilityExpressions.IsCourseAvailable)
            .WhereIf(predicate is not null, predicate!)
            .Include(x => x.CourseTheme)
            .Include(x => x.Teacher)
            .ThenInclude(x => x.User);

        private IQueryable<CourseDto> ToDto(IQueryable<Course> queryable) => queryable
            .Select(x => new CourseDto
            {
                CourseId = x.CourseId,
                Name = x.Name,
                About = x.About,
                TeacherName = x.Teacher.User.DisplayName,
                CourseThemeName = x.CourseTheme.ThemeName,
                CourseThemeId = x.CourseThemeId,
                TeacherId = x.Teacher.User.Id,
                Price = x.Price,
                MaxLessons = x.MaxLessons,
                FreeLessons = x.FreeLessons,
                TeacherAbout = x.Teacher.About,
                IsActive = x.IsActive,
                ApproveStatus = x.ApproveStatus
            });

        public async Task<IEnumerable<CourseDto>> GetCoursesByTeacher(int teacherId, bool isOnlyAvailable)
        {
            return await ToDto(
                    Courses(isOnlyAvailable, x => x.Teacher.User.Id == teacherId)
                ).ToArrayAsync();
        }

        public async Task<CourseDto> GetCourseByTeacher(int teacherId, int courseId, bool isOnlyAvailable)
        {
            return await ToDto(
                    Courses(isOnlyAvailable, x => x.Teacher.User.Id == teacherId)
                ).FirstOrDefaultAsync(x => x.CourseId == courseId) ?? throw new NotFoundException("Course");
        }

        public async Task<CourseDto> GetCourse(int courseId, bool isOnlyAvailable)
        {
            return await ToDto(
                    Courses(isOnlyAvailable, null)
                ).FirstOrDefaultAsync(x => x.CourseId == courseId) ?? throw new NotFoundException("Course");
        }

        public async Task<IEnumerable<CourseDto>> GetCourses(bool isOnlyAvailable)
        {
            return await ToDto(
                    Courses(isOnlyAvailable, null)
                ).ToArrayAsync();
        }

        public async Task<IEnumerable<CourseDto>> GetCoursesByTheme(int themeId, bool isOnlyAvailable)
        {
            return await ToDto(
                    Courses(isOnlyAvailable, x => x.CourseTheme.CourseThemeId == themeId)
                ).ToArrayAsync();
        }

        public async Task<int> EditCourse(int teacherId, int courseId, CourseInputDto courseDto)
        {   
            var course = await Courses(false, x => x.Teacher.User.Id == teacherId)
                .FirstOrDefaultAsync(x => x.CourseId == courseId) ?? throw new NotFoundException("Course");

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

            var course = new Course
            {
                Name = courseDto.Name,
                Price = courseDto.Price,
                MaxLessons = courseDto.MaxLessons,
                FreeLessons = courseDto.FreeLessons,
                CourseThemeId = courseDto.CourseThemeId,
                About = courseDto.Description
            };

            user.TeacherProfile.Courses.Add(course);
            await context.SaveChangesAsync();
            return course.CourseId;
        }

        public async Task<int> SwitchCourseActivity(int teacherId, int courseId, bool activity)
        {
            var course = await Courses(false, x => x.Teacher.User.Id == teacherId)
                .FirstOrDefaultAsync(x => x.CourseId == courseId) ?? throw new NotFoundException("Course");

            course.IsActive = activity;
            await context.SaveChangesAsync();
            return course.CourseId;
        }
    }
}
