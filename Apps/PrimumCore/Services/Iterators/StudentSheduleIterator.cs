using CoreConnection.DTOs;
using CoreConnection.Entities;
using CoreDBModel.Models;
using Microsoft.EntityFrameworkCore;
using PrimumCore.Exceptions;
using PrimumCore.Extentions;
using PublishServiceConnection;
using PublishServiceConnection.Events;
using System.Linq.Expressions;

namespace PrimumCore.Services.Iterators
{
    public class StudentSheduleIterator(PrimumContext context, PublisherService publisher)
    {
        private IQueryable<AbonementShedule> AbonementShedules(Expression<Func<AbonementShedule, bool>>? predicate) => context
            .Set<AbonementShedule>()
            .WhereIf(predicate is not null, predicate!)
            .Include(x => x.TeacherShedule)
            .ThenInclude(x => x.Teacher)
            .ThenInclude(x => x.User)
            .Include(x => x.Abonement)
            .ThenInclude(x => x.Course)
            .ThenInclude(x => x.Teacher)
            .ThenInclude(x => x.User)
            .Include(x => x.Abonement)
            .ThenInclude(x => x.Student)
            .ThenInclude(x => x.User);

        public async Task<PageResult<StudentSheduleDto>> GetAbonementShedules(int abonementId, int _page, int _pageSize)
        {
            return await AbonementShedules(x => x.Abonement.Id == abonementId).ToDto().ToPageResult(_page, _pageSize);
        }

        public async Task<PageResult<StudentSheduleDto>> GetStudentShedules(int studentId, int _page, int _pageSize)
        {
            return await AbonementShedules(x => x.Abonement.Student.User.Id == studentId).ToDto().ToPageResult(_page, _pageSize);
        }

        public async Task<StudentSheduleDto> GetStudentShedule(int studentId, int sheduleId)
        {
            return await AbonementShedules(x => x.Abonement.Student.User.Id == studentId).ToDto().One(x => x.AbonementSheduleId == sheduleId);
        }

        public async Task<int> DeleteStudentShedule(int studentId, int abonementSheduleId)
        {
            var abonementShedule = await AbonementShedules(null)
                .One(x => x.Id == abonementSheduleId);
            if (abonementShedule.Abonement.Student.User.Id != studentId) { throw new BusinessLogicException("Only owner can delete shedule"); }

            var notification = new DeleteAbonementSheduleEvent
            {
                StudentName = abonementShedule.Abonement.Student.User.DisplayName,
                StudentUserId = abonementShedule.Abonement.Student.User.Id,
                TeacherName = abonementShedule.TeacherShedule.Teacher.User.DisplayName,
                TeacherUserId = abonementShedule.TeacherShedule.Teacher.User.Id,
                CourseName = abonementShedule.Abonement.Course.Name,
                AbonementId = abonementShedule.Abonement.Id,
                AbonementSheduleId = abonementShedule.Id,
                DayOfWeek = abonementShedule.TeacherShedule.DayOfWeek.ToString(),
                Time = abonementShedule.TeacherShedule.Time
            };
            abonementShedule.Abonement.AbonementShedules.Remove(abonementShedule);
            await context.SaveChangesAsync();

            await publisher.Push(notification);
            return abonementShedule.Id;
        }
    }
}
