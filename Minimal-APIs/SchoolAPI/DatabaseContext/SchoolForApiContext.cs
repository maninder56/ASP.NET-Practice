﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DatabaseContext;

public partial class SchoolForApiContext : DbContext
{
    public SchoolForApiContext() { }

    public SchoolForApiContext(DbContextOptions<SchoolForApiContext> options)
        : base(options) { }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<OfficeAssignment> OfficeAssignments { get; set; }

    public virtual DbSet<OnlineCourse> OnlineCourses { get; set; }

    public virtual DbSet<OnsiteCourse> OnsiteCourses { get; set; }

    public virtual DbSet<Person> People { get; set; }

    public virtual DbSet<StudentGrade> StudentGrades { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.CourseId).HasName("PK_School.Course");

            entity.ToTable("Course", "school");

            entity.Property(e => e.CourseId)
                .ValueGeneratedNever()
                .HasColumnName("CourseID");
            entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");
            entity.Property(e => e.Title).HasMaxLength(100);

            entity.HasOne(d => d.Department).WithMany(p => p.Courses)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Course_Department");

            entity.HasMany(d => d.People).WithMany(p => p.Courses)
                .UsingEntity<Dictionary<string, object>>(
                    "CourseInstructor",
                    r => r.HasOne<Person>().WithMany()
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_CourseInstructor_Person"),
                    l => l.HasOne<Course>().WithMany()
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_CourseInstructor_Course"),
                    j =>
                    {
                        j.HasKey("CourseId", "PersonId");
                        j.ToTable("CourseInstructor", "school");
                        j.IndexerProperty<int>("CourseId").HasColumnName("CourseID");
                        j.IndexerProperty<int>("PersonId").HasColumnName("PersonID");
                    });
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.ToTable("Department", "school");

            entity.Property(e => e.DepartmentId)
                .ValueGeneratedNever()
                .HasColumnName("DepartmentID");
            entity.Property(e => e.Budget).HasColumnType("money");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.StartDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<OfficeAssignment>(entity =>
        {
            entity.HasKey(e => e.InstructorId);

            entity.ToTable("OfficeAssignment", "school");

            entity.Property(e => e.InstructorId)
                .ValueGeneratedNever()
                .HasColumnName("InstructorID");
            entity.Property(e => e.Location).HasMaxLength(50);
            entity.Property(e => e.Timestamp)
                .IsRowVersion()
                .IsConcurrencyToken();

            entity.HasOne(d => d.Instructor).WithOne(p => p.OfficeAssignment)
                .HasForeignKey<OfficeAssignment>(d => d.InstructorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OfficeAssignment_Person");
        });

        modelBuilder.Entity<OnlineCourse>(entity =>
        {
            entity.HasKey(e => e.CourseId);

            entity.ToTable("OnlineCourse", "school");

            entity.Property(e => e.CourseId)
                .ValueGeneratedNever()
                .HasColumnName("CourseID");
            entity.Property(e => e.Url)
                .HasMaxLength(100)
                .HasColumnName("URL");

            entity.HasOne(d => d.Course).WithOne(p => p.OnlineCourse)
                .HasForeignKey<OnlineCourse>(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OnlineCourse_Course");
        });

        modelBuilder.Entity<OnsiteCourse>(entity =>
        {
            entity.HasKey(e => e.CourseId);

            entity.ToTable("OnsiteCourse", "school");

            entity.Property(e => e.CourseId)
                .ValueGeneratedNever()
                .HasColumnName("CourseID");
            entity.Property(e => e.Days).HasMaxLength(50);
            entity.Property(e => e.Location).HasMaxLength(50);
            entity.Property(e => e.Time).HasColumnType("smalldatetime");

            entity.HasOne(d => d.Course).WithOne(p => p.OnsiteCourse)
                .HasForeignKey<OnsiteCourse>(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OnsiteCourse_Course");
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.PersonId).HasName("PK_School.Student");

            entity.ToTable("Person", "school");

            entity.Property(e => e.PersonId).HasColumnName("PersonID");
            entity.Property(e => e.Discriminator).HasMaxLength(50);
            entity.Property(e => e.EnrollmentDate).HasColumnType("datetime");
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.HireDate).HasColumnType("datetime");
            entity.Property(e => e.LastName).HasMaxLength(50);
        });

        modelBuilder.Entity<StudentGrade>(entity =>
        {
            entity.HasKey(e => e.EnrollmentId);

            entity.ToTable("StudentGrade", "school");

            entity.Property(e => e.EnrollmentId).HasColumnName("EnrollmentID");
            entity.Property(e => e.CourseId).HasColumnName("CourseID");
            entity.Property(e => e.Grade).HasColumnType("decimal(3, 2)");
            entity.Property(e => e.StudentId).HasColumnName("StudentID");

            entity.HasOne(d => d.Course).WithMany(p => p.StudentGrades)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StudentGrade_Course");

            entity.HasOne(d => d.Student).WithMany(p => p.StudentGrades)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StudentGrade_Student");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
