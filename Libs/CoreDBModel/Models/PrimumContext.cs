using CoreDBModel.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Threading;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CoreDBModel.Models;

public partial class PrimumContext : DbContext
{
    public PrimumContext()
    {
    }

    public PrimumContext(DbContextOptions<PrimumContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Abonement> Abonements { get; set; }

    public virtual DbSet<AbonementShedule> AbonementShedules { get; set; }

    public virtual DbSet<AdminProfile> AdminProfiles { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Lesson> Lessons { get; set; }

    public virtual DbSet<StudentProfile> StudentProfiles { get; set; }

    public virtual DbSet<TeacherProfile> TeacherProfiles { get; set; }

    public virtual DbSet<TeacherShedule> TeacherShedules { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<CourseTheme> CourseThemes {  get; set; }

    public virtual DbSet<AdminPermission> Permissions { get; set; }

    public virtual DbSet<IncidentLog> IncidentLogs { get; set; }

    public virtual DbSet<Promocode> Promocodes { get; set; }

    public virtual DbSet<VerificationToken> VerificationTokens { get; set; }

    public virtual DbSet<StudentGrading> StudentGradings { get; set; }

    public virtual DbSet<CourseRank> CourseRanks { get; set; }

    public virtual DbSet<TeacherRank> TeacherRanks { get; set; }

    public virtual DbSet<StudentRank> StudentRanks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Abonement>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Id).IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.IsReferal)
                .HasDefaultValue(false);

            entity.HasOne(d => d.Course).WithMany(p => p.Abonements)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(d => d.Student).WithMany(p => p.Abonements)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<AbonementShedule>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.HasOne(d => d.Abonement)
                .WithMany(a => a.AbonementShedules)
                .HasForeignKey(d => d.AbonementId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(d => d.TeacherShedule)
                .WithOne(d => d.AbonementShedule)
                .HasForeignKey<AbonementShedule>(d => d.TeacherSheduleId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<AdminProfile>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Id).IsUnique();
            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.Property(e => e.UserId).IsRequired();
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Id).IsUnique();
            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.Property(e => e.FreeLessons).HasDefaultValue(1);
            entity.Property(e => e.MaxLessons).HasDefaultValue(1);

            entity.HasOne(d => d.Teacher).WithMany(p => p.Courses).HasForeignKey(d => d.TeacherId)
                .OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(e => e.CourseTheme).WithMany(p => p.Courses).HasForeignKey(e => e.CourseThemeId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(u => u.Rank)
                .WithMany(p => p.Courses)
                .HasForeignKey(p => p.RankId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Lesson>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Id).IsUnique();
            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.HasOne(d => d.Abonement).WithMany(a => a.Lessons).HasForeignKey(d => d.AbonementId)
                .OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(d => d.Grading).WithOne(a => a.Lesson).HasForeignKey<StudentGrading>(d => d.LessonId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<StudentGrading>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Id).IsUnique();
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<StudentProfile>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Id).IsUnique();
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Cash).HasDefaultValue(0m);

            entity.Property(e => e.UserId).IsRequired();
            entity.HasOne(u => u.Rank)
                .WithMany(p => p.Students)
                .HasForeignKey(p => p.RankId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<TeacherProfile>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Id).IsUnique();
            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.Property(e => e.UserId).IsRequired();
            entity.HasOne(u => u.Rank)
                .WithMany(p => p.Teachers)
                .HasForeignKey(p => p.RankId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<TeacherShedule>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Id).IsUnique();
            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.HasOne(d => d.Teacher)
                .WithMany(p => p.TeacherShedules)
                .HasForeignKey(d => d.TeacherId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<CourseTheme>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Id).IsUnique();
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<AdminPermission>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Id).IsUnique();
            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.HasOne(e => e.AdminProfile)
                .WithMany(e => e.Permissions)
                .HasForeignKey(e => e.AdminProfileId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(e => e.PromoterAdminProfile)
                .WithMany(e => e.GivenPermissions)
                .HasForeignKey(e => e.PromoterAdminProfileId)
                .OnDelete(DeleteBehavior.NoAction);
        });

        modelBuilder.Entity<IncidentLog>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Id).IsUnique();
            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.HasOne(e => e.AdminProfile)
                .WithMany(e => e.IncidentLogs)
                .HasForeignKey(e => e.AdminProfileId)
                .OnDelete(DeleteBehavior.NoAction);
        });

        modelBuilder.Entity<Promocode>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Id).IsUnique();
            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.HasOne(e => e.Student)
                .WithMany(e => e.Promocodes)
                .HasForeignKey(e => e.StudentId)
                .OnDelete(DeleteBehavior.NoAction);
        });

        modelBuilder.Entity<VerificationToken>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Id).IsUnique();
            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.HasOne(e => e.User)
                .WithMany(e => e.VerificationTokens)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Id).IsUnique();
            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.HasOne(u => u.TeacherProfile)
                .WithOne(p => p.User)
                .HasForeignKey<TeacherProfile>(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(u => u.AdminProfile)
                .WithOne(p => p.User)
                .HasForeignKey<AdminProfile>(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(u => u.StudentProfile)
                .WithOne(p => p.User)
                .HasForeignKey<StudentProfile>(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasQueryFilter(AvailabilityExpressions.IsUserAvailable);
        });

        modelBuilder.Entity<CourseRank>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Id).IsUnique();
            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.HasData(
                new CourseRank
                {
                    Id = -1,
                    Level = 1,
                    Rank = "Новый",
                    RequiredExperience = 0
                });
        });

        modelBuilder.Entity<TeacherRank>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Id).IsUnique();
            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.HasData(
                new TeacherRank
                {
                    Id = -1,
                    Level = 1,
                    Rank = "Начинающий наставник",
                    RequiredExperience = 0,
                    EarningMultiplier = 0.3f
                });
        });

        modelBuilder.Entity<StudentRank>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Id).IsUnique();
            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.HasData(
                new StudentRank
                {
                    Id = -1,
                    Level = 1,
                    Rank = "Новенький",
                    RequiredExperience = 0,
                    CoinDiscount = 0f
                });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    public override int SaveChanges()
    {
        SavingProcedures();
        return base.SaveChanges();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        SavingProcedures();
        return await base.SaveChangesAsync(cancellationToken);
    }

    private void SavingProcedures()
    {
        UpdateTeacherRanks();
        UpdateCourseRanks();
        UpdateStudentRanks();
        UpdateAbonementRatings();
        UpdateStudentRatings();

        UpdateTimestamps();
    }

    private void UpdateTimestamps()
    {
        var utcNow = DateTime.UtcNow;

        var addedEntries = ChangeTracker.Entries<BaseEntity>()
            .Where(e => e.State == EntityState.Added);

        foreach (var entry in addedEntries)
        {
            entry.Entity.CreatedAt = utcNow;
        }
    }

    private void UpdateTeacherRanks()
    {
        var teacherEntries = ChangeTracker.Entries<TeacherProfile>()
            .Where(e => e.State == EntityState.Added || e.Property(p => p.Experience).IsModified);

        foreach (var entry in teacherEntries)
        {
            var profile = entry.Entity;

            var suitableRank = TeacherRanks
                .OrderByDescending(r => r.RequiredExperience)
                .First(r => r.RequiredExperience <= profile.Experience);
            profile.Rank = suitableRank;
            profile.RankId = suitableRank.Id;
        }
    }

    private void UpdateStudentRanks()
    {
        var teacherEntries = ChangeTracker.Entries<StudentProfile>()
            .Where(e => e.State == EntityState.Added || e.Property(p => p.Experience).IsModified);

        foreach (var entry in teacherEntries)
        {
            var profile = entry.Entity;

            var suitableRank = StudentRanks
                .OrderByDescending(r => r.RequiredExperience)
                .First(r => r.RequiredExperience <= profile.Experience);
            profile.Rank = suitableRank;
            profile.RankId = suitableRank.Id;
        }
    }

    private void UpdateCourseRanks()
    {
        var teacherEntries = ChangeTracker.Entries<Course>()
            .Where(e => e.State == EntityState.Added || e.Property(p => p.Experience).IsModified);

        foreach (var entry in teacherEntries)
        {
            var profile = entry.Entity;

            var suitableRank = CourseRanks
                .OrderByDescending(r => r.RequiredExperience)
                .First(r => r.RequiredExperience <= profile.Experience);
            profile.Rank = suitableRank;
            profile.RankId = suitableRank.Id;
        }
    }

    private void UpdateAbonementRatings()
    {
        var gradeEntries = ChangeTracker.Entries<StudentGrading>()
            .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

        foreach (var entry in gradeEntries)
        {
            var abonement = Abonements
                .Include(x => x.Lessons)
                .First(x => x.Lessons.Any(y => y.Id == entry.Entity.LessonId));

            IEnumerable<StudentGrading> grades = abonement.Lessons.Where(x => x.Grading != null).Select(x => x.Grading!).ToArray();
            if (!grades.Any()) 
            {
                abonement.Rating = null;
            }
            else
            {
                abonement.Rating = grades.Select(x => x.GetFinalGrade()).Average();
            }
        }
    }

    private void UpdateStudentRatings()
    {
        var gradeEntries = ChangeTracker.Entries<Abonement>()
            .Where(e => e.Property(p => p.Rating).IsModified);

        foreach (var entry in gradeEntries)
        {
            var student = StudentProfiles
                .Include(x => x.Abonements)
                .First(x => x.Abonements.Any(y => y.Id == entry.Entity.Id));

            IEnumerable<float> abonGrades = student.Abonements.Where(x => x.Rating != null).Select(x => x.Rating!.Value).ToArray();
            if (!abonGrades.Any())
            {
                student.Rating = null;
            }
            else
            {
                student.Rating = abonGrades.Average();
            }
        }
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
