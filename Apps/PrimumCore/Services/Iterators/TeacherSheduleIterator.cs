using CoreConnection.DTOs;
using CoreConnection.DTOs.Inputs;
using CoreConnection.Entities;
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
    public class TeacherSheduleIterator(PrimumContext context, PublisherService publisher)
    {
        private IQueryable<TeacherShedule> TeacherShedules(bool isOnlyAvailable, Expression<Func<TeacherShedule, bool>>? predicate) => context
            .Set<TeacherShedule>()
            .WhereIf(predicate is not null, predicate!)
            .WhereIf(isOnlyAvailable, AvailabilityExpressions.IsTeacherSheduleAvailable)
            .Include(x => x.Teacher)
            .ThenInclude(x => x.User)
            .Include(x => x.AbonementShedule)
            .ThenInclude(x => x.Abonement)
            .ThenInclude(x => x.Student)
            .ThenInclude(x => x.User)
            .Include(x => x.AbonementShedule)
            .ThenInclude(x => x.Abonement)
            .ThenInclude(x => x.Course);

        public async Task<PageResult<TeacherSheduleDto>> GetTeacherShedules(int teacherId, bool isOnlyAvailable, int _page, int _pageSize)
        {
            return await TeacherShedules(isOnlyAvailable, x => x.Teacher.User.Id == teacherId).ToDto().ToPageResult(_page, _pageSize);
        }

        public async Task<TeacherSheduleDto> GetTeacherShedule(int teacherId, int sheduleId, bool isOnlyAvailable)
        {
            return await TeacherShedules(isOnlyAvailable, x => x.Teacher.User.Id == teacherId).ToDto().One(x => x.TeacherSheduleId == sheduleId);
        }

        public async Task<int> CreateTeacherShedule(int teacherId, TeacherSheduleInputDto sheduleDto)
        {
            var teacher = await context.Set<TeacherProfile>()
                .Include(a => a.TeacherShedules)
                .Include(a => a.User)
                .One(x => x.User.Id == teacherId);
            if (!AvailabilityExpressions.IsTeacherAvailable.Compile()(teacher.User)) { throw new NotAvailableException("Teacher"); }

            if (teacher.TeacherShedules.Any(s => s.DayOfWeek == sheduleDto.DayOfWeek && s.Time == sheduleDto.Time)) 
                { throw new BusinessLogicException("Shedule already exists"); }

            var shedule = new TeacherShedule
            {
                Time = sheduleDto.Time,
                DayOfWeek = sheduleDto.DayOfWeek,
            };

            teacher.TeacherShedules.Add(shedule);
            await context.SaveChangesAsync();
            return shedule.TeacherSheduleId;
        }

        public async Task<int> DeleteTeacherShedule(int teacherId, int sheduleId)
        {
            var teacher = await context.Set<TeacherProfile>()
                .Include(u => u.User)
                .Include(a => a.TeacherShedules)
                .ThenInclude(e => e.AbonementShedule)
                .One(x => x.User.Id == teacherId);
            if (!AvailabilityExpressions.IsTeacherAvailable.Compile()(teacher.User)) { throw new NotAvailableException("Teacher"); }

            var shedule = teacher.TeacherShedules.FirstOrDefault(s => s.TeacherSheduleId == sheduleId);
            if (shedule is null) { throw new NotFoundException("Shedule"); }
            if (!AvailabilityExpressions.IsTeacherSheduleAvailable.Compile()(shedule)) { throw new BusinessLogicException("Shedule already busy"); }

            teacher.TeacherShedules.Remove(shedule);
            await context.SaveChangesAsync();
            return shedule.TeacherSheduleId;
        }
    }
}
