using CoreConnection.DTOs;
using CoreConnection.DTOs.Inputs;
using CoreDBModel.Models;
using CoreDBModel.Models.Enums;
using PrimumCore.Entities;
using PrimumCore.Extentions;
using PrimumCore.Services.Utilities;
using SolutionConfiguration;

namespace PrimumCore.Services.Iterators
{
    public class CourseIterator(DatabaseIterator dbIterator, RandomStringGenerator generator, ConfigurationClient configClient)
    {
        public async Task<PageResult<CourseDto>> GetCoursesByTeacher(int teacherId, bool isOnlyAvailable, int _page, int _pageSize)
        {
            return await dbIterator.Courses(isOnlyAvailable)
                .Where(x => x.Teacher.User.Id == teacherId)
                .ToDto(await configClient.GetGatewayUrl())
                .ToPageResult(_page, _pageSize);
        }

        public async Task<CourseDto> GetCourseByTeacher(int teacherId, int courseId, bool isOnlyAvailable)
        {
            return await dbIterator.Courses(isOnlyAvailable)
                .Where(x => x.Teacher.User.Id == teacherId)
                .ToDto(await configClient.GetGatewayUrl())
                .One(x => x.Id == courseId);
        }

        public async Task<PageResult<CourseDtoLite>> GetCourses(bool isOnlyAvailable, int _page, int _pageSize)
        {
            return await dbIterator.Courses(isOnlyAvailable).ToDtoLite().ToPageResult(_page, _pageSize);
        }

        public async Task<CourseDtoLite> GetCourse(int courseId, bool isOnlyAvailable)
        {
            return await dbIterator.Courses(isOnlyAvailable).ToDtoLite().One(x => x.Id == courseId);
        }

        public async Task<PageResult<CourseDtoLite>> GetCoursesByTheme(int themeId, bool isOnlyAvailable, int _page, int _pageSize)
        {
            return await dbIterator.Courses(isOnlyAvailable)
                .Where(x => x.CourseTheme.Id == themeId)
                .ToDtoLite()
                .ToPageResult(_page, _pageSize);
        }

        public async Task<int> EditCourse(int teacherId, int courseId, CourseInputDto courseDto)
        {   
            var course = await dbIterator.Courses(false)
                .Where(x => x.Teacher.User.Id == teacherId)
                .One(x => x.Id == courseId);

            if(courseDto.Name != course.Name || courseDto.Description != course.About)
            {
                course.ApproveStatus = ApproveStatus.NeedModeratorReview;
                course.Name = courseDto.Name;
                course.About = courseDto.Description;
            }

            course.Price = courseDto.Price;
            course.MaxLessons = courseDto.MaxLessons;
            course.FreeLessons = courseDto.FreeLessons;

            await dbIterator.SaveChangesAsync();
            return course.Id;
        }

        public async Task<int> CreateCourse(int teacherId, CourseInputDto courseDto)
        {
            var teacher = await dbIterator.Teachers(true)
                .One(x => x.User.Id == teacherId);

            var course = new Course
            {
                Name = courseDto.Name,
                Price = courseDto.Price,
                MaxLessons = courseDto.MaxLessons,
                FreeLessons = courseDto.FreeLessons,
                CourseThemeId = courseDto.CourseThemeId,
                About = courseDto.Description,
                ReferalToken = $"{teacherId}:{generator.GenerateRandomString()}"
            };

            teacher.Courses.Add(course);
            await dbIterator.SaveChangesAsync();
            return course.Id;
        }

        public async Task<int> SwitchCourseActivity(int teacherId, int courseId, bool activity)
        {
            var course = await dbIterator.Courses(false)
                .Where(x => x.Teacher.User.Id == teacherId)
                .One(x => x.Id == courseId);

            course.IsActive = activity;
            await dbIterator.SaveChangesAsync();
            return course.Id;
        }
    }
}
