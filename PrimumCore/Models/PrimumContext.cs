using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace PrimumCore.Models;

public partial class PrimumContext : DbContext, IPrimumContext
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

    DatabaseFacade IPrimumContext.Database => base.Database;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Abonement>(entity =>
        {
            entity.HasKey(e => e.AbonementId);
            entity.HasIndex(e => e.AbonementId).IsUnique();

            entity.Property(e => e.AbonementId)
                .ValueGeneratedOnAdd();

            entity.HasOne(d => d.Course).WithMany(p => p.Abonements)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(d => d.Student).WithMany(p => p.Abonements)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<AbonementShedule>(entity =>
        {
            entity.HasKey(e => e.AbonementSheduleId);
            entity.Property(e => e.AbonementSheduleId).ValueGeneratedOnAdd();

            entity.Property(e => e.LastIteration).HasColumnType("datetime2");

            entity.HasOne(d => d.Abonement)
                .WithMany(a => a.AbonementShedules)
                .HasForeignKey(d => d.AbonementId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<AdminProfile>(entity =>
        {
            entity.HasKey(e => e.AdminId);
            entity.HasIndex(e => e.AdminId).IsUnique();
            entity.Property(e => e.AdminId).ValueGeneratedOnAdd();

            entity.Property(e => e.UserId).IsRequired();
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.CourseId);
            entity.HasIndex(e => e.CourseId).IsUnique();
            entity.Property(e => e.CourseId).ValueGeneratedOnAdd();

            entity.Property(e => e.FreeLessons).HasDefaultValue(1);
            entity.Property(e => e.MaxLessons).HasDefaultValue(1);

            entity.HasOne(d => d.Teacher).WithMany(p => p.Courses).HasForeignKey(d => d.TeacherId)
                .OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(e => e.CourseTheme).WithMany(p => p.Courses).HasForeignKey(e => e.CourseThemeId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Lesson>(entity =>
        {
            entity.HasKey(e => e.LessonId);
            entity.HasIndex(e => e.LessonId).IsUnique();
            entity.Property(e => e.LessonId).ValueGeneratedOnAdd();

            entity.Property(e => e.DateTime).HasColumnType("datetime2");

            entity.HasOne(d => d.Abonement).WithMany(a => a.Lessons).HasForeignKey(d => d.AbonementId)
                .OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(d => d.Grading).WithOne(a => a.Lesson).HasForeignKey<StudentGrading>(d => d.LessonId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<StudentGrading>(entity =>
        {
            entity.HasKey(e => e.StudentGradingId);
            entity.HasIndex(e => e.StudentGradingId).IsUnique();
            entity.Property(e => e.StudentGradingId).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<StudentProfile>(entity =>
        {
            entity.HasKey(e => e.StudentId);
            entity.HasIndex(e => e.StudentId).IsUnique();
            entity.Property(e => e.StudentId).ValueGeneratedOnAdd();

            entity.Property(e => e.UserId).IsRequired();
        });

        modelBuilder.Entity<TeacherProfile>(entity =>
        {
            entity.HasKey(e => e.TeacherId);
            entity.HasIndex(e => e.TeacherId).IsUnique();
            entity.Property(e => e.TeacherId).ValueGeneratedOnAdd();

            entity.Property(e => e.UserId).IsRequired();
        });

        modelBuilder.Entity<TeacherShedule>(entity =>
        {
            entity.HasKey(e => e.TeacherSheduleId);
            entity.HasIndex(e => e.TeacherSheduleId).IsUnique();
            entity.Property(e => e.TeacherSheduleId).ValueGeneratedOnAdd();

            entity.HasOne(d => d.Teacher)
                .WithMany(p => p.TeacherShedules)
                .HasForeignKey(d => d.TeacherId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(d => d.AbonementShedule)
                .WithOne(a => a.TeacherShedule)
                .HasForeignKey<AbonementShedule>(d => d.TeacherSheduleId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<CourseTheme>(entity =>
        {
            entity.HasKey(e => e.CourseThemeId);
            entity.HasIndex(e => e.CourseThemeId).IsUnique();
            entity.Property(e => e.CourseThemeId).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<AdminPermission>(entity =>
        {
            entity.HasKey(e => e.AdminPermissionId);
            entity.HasIndex(e => e.AdminPermissionId).IsUnique();
            entity.Property(e => e.AdminPermissionId).ValueGeneratedOnAdd();

            entity.Property(e => e.PromotionDate).HasColumnType("datetime2");

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
            entity.HasKey(e => e.LogId);
            entity.HasIndex(e => e.LogId).IsUnique();
            entity.Property(e => e.LogId).ValueGeneratedOnAdd();

            entity.Property(e => e.DecisionDate).HasColumnType("datetime2");

            entity.HasOne(e => e.AdminProfile)
                .WithMany(e => e.IncidentLogs)
                .HasForeignKey(e => e.AdminProfileId)
                .OnDelete(DeleteBehavior.NoAction);
        });

        modelBuilder.Entity<Promocode>(entity =>
        {
            entity.HasKey(e => e.PromocodeId);
            entity.HasIndex(e => e.PromocodeId).IsUnique();
            entity.Property(e => e.PromocodeId).ValueGeneratedOnAdd();

            entity.HasOne(e => e.Student)
                .WithMany(e => e.Promocodes)
                .HasForeignKey(e => e.StudentId)
                .OnDelete(DeleteBehavior.NoAction);
        });

        modelBuilder.Entity<VerificationToken>(entity =>
        {
            entity.HasKey(e => e.TokenId);
            entity.HasIndex(e => e.TokenId).IsUnique();
            entity.Property(e => e.TokenId).ValueGeneratedOnAdd();

            entity.HasOne(e => e.User)
                .WithMany(e => e.VerificationTokens)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Id).IsUnique();
            entity.Property(e => e.Cash).HasDefaultValue(0);
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

            entity.HasQueryFilter(e => !e.IsBanned && e.IsMailChecked);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
