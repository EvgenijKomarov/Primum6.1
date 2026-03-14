using CoreConnection.DTOs;
using CoreConnection.DTOs.Inputs;
using CoreConnection.Entities;
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

        public async Task<PageResult<CourseDto>> GetCoursesByTeacher(int teacherId, bool isOnlyAvailable, int _page, int _pageSize)
        {
            return await Courses(isOnlyAvailable, x => x.Teacher.User.Id == teacherId).ToDto().ToPageResult(_page, _pageSize);
        }

        public async Task<CourseDto> GetCourseByTeacher(int teacherId, int courseId, bool isOnlyAvailable)
        {
            return await Courses(isOnlyAvailable, x => x.Teacher.User.Id == teacherId).ToDto().One(x => x.CourseId == courseId);
        }

        public async Task<PageResult<CourseDto>> GetCourses(bool isOnlyAvailable, int _page, int _pageSize)
        {
            return await Courses(isOnlyAvailable, null).ToDto().ToPageResult(_page, _pageSize);
        }

        public async Task<CourseDto> GetCourse(int courseId, bool isOnlyAvailable)
        {
            return await Courses(isOnlyAvailable, null).ToDto().One(x => x.CourseId == courseId);
        }

        public async Task<PageResult<CourseDto>> GetCoursesByTheme(int themeId, bool isOnlyAvailable, int _page, int _pageSize)
        {
            return await Courses(isOnlyAvailable, x => x.CourseTheme.Id == themeId).ToDto().ToPageResult(_page, _pageSize);
        }

        public async Task<int> EditCourse(int teacherId, int courseId, CourseInputDto courseDto)
        {   
            var course = await Courses(false, x => x.Teacher.User.Id == teacherId)
                .One(x => x.Id == courseId);

            course.Price = courseDto.Price;
            course.MaxLessons = courseDto.MaxLessons;
            course.FreeLessons = courseDto.FreeLessons;

            await context.SaveChangesAsync();
            return course.Id;
        }

        public async Task<int> CreateCourse(int teacherId, CourseInputDto courseDto)
        {
            var teacher = await context.Set<TeacherProfile>()
                .Include(u => u.User)
                .Include(a => a.Courses)
                .One(x => x.User.Id == teacherId);

            var course = new Course
            {
                Name = courseDto.Name,
                Price = courseDto.Price,
                MaxLessons = courseDto.MaxLessons,
                FreeLessons = courseDto.FreeLessons,
                CourseThemeId = courseDto.CourseThemeId,
                About = courseDto.Description
            };

            teacher.Courses.Add(course);
            await context.SaveChangesAsync();
            return course.Id;
        }

        public async Task<int> SwitchCourseActivity(int teacherId, int courseId, bool activity)
        {
            var course = await Courses(false, x => x.Teacher.User.Id == teacherId)
                .One(x => x.Id == courseId);

            course.IsActive = activity;
            await context.SaveChangesAsync();
            return course.Id;
        }
    }
}
