using CoreDBModel.Constants;
using CoreDBModel.Models;
using Microsoft.EntityFrameworkCore;
using PrimumCore.Extentions;
using System.Linq.Expressions;

namespace PrimumCore.Services.Iterators
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

        public IQueryable<Abonement> Abonements(bool isOnlyAlive) => context
            .Set<Abonement>()
            .WhereIf(isOnlyAlive, AvailabilityExpressions.IsAbonementAlive)
            .Include(x => x.Course)
            .ThenInclude(x => x.Teacher)
            .ThenInclude(x => x.User)
            .Include(x => x.Student)
            .ThenInclude(x => x.User)
            .Include(x => x.Lessons)
            .Include(x => x.Course)
            .ThenInclude(x => x.CourseTheme);

        public IQueryable<Course> Courses(bool isOnlyAvailable) => context
            .Set<Course>()
            .WhereIf(isOnlyAvailable, AvailabilityExpressions.IsCourseAvailable)
            .Include(x => x.CourseTheme)
            .Include(x => x.Teacher)
            .ThenInclude(x => x.User);

        public IQueryable<AdminProfile> Admins() => context.Set<AdminProfile>()
            .Include(x => x.User)
            .Include(x => x.Permissions);

        public IQueryable<CourseTheme> Themes(bool isOnlyAvailable) => context
            .Set<CourseTheme>()
            .WhereIf(isOnlyAvailable, AvailabilityExpressions.IsThemeAvailable);

        public IQueryable<IncidentLog> IncidentLogs(bool OnlyUnrevisioned) => context
            .Set<IncidentLog>()
            .Include(x => x.AdminProfile)
            .ThenInclude(x => x.User)
            .Where(x => x.AdminProfile != null)
            .WhereIf(OnlyUnrevisioned, x => !x.IsRevisioned);

        public IQueryable<Lesson> Lessons() => context
            .Set<Lesson>()
            .Include(x => x.Abonement)
            .ThenInclude(x => x.Course)
            .ThenInclude(x => x.Teacher)
            .ThenInclude(x => x.User)
            .Include(x => x.Abonement)
            .ThenInclude(x => x.Student)
            .ThenInclude(x => x.User)
            .Include(x => x.Abonement)
            .ThenInclude(a => a.Course)
            .Include(x => x.Grading);

        public IQueryable<Promocode> Promocodes(bool isOnlyAvailable) => context
            .Set<Promocode>()
            .WhereIf(isOnlyAvailable, AvailabilityExpressions.IsPromocodeAvailable)
            .Include(x => x.Student)
            .ThenInclude(x => x.User);

        public IQueryable<TeacherProfile> Teachers(bool isOnlyAvailable) => context
            .Set<TeacherProfile>()
            .Include(x => x.User)
            .WhereIf(isOnlyAvailable, AvailabilityExpressions.IsTeacherAvailable);

        public IQueryable<StudentProfile> Students() => context
            .Set<StudentProfile>()
            .Include(x => x.User);

        public IQueryable<AbonementShedule> AbonementShedules() => context
            .Set<AbonementShedule>()
            .Include(x => x.TeacherShedule)
            .Include(x => x.Abonement)
            .ThenInclude(x => x.Course)
            .ThenInclude(x => x.Teacher)
            .ThenInclude(x => x.User);

        public IQueryable<TeacherShedule> TeacherShedules(bool isOnlyAvailable) => context
            .Set<TeacherShedule>()
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

        public IQueryable<User> Users(bool isOnlyAvailable) => context
            .Set<User>()
            .WhereIf(isOnlyAvailable, AvailabilityExpressions.IsUserAvailable)
            .Include(u => u.TeacherProfile)
            .Include(u => u.StudentProfile)
            .Include(u => u.AdminProfile);
    }
}
