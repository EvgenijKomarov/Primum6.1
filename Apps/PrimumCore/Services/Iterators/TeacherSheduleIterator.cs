using CoreConnection.DTOs;
using CoreConnection.DTOs.Inputs;
using PrimumCore.Entities;
using CoreDBModel.Constants;
using CoreDBModel.Extensions;
using CoreDBModel.Models;
using Microsoft.EntityFrameworkCore;
using PrimumCore.Exceptions;
using PrimumCore.Extentions;
using PublishServiceConnection;
using PublishServiceConnection.Events;
using System.Linq.Expressions;

namespace PrimumCore.Services.Iterators
{
    public class TeacherSheduleIterator(DatabaseIterator dbIterator)
    {
        public async Task<PageResult<TeacherSheduleDto>> GetTeacherShedules(int teacherId, bool isOnlyAvailable, int _page, int _pageSize)
        {
            return await dbIterator.TeacherShedules(isOnlyAvailable)
                .Where(x => x.Teacher.User.Id == teacherId)
                .ToDto()
                .ToPageResult(_page, _pageSize);
        }

        public async Task<TeacherSheduleDto> GetTeacherShedule(int teacherId, int sheduleId, bool isOnlyAvailable)
        {
            return await dbIterator.TeacherShedules(isOnlyAvailable)
                .Where(x => x.Teacher.User.Id == teacherId)
                .ToDto()
                .One(x => x.Id == sheduleId);
        }

        public async Task<int> CreateTeacherShedule(int teacherId, TeacherSheduleInputDto sheduleDto)
        {
            var teacher = await dbIterator.Teachers(true)
                .Include(x => x.TeacherShedules)
                .One(x => x.User.Id == teacherId);
            if (!AvailabilityExpressions.IsTeacherAvailable.Compile()(teacher)) { throw new NotAvailableException("Teacher"); }

            if (teacher.TeacherShedules.Any(s => s.DayOfWeek == sheduleDto.DayOfWeek && s.Time == sheduleDto.Time)) 
                { throw new BusinessLogicException("Shedule already exists"); }

            var shedule = new TeacherShedule
            {
                Time = sheduleDto.Time,
                DayOfWeek = sheduleDto.DayOfWeek,
            };

            teacher.TeacherShedules.Add(shedule);
            await dbIterator.SaveChangesAsync();
            return shedule.Id;
        }

        public async Task<int> DeleteTeacherShedule(int teacherId, int sheduleId)
        {
            var teacher = await dbIterator.Teachers(true)
                .Include(x => x.TeacherShedules)
                .One(x => x.User.Id == teacherId);
            if (!AvailabilityExpressions.IsTeacherAvailable.Compile()(teacher)) { throw new NotAvailableException("Teacher"); }

            var shedule = teacher.TeacherShedules.FirstOrDefault(s => s.Id == sheduleId);
            if (shedule is null) { throw new NotFoundException("Shedule"); }
            if (!AvailabilityExpressions.IsTeacherSheduleAvailable.Compile()(shedule)) { throw new BusinessLogicException("Shedule already busy"); }

            teacher.TeacherShedules.Remove(shedule);
            await dbIterator.SaveChangesAsync();
            return shedule.Id;
        }
    }
}
