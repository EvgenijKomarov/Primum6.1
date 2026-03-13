using CoreConnection.DTOs;
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

        private IQueryable<AbonementDto> ToDto(IQueryable<Abonement> queryable) => queryable
            .Select(a => new AbonementDto
            {
                StudentId = a.Student.User.Id,
                StudentDisplayName = a.Student.User.DisplayName,
                TeacherId = a.Course.Teacher.User.Id,
                TeacherDisplayName = a.Course.Teacher.User.DisplayName,
                AbonementId = a.AbonementId,
                CourseName = a.Course.Name,
                CourseId = a.Course.CourseId,
                CourseThemeName = a.Course.CourseTheme.ThemeName,
                CourseThemeId = a.Course.CourseTheme.CourseThemeId,
                PricePerLesson = a.PricePerLesson,
                AbonementStatus = a.AbonementStatus
            });

        public async Task<IEnumerable<AbonementDto>> GetTeacherAbonements(int teacherId)
        {
            return await ToDto(
                    Abonements(true, x => x.Course.Teacher.User.Id == teacherId)
                ).ToArrayAsync();
        }

        public async Task<AbonementDto> GetTeacherAbonement(int teacherId, int abonementId)
        {
            return await ToDto(
                    Abonements(true, x => x.Course.Teacher.User.Id == teacherId)
                    .Where(x => x.Course.Teacher.User.Id == teacherId)
                ).FirstOrDefaultAsync(x => x.AbonementId == abonementId) ?? throw new NotFoundException("Abonement");
        }

        public async Task<IEnumerable<AbonementDto>> GetStudentAbonements(int studentId)
        {
            return await ToDto(
                    Abonements(false, x => x.Student.User.Id == studentId)
                ).ToArrayAsync();
        }

        public async Task<AbonementDto> GetStudentAbonement(int studentId, int abonementId)
        {
            return await ToDto(
                    Abonements(false, x => x.Student.User.Id == studentId)
                ).FirstOrDefaultAsync(x => x.AbonementId == abonementId) ?? throw new NotFoundException("Abonement");
        }

        public async Task<int> AbonementChangeStatus(int studentId, int abonementId, AbonementStatus status)
        {
            var abonement = await Abonements(false, x => x.Student.User.Id == studentId)
                .FirstOrDefaultAsync(x => x.AbonementId == abonementId) ?? throw new NotFoundException("Abonement");

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
