using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HRMSApp.Data.Models
{
    public partial class hrmsContext : DbContext
    {
        public hrmsContext()
        {
        }

        public hrmsContext(DbContextOptions<hrmsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<DeptEmp> DeptEmp { get; set; }
        public virtual DbSet<DeptManager> DeptManager { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<Salary> Salary { get; set; }
        public virtual DbSet<Title> Title { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL("server=localhost;port=3306;user=root;password=Appy170@;database=hrms");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasKey(e => e.DeptNo)
                    .HasName("PRIMARY");

                entity.ToTable("department");

                entity.Property(e => e.DeptNo)
                    .HasColumnName("dept_no")
                    .HasMaxLength(4)
                    .IsFixedLength();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(45)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DeptEmp>(entity =>
            {
                entity.HasKey(e => new { e.DeptNo, e.EmpNo })
                    .HasName("PRIMARY");

                entity.ToTable("dept_emp");

                entity.HasIndex(e => e.DeptNo)
                    .HasName("dept_no");

                entity.HasIndex(e => e.EmpNo)
                    .HasName("emp_no");

                entity.Property(e => e.DeptNo)
                    .HasColumnName("dept_no")
                    .HasMaxLength(4)
                    .IsFixedLength();

                entity.Property(e => e.EmpNo).HasColumnName("emp_no");

                entity.Property(e => e.FromDate)
                    .HasColumnName("from_date")
                    .HasColumnType("date");

                entity.Property(e => e.ToDate)
                    .HasColumnName("to_date")
                    .HasColumnType("date");

                entity.HasOne(d => d.DeptNoNavigation)
                    .WithOne(p => p.DeptEmp)
                    .HasForeignKey<DeptEmp>(d => d.DeptNo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Dept_Emp_ibfk_1");

                entity.HasOne(d => d.EmpNoNavigation)
                    .WithMany(p => p.DeptEmp)
                    .HasForeignKey(d => d.EmpNo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Dept_Emp_ibfk_2");
            });

            modelBuilder.Entity<DeptManager>(entity =>
            {
                entity.HasKey(e => new { e.DeptNo, e.EmpNo })
                    .HasName("PRIMARY");

                entity.ToTable("dept_manager");

                entity.HasIndex(e => e.DeptNo)
                    .HasName("dept_no");

                entity.HasIndex(e => e.EmpNo)
                    .HasName("emp_no");

                entity.Property(e => e.DeptNo)
                    .HasColumnName("dept_no")
                    .HasMaxLength(4)
                    .IsFixedLength();

                entity.Property(e => e.EmpNo).HasColumnName("emp_no");

                entity.Property(e => e.FromDate)
                    .HasColumnName("from_date")
                    .HasColumnType("date");

                entity.Property(e => e.ToDate)
                    .HasColumnName("to_date")
                    .HasColumnType("date");

                entity.HasOne(d => d.DeptNoNavigation)
                    .WithMany(p => p.DeptManager)
                    .HasForeignKey(d => d.DeptNo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("dept_manager_ibfk_1");

                entity.HasOne(d => d.EmpNoNavigation)
                    .WithMany(p => p.DeptManager)
                    .HasForeignKey(d => d.EmpNo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("dept_manager_ibfk_2");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.EmpNo)
                    .HasName("PRIMARY");

                entity.ToTable("employee");

                entity.Property(e => e.EmpNo).HasColumnName("emp_no");

                entity.Property(e => e.Address1)
                    .IsRequired()
                    .HasColumnName("address1")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Address2)
                    .HasColumnName("address2")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.BirthDate)
                    .HasColumnName("birth_date")
                    .HasColumnType("date");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasColumnName("city")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("first_name")
                    .HasMaxLength(24)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasColumnName("gender")
                    .HasColumnType("enum('M','F')");

                entity.Property(e => e.HireDate)
                    .HasColumnName("hire_date")
                    .HasColumnType("date");

                entity.Property(e => e.HomePhone)
                    .HasColumnName("home_phone")
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("last_name")
                    .HasMaxLength(24)
                    .IsUnicode(false);

                entity.Property(e => e.MiddleName)
                    .HasColumnName("middle_name")
                    .HasMaxLength(24)
                    .IsUnicode(false);

                entity.Property(e => e.MobilePhone)
                    .IsRequired()
                    .HasColumnName("mobile_phone")
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.Ssn)
                    .HasColumnName("ssn")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasColumnName("state")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Zipcode)
                    .IsRequired()
                    .HasColumnName("zipcode")
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Salary>(entity =>
            {
                entity.HasKey(e => new { e.EmpNo, e.Salary1 })
                    .HasName("PRIMARY");

                entity.ToTable("salary");

                entity.HasIndex(e => e.EmpNo)
                    .HasName("emp_no");

                entity.Property(e => e.EmpNo).HasColumnName("emp_no");

                entity.Property(e => e.Salary1).HasColumnName("salary");

                entity.Property(e => e.FromDate)
                    .HasColumnName("from_date")
                    .HasColumnType("date");

                entity.Property(e => e.ToDate)
                    .HasColumnName("to_date")
                    .HasColumnType("date");

                entity.HasOne(d => d.EmpNoNavigation)
                    .WithMany(p => p.Salary)
                    .HasForeignKey(d => d.EmpNo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Salary_ibfk_1");
            });

            modelBuilder.Entity<Title>(entity =>
            {
                entity.HasKey(e => new { e.EmpNo, e.Title1 })
                    .HasName("PRIMARY");

                entity.ToTable("title");

                entity.HasIndex(e => e.EmpNo)
                    .HasName("emp_no");

                entity.Property(e => e.EmpNo).HasColumnName("emp_no");

                entity.Property(e => e.Title1)
                    .HasColumnName("title")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FromDate)
                    .HasColumnName("from_date")
                    .HasColumnType("date");

                entity.Property(e => e.ToDate)
                    .HasColumnName("to_date")
                    .HasColumnType("date");

                entity.HasOne(d => d.EmpNoNavigation)
                    .WithMany(p => p.Title)
                    .HasForeignKey(d => d.EmpNo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Title_ibfk_1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
