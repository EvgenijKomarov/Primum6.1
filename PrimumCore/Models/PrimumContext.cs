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

            entity.Property(e => e.Permissions).HasDefaultValue(0);
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
            entity.Property(e => e.LessonId).ValueGeneratedOnAdd();

            entity.Property(e => e.DateTime).HasColumnType("datetime2");

            entity.HasOne(d => d.Abonement).WithMany(a => a.Lessons).HasForeignKey(d => d.AbonementId)
                .OnDelete(DeleteBehavior.Restrict);
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
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
