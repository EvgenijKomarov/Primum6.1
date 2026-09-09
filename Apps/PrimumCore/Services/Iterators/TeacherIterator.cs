using Common.Utilities;
using CoreConnection.DTOs;
using CoreDBModel.Models.Enums;
using Microsoft.EntityFrameworkCore;
using PrimumCore.Entities;
using PrimumCore.Extentions;

namespace PrimumCore.Services.Iterators
{
    public class TeacherIterator(DatabaseIterator dbIterator, ConverterToDateTimeService dateTimeService)
    {
        public async Task<PageResult<TeacherProfileDto>> GetTeachers(bool isOnlyAvailable, bool isConfidential, int _page, int _pageSize)
        {
            return await dbIterator.Teachers(isOnlyAvailable).ToDto(isConfidential).ToPageResult(_page, _pageSize);
        }

        public async Task<TeacherProfileDto> GetTeacher(int teacherId, bool isOnlyAvailable, bool isConfidential)
        {
            return await dbIterator.Teachers(isOnlyAvailable).ToDto(isConfidential).One(x => x.UserId == teacherId);
        }

        public async Task<IEnumerable<DateTime>> GetTeacherAvailableTime(int teacherId)
        {
            var fromDays = 1;
            var toDays = 7;

            var teacher = await dbIterator
                .Teachers(true)
                .Include(x => x.TeacherShedules)
                .ThenInclude(x => x.AbonementShedule)
                .Include(x => x.Courses)
                .ThenInclude(x => x.Abonements)
                .ThenInclude(x => x.Lessons)
                .One(x => x.UserId == teacherId);

            var busySchedules = teacher
                .Courses
                .SelectMany(x => x.Abonements)
                .SelectMany(x => x.Lessons)
                .Where(x => x.DateTime > DateTime.UtcNow.AddDays(fromDays) && x.DateTime < DateTime.UtcNow.AddDays(toDays))
                .Where(x => x.IsNormal())
                .Select(x => (x.DateTime.DayOfWeek, x.DateTime.Hour));

            return teacher
                .TeacherShedules
                .Where(x => !busySchedules.Any(y => y.DayOfWeek == x.DayOfWeek && y.Hour == x.Time))
                .Select(x => dateTimeService.GetNextSuitableDateNextWeek(x.DayOfWeek, x.Time))
                .Where(x => x < DateTime.UtcNow.AddDays(7)); //избыточная проверка, чтобы даты были точно в пределе одной недели
        }
    }
}
