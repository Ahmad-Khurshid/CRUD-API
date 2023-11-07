using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CRUD_APi.Models
{
    public partial class EmpDatabaseContext : DbContext
    {
        public EmpDatabaseContext()
        {
        }

        public EmpDatabaseContext(DbContextOptions<EmpDatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Abc> Abcs { get; set; } = null!;
        public virtual DbSet<AverageSalary> AverageSalaries { get; set; } = null!;
        public virtual DbSet<Department> Departments { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<SourceTable> SourceTables { get; set; } = null!;
        public virtual DbSet<Std> Stds { get; set; } = null!;
        public virtual DbSet<TargetTable> TargetTables { get; set; } = null!;
        public virtual DbSet<Temp1> Temp1s { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            { 
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Abc>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.Name })
                    .HasName("PK__abc__853DFACE7E7A4E0A");

                entity.ToTable("abc");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name).HasColumnName("name");
            });

            modelBuilder.Entity<AverageSalary>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("AverageSAlary");

                entity.Property(e => e.AverageSalary1).HasColumnName("Average Salary");

                entity.Property(e => e.Departmentname).IsUnicode(false);
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("Department");

                entity.Property(e => e.DepartmentId).ValueGeneratedNever();

                entity.Property(e => e.DepartmentName).IsUnicode(false);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.EmpId)
                    .HasName("PK__Employee__AF2DBB991DC0F3EE");

                entity.Property(e => e.EmpName).IsUnicode(false);

                entity.HasOne(d => d.EmpDepartment)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.EmpDepartmentId)
                    .HasConstraintName("FK__Employees__EmpDe__4BAC3F29");
            });

            modelBuilder.Entity<SourceTable>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("source_table");

                entity.Property(e => e.Gender)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("gender");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Std>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Std");

                entity.HasIndex(e => e.Firstid, "IX_STD_Index_CLUSTERED_uNIQUE")
                    .IsUnique()
                    .IsClustered();

                entity.HasIndex(e => e.SecondId, "IX_STD_Index_nonCLUSTERED_uNIQUE")
                    .IsUnique();

                entity.Property(e => e.Firstid).HasColumnName("firstid");

                entity.Property(e => e.SecondId).HasColumnName("secondId");
            });

            modelBuilder.Entity<TargetTable>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("target_table");

                entity.Property(e => e.Gender)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("gender");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Temp1>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("temp1");

                entity.Property(e => e.Departmentname)
                    .IsUnicode(false)
                    .HasColumnName("departmentname");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
