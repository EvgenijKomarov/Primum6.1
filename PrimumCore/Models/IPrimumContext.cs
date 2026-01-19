using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimumCore.Models
{
    public interface IPrimumContext : IDisposable, IAsyncDisposable
    {
        DbSet<Abonement> Abonements { get; set; }
        DbSet<AbonementShedule> AbonementShedules { get; set; }
        DbSet<AdminProfile> AdminProfiles { get; set; }
        DbSet<Course> Courses { get; set; }
        DbSet<Lesson> Lessons { get; set; }
        DbSet<StudentProfile> StudentProfiles { get; set; }
        DbSet<TeacherProfile> TeacherProfiles { get; set; }
        DbSet<TeacherShedule> TeacherShedules { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<CourseTheme> CourseThemes { get; set; }
        DbSet<AdminPermission> Permissions { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        DatabaseFacade Database { get; }
    }
}
