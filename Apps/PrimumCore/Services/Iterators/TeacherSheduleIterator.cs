using CoreConnection.DTOs;
using CoreConnection.DTOs.Inputs;
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
        private IQueryable<TeacherShedule> Themes(bool isOnlyAvailable, Expression<Func<TeacherShedule, bool>>? predicate) => context
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

        private IQueryable<TeacherSheduleDto> ToDto(IQueryable<TeacherShedule> queryable) => queryable
            .Select(x => new TeacherSheduleDto
            {
                TeacherSheduleId = x.TeacherSheduleId,
                DayOfWeek = x.DayOfWeek,
                Time = x.Time,
                IsAvailable = AvailabilityExpressions.IsTeacherSheduleAvailable.Compile()(x),
                StudentName = x.AbonementShedule == null ? null : x.AbonementShedule.Abonement.Student.User.DisplayName,
                StudentId = x.AbonementShedule == null ? null : x.AbonementShedule.Abonement.Student.User.Id,
                CourseName = x.AbonementShedule == null ? null : x.AbonementShedule.Abonement.Course.Name,
                CourseId = x.AbonementShedule == null ? null : x.AbonementShedule.Abonement.Course.CourseId,
            });

        public async Task<IEnumerable<TeacherSheduleDto>> GetTeacherShedules(int teacherId, bool isOnlyAvailable)
        {
            return await ToDto(
                    Themes(isOnlyAvailable, x => x.Teacher.User.Id == teacherId)
                ).ToArrayAsync();
        }

        public async Task<TeacherSheduleDto> GetTeacherShedule(int teacherId, int sheduleId, bool isOnlyAvailable)
        {
            return await ToDto(
                    Themes(isOnlyAvailable, x => x.Teacher.User.Id == teacherId)
                ).FirstOrDefaultAsync(x => x.TeacherSheduleId == sheduleId) ?? throw new NotFoundException("Shedule");
        }

        public async Task<int> CreateTeacherShedule(int teacherId, TeacherSheduleInputDto sheduleDto)
        {
            var user = await context.Set<User>()
                .Include(u => u.TeacherProfile)
                .ThenInclude(a => a.TeacherShedules)
                .FirstOrDefaultAsync(x => x.Id == teacherId);
            if (user is null || user.TeacherProfile is null) { throw new NotFoundException("Teacher"); }
            if (!AvailabilityExpressions.IsTeacherAvailable.Compile()(user)) { throw new NotAvailableException("Teacher"); }

            if (user.TeacherProfile.TeacherShedules.Any(s => s.DayOfWeek == sheduleDto.DayOfWeek && s.Time == sheduleDto.Time)) 
                { throw new BusinessLogicException("Shedule already exists"); }

            var shedule = new TeacherShedule
            {
                Time = sheduleDto.Time,
                DayOfWeek = sheduleDto.DayOfWeek,
            };

            user.TeacherProfile.TeacherShedules.Add(shedule);
            await context.SaveChangesAsync();
            return shedule.TeacherSheduleId;
        }

        public async Task<int> DeleteTeacherShedule(int teacherId, int sheduleId)
        {
            var user = await context.Set<User>()
                .Include(u => u.TeacherProfile)
                .ThenInclude(a => a.TeacherShedules)
                .ThenInclude(e => e.AbonementShedule)
                .FirstOrDefaultAsync(x => x.Id == teacherId);
            if (user is null || user.TeacherProfile is null) { throw new NotFoundException("Teacher"); }
            if (!AvailabilityExpressions.IsTeacherAvailable.Compile()(user)) { throw new BusinessLogicException("Teacher"); }

            var shedule = user.TeacherProfile.TeacherShedules.FirstOrDefault(s => s.TeacherSheduleId == sheduleId);
            if (shedule is null) { throw new NotFoundException("Shedule"); }
            if (!AvailabilityExpressions.IsTeacherSheduleAvailable.Compile()(shedule)) { throw new BusinessLogicException("Shedule already busy"); }

            user.TeacherProfile.TeacherShedules.Remove(shedule);
            await context.SaveChangesAsync();
            return shedule.TeacherSheduleId;
        }
    }
}
