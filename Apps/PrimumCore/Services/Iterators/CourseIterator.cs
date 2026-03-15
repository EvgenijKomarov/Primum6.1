using CoreConnection.DTOs;
using CoreConnection.DTOs.Inputs;
using CoreConnection.Entities;
using CoreDBModel.Models;
using PrimumCore.Extentions;

namespace PrimumCore.Services.Iterators
{
    public class CourseIterator(DatabaseIterator dbIterator)
    {
        public async Task<PageResult<CourseDto>> GetCoursesByTeacher(int teacherId, bool isOnlyAvailable, int _page, int _pageSize)
        {
            return await dbIterator.Courses(isOnlyAvailable)
                .Where(x => x.Teacher.User.Id == teacherId)
                .ToDto()
                .ToPageResult(_page, _pageSize);
        }

        public async Task<CourseDto> GetCourseByTeacher(int teacherId, int courseId, bool isOnlyAvailable)
        {
            return await dbIterator.Courses(isOnlyAvailable)
                .Where(x => x.Teacher.User.Id == teacherId)
                .ToDto()
                .One(x => x.Id == courseId);
        }

        public async Task<PageResult<CourseDto>> GetCourses(bool isOnlyAvailable, int _page, int _pageSize)
        {
            return await dbIterator.Courses(isOnlyAvailable).ToDto().ToPageResult(_page, _pageSize);
        }

        public async Task<CourseDto> GetCourse(int courseId, bool isOnlyAvailable)
        {
            return await dbIterator.Courses(isOnlyAvailable).ToDto().One(x => x.Id == courseId);
        }

        public async Task<PageResult<CourseDto>> GetCoursesByTheme(int themeId, bool isOnlyAvailable, int _page, int _pageSize)
        {
            return await dbIterator.Courses(isOnlyAvailable)
                .Where(x => x.CourseTheme.Id == themeId)
                .ToDto()
                .ToPageResult(_page, _pageSize);
        }

        public async Task<int> EditCourse(int teacherId, int courseId, CourseInputDto courseDto)
        {   
            var course = await dbIterator.Courses(false)
                .Where(x => x.Teacher.User.Id == teacherId)
                .One(x => x.Id == courseId);

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
                About = courseDto.Description
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
