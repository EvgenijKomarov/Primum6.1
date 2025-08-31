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

    DatabaseFacade IPrimumContext.Database => base.Database;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Abonement>(entity =>
        {
            entity.HasIndex(e => e.AbonementId, "IX_Abonements_AbonementId").IsUnique();

            entity.Property(e => e.AbonementId)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("0");

            entity.HasKey(e => e.AbonementId).HasAnnotation("Sqlite:Autoincrement", true);

            entity.HasOne(d => d.Course).WithMany(p => p.Abonements)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(d => d.Student).WithMany(p => p.Abonements).HasForeignKey(d => d.StudentId);
        });

        modelBuilder.Entity<AbonementShedule>(entity =>
        {
            entity.HasKey(e => e.AbonementSheduleId).HasAnnotation("Sqlite:Autoincrement", true);

            entity.Property(e => e.AbonementSheduleId)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("0");

            entity.Property(e => e.LastIteration).HasColumnType("DATETIME");

            entity.HasOne(d => d.Abonement).WithMany(a => a.AbonementShedules).HasForeignKey(d => d.AbonementSheduleId);
        });

        modelBuilder.Entity<AdminProfile>(entity =>
        {
            entity.HasKey(e => e.AdminId).HasAnnotation("Sqlite:Autoincrement", true);

            entity.Property(e => e.AdminId)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("0");

            entity.HasIndex(e => e.AdminId, "IX_AdminProfiles_AdminId").IsUnique();

            entity.Property(e => e.Permissions).HasDefaultValueSql("0");
            entity.Property(e => e.UserId).IsRequired();
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.CourseId)
                .HasAnnotation("Sqlite:Autoincrement", true);

            entity.Property(e => e.CourseId)
                  .ValueGeneratedOnAdd()
                  .HasDefaultValueSql("0");

            entity.HasIndex(e => e.CourseId, "IX_Courses_CourseId").IsUnique();

            entity.Property(e => e.FreeLessons).HasDefaultValue(1);
            entity.Property(e => e.MaxLessons).HasDefaultValue(1);

            entity.HasOne(d => d.Teacher).WithMany(p => p.Courses).HasForeignKey(d => d.TeacherId);
        });

        modelBuilder.Entity<Lesson>(entity =>
        {
            entity.HasKey(e => e.LessonId)
                .HasAnnotation("Sqlite:Autoincrement", true);

            entity.Property(e => e.LessonId)
                  .ValueGeneratedOnAdd()
                  .HasDefaultValueSql("0");

            entity.Property(e => e.DateTime).HasColumnType("DATETIME");

            entity.HasOne(d => d.Abonement).WithMany(a =>a.Lessons).HasForeignKey(d => d.AbonementId);
        });

        modelBuilder.Entity<StudentProfile>(entity =>
        {
            entity.HasKey(e => e.StudentId)
                .HasAnnotation("Sqlite:Autoincrement", true);

            entity.Property(e => e.StudentId)
                  .ValueGeneratedOnAdd()
                  .HasDefaultValueSql("0");

            entity.HasIndex(e => e.StudentId, "IX_StudentProfiles_StudentId").IsUnique();
            entity.Property(e => e.UserId).IsRequired();
        });

        modelBuilder.Entity<TeacherProfile>(entity =>
        {
            entity.HasKey(e => e.TeacherId)
                .HasAnnotation("Sqlite:Autoincrement", true);

            entity.Property(e => e.TeacherId)
                  .ValueGeneratedOnAdd()
                  .HasDefaultValueSql("0");

            entity.HasIndex(e => e.TeacherId, "IX_TeacherProfiles_TeacherId").IsUnique();
            entity.Property(e => e.UserId).IsRequired();
        });

        modelBuilder.Entity<TeacherShedule>(entity =>
        {
            entity.HasKey(e => e.TeacherSheduleId)
                .HasAnnotation("Sqlite:Autoincrement", true);

            entity.Property(e => e.TeacherSheduleId)
                  .ValueGeneratedOnAdd()
                  .HasDefaultValueSql("0");

            entity.HasIndex(e => e.TeacherSheduleId, "IX_TeacherShedules_TeacherSheduleId").IsUnique();

            entity.HasOne(d => d.Teacher).WithMany(p => p.TeacherShedules).HasForeignKey(d => d.TeacherId);
            entity.HasOne(d => d.AbonementShedule).WithOne(a => a.TeacherShedule).HasForeignKey<AbonementShedule>(d => d.TeacherSheduleId).OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id)
                .HasAnnotation("Sqlite:Autoincrement", true);

            entity.Property(e => e.Id)
                  .ValueGeneratedOnAdd()
                  .HasDefaultValueSql("0");

            entity.HasIndex(e => e.Id, "IX_Users_Id").IsUnique();

            entity.HasOne(u => u.TeacherProfile).WithOne(p => p.User).HasForeignKey<TeacherProfile>(p => p.UserId).OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(u => u.AdminProfile).WithOne(p => p.User).HasForeignKey<AdminProfile>(p => p.UserId).OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(u => u.StudentProfile).WithOne(p => p.User).HasForeignKey<StudentProfile>(p => p.UserId).OnDelete(DeleteBehavior.Cascade);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
