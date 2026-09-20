using CoreDBModel.Extensions;
using CoreDBModel.Constants;
using CoreDBModel.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CoreDBModel.Services
{
    public class DatabaseIterator(PrimumContext context)
    {
        public async Task SaveChangesAsync() => await context.SaveChangesAsync();

        public async Task AddAsync<TEntity>(TEntity entity) where TEntity : BaseEntity
            => await context.Set<TEntity>().AddAsync(entity);

        public async Task RemoveRangeAsync<TEntity>(IEnumerable<TEntity> entities) where TEntity : BaseEntity
            => context.Set<TEntity>().RemoveRange(entities);

        public async Task RemoveAsync<TEntity>(TEntity entity) where TEntity : BaseEntity
            => context.Set<TEntity>().Remove(entity);

        //TABLES

        public IQueryable<VerificationToken> VerificationTokens(bool isOnlyAlive) => context
            .Set<VerificationToken>()
            .WhereIf(isOnlyAlive, x => !x.IsUsed);

        public IQueryable<Abonement> Abonements(bool isOnlyAlive) => context
            .Set<Abonement>()
            .WhereIf(isOnlyAlive, AvailabilityExpressions.IsAbonementAlive);

        public IQueryable<Course> Courses(bool isOnlyAvailable) => context
            .Set<Course>()
            .WhereIf(isOnlyAvailable, AvailabilityExpressions.IsCourseAvailable)
            .Include(x => x.CourseTheme)
            .Include(x => x.Teacher)
            .ThenInclude(x => x.User);

        public IQueryable<AdminProfile> Admins() => context.Set<AdminProfile>();

        public IQueryable<ConsultationRequest> ConsultationRequests(bool isOnlyUnrevisioned) => context.Set<ConsultationRequest>()
            .WhereIf(isOnlyUnrevisioned, x => x.IsRevisioned == false);

        public IQueryable<CourseTheme> Themes(bool isOnlyAvailable) => context
            .Set<CourseTheme>()
            .WhereIf(isOnlyAvailable, AvailabilityExpressions.IsThemeAvailable);

        public IQueryable<IncidentLog> IncidentLogs(bool OnlyUnrevisioned) => context
            .Set<IncidentLog>()
            .Where(x => x.AdminProfile != null)
            .WhereIf(OnlyUnrevisioned, x => !x.IsRevisioned);

        public IQueryable<Lesson> Lessons() => context
            .Set<Lesson>();

        public IQueryable<Promocode> Promocodes(bool isOnlyAvailable) => context
            .Set<Promocode>()
            .WhereIf(isOnlyAvailable, AvailabilityExpressions.IsPromocodeAvailable);

        public IQueryable<TeacherProfile> Teachers(bool isOnlyAvailable) => context
            .Set<TeacherProfile>()
            .Include(x => x.User)
            .WhereIf(isOnlyAvailable, AvailabilityExpressions.IsTeacherAvailable);

        public IQueryable<StudentProfile> Students() => context
            .Set<StudentProfile>();

        public IQueryable<AbonementShedule> AbonementShedules() => context
            .Set<AbonementShedule>();

        public IQueryable<TeacherShedule> TeacherShedules(bool isOnlyAvailable) => context
            .Set<TeacherShedule>()
            .WhereIf(isOnlyAvailable, AvailabilityExpressions.IsTeacherSheduleAvailable)
            .Include(x => x.Teacher)
            .ThenInclude(x => x.User);

        public IQueryable<User> Users(bool isOnlyAvailable) => context
            .Set<User>()
            .WhereIf(isOnlyAvailable, AvailabilityExpressions.IsUserAvailable)
            .IgnoreQueryFilters();

        public IQueryable<StudentRank> StudentRanks() => context
            .Set<StudentRank>();

        public IQueryable<TeacherRank> TeacherRanks() => context
            .Set<TeacherRank>();

        public IQueryable<CourseRank> CourseRanks() => context
            .Set<CourseRank>();
    }
}
