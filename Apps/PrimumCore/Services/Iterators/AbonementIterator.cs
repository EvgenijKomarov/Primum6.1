using CoreConnection.DTOs;
using CoreConnection.Entities;
using CoreDBModel.Constants;
using CoreDBModel.Extensions;
using CoreDBModel.Models;
using CoreDBModel.Models.Enums;
using Microsoft.EntityFrameworkCore;
using PrimumCore.Exceptions;
using PrimumCore.Extentions;
using PublishServiceConnection;
using PublishServiceConnection.Events;
using System.Linq;
using System.Linq.Expressions;

namespace PrimumCore.Services.Iterators
{
    public class AbonementIterator(PrimumContext context, PublisherService publisher)
    {
        private IQueryable<Abonement> Abonements(bool isOnlyAlive, Expression<Func<Abonement, bool>>? predicate) => context
            .Set<Abonement>()
            .WhereIf(isOnlyAlive, AvailabilityExpressions.IsAbonementAlive)
            .WhereIf(predicate is not null, predicate!)
            .Include(x => x.Course)
            .ThenInclude(x => x.Teacher)
            .ThenInclude(x => x.User)
            .Include(x => x.Student)
            .ThenInclude(x => x.User)
            .Include(x => x.Lessons)
            .Include(x => x.Course)
            .ThenInclude(x => x.CourseTheme);

        public async Task<PageResult<AbonementDto>> GetTeacherAbonements(int teacherId, int _page, int _pageSize)
        {
            return await Abonements(true, x => x.Course.Teacher.User.Id == teacherId).ToDto().ToPageResult(_page, _pageSize);
        }

        public async Task<AbonementDto> GetTeacherAbonement(int teacherId, int abonementId)
        {
            return await Abonements(true, x => x.Course.Teacher.User.Id == teacherId).ToDto().One(x => x.AbonementId == abonementId);
        }

        public async Task<PageResult<AbonementDto>> GetStudentAbonements(int studentId, int _page, int _pageSize)
        {
            return await Abonements(false, x => x.Student.User.Id == studentId).ToDto().ToPageResult(_page, _pageSize);
        }

        public async Task<AbonementDto> GetStudentAbonement(int studentId, int abonementId)
        {
            return await Abonements(false, x => x.Student.User.Id == studentId).ToDto().One(x => x.AbonementId == abonementId);
        }

        public async Task<int> AbonementChangeStatus(int studentId, int abonementId, AbonementStatus status)
        {
            var abonement = await Abonements(false, x => x.Student.User.Id == studentId)
                .One(x => x.AbonementId == abonementId);

            abonement.AbonementStatus = status;
            if (status == AbonementStatus.Deleted) { abonement.AbonementShedules.Clear(); }
            await context.SaveChangesAsync();
            await publisher.Push(new AbonementChangeStatusEvent
            {
                StudentName = abonement.Student.User.DisplayName,
                StudentUserId = abonement.Student.User.Id,
                TeacherName = abonement.Course.Teacher.User.DisplayName,
                TeacherUserId = abonement.Course.Teacher.User.Id,
                CourseName = abonement.Course.Name,
                AbonementId = abonement.AbonementId,
                AbonementStatus = abonement.AbonementStatus.ToString()
            });
            return abonement.AbonementId;
        }
    }
}
