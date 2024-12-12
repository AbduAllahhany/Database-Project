using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using hospital.management.system.DAL;
using hospital.management.system.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace hospital.management.system.DAL.Persistence;

public partial class ApplicationDbContext : IdentityDbContext<ApplicationUser,IdentityRole<Guid>, Guid>
{

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Admission> Admissions { get; set; }

    public virtual DbSet<Bill> Bills { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Doctor> Doctors { get; set; }

    public virtual DbSet<EmergencyContact> EmergencyContacts { get; set; }

    public virtual DbSet<Insurance> Insurances { get; set; }

    public virtual DbSet<MedicalRecord> MedicalRecords { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    public virtual DbSet<Appointment> PatientDoctorAppointments { get; set; }
    public virtual DbSet<PatientPhone> PatientPhones { get; set; }

    public virtual DbSet<Room> Rooms { get; set; }

    public virtual DbSet<Staff> Staff { get; set; }

    public virtual DbSet<Visit> Visits { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Admission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Admissio__3214EC077F42FC34");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

            entity.HasOne(d => d.Patient).WithMany(p => p.Admissions)
                .HasConstraintName("FK__Admission__patie__70DDC3D8");

            entity.HasOne(d => d.Room).WithMany(p => p.Admissions).HasConstraintName("FK__Admission__roomI__6FE99F9F");
        });

        modelBuilder.Entity<Bill>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Bill__3214EC07FB416F69");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Remaining).HasComputedColumnSql("([TotalAmount]-[PaidAmount])", false);

            entity.HasOne(d => d.Patient).WithMany(p => p.Bills)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Bill__patientId__74AE54BC");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Departme__3214EC078DB5010E");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
        });

        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Doctor__3214EC0708B2C4AD");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Salary).HasDefaultValue(0m);

            entity.HasOne(d => d.Department).WithMany(p => p.Doctors)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Doctor__departme__534D60F1");
            entity.HasOne(p => p.User)
                .WithOne()
                .HasForeignKey<Doctor>(p => p.UserId)
                .OnDelete(DeleteBehavior.NoAction);


        });

        modelBuilder.Entity<EmergencyContact>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Emergenc__3214EC073ADB0451");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Phone).IsFixedLength();

            entity.HasOne(d => d.Patient).WithMany(p => p.EmergencyContacts)
                .HasConstraintName("FK__Emergency__patie__7E37BEF6");
        });

        modelBuilder.Entity<Insurance>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Insuranc__3214EC07BC6E5361");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CoverageMoney).HasDefaultValue(0m);

            entity.HasOne(d => d.Patient).WithMany(p => p.Insurances)
                .HasConstraintName("FK__Insurance__patie__797309D9");
        });

        modelBuilder.Entity<MedicalRecord>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Medical___3214EC07A123C966");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

            entity.HasOne(d => d.Doctor).WithMany(p => p.MedicalRecords)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Medical_R__docto__02084FDA");

            entity.HasOne(d => d.Patient).WithMany(p => p.MedicalRecords)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Medical_R__patie__02FC7413");
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Patient__3214EC073B6DCB54");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.HasOne(p => p.User)
                .WithOne()
                .HasForeignKey<Patient>(p => p.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        });

        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Patient___3214EC0784C5A62C");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Status).HasDefaultValue("Pending");

            entity.HasOne(d => d.Doctor).WithMany(p => p.Appointments)
                .HasConstraintName("FK__Patient_D__docto__5BE2A6F2");

            entity.HasOne(d => d.Patient).WithMany(p => p.Appointments)
                .HasConstraintName("FK__Patient_D__patie__5AEE82B9");
        });

        modelBuilder.Entity<PatientPhone>(entity =>
        {
            entity.HasKey(e => new { e.PatientId, e.Number }).HasName("PK__Patient___BEA29408F7AA0AAA");

            entity.Property(e => e.Number).IsFixedLength();

            entity.HasOne(d => d.Patient).WithMany(p => p.PatientPhones)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Patient_P__patie__571DF1D5");
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Room__3214EC07A1950031");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Type).HasDefaultValue("general");
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Staff__3214EC0799EFF6CD");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Role).HasDefaultValue("intern");

            entity.HasOne(d => d.Dept).WithMany(p => p.Staff)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Staff__dayOfWork__6C190EBB");
            entity.HasOne(p => p.User)
                .WithOne()
                .HasForeignKey<Staff>(p => p.UserId)
                .OnDelete(DeleteBehavior.NoAction);


        });

        modelBuilder.Entity<Visit>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Visit__3214EC07169C3B2E");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

            entity.HasOne(d => d.Patient).WithMany(p => p.Visits).HasConstraintName("FK__Visit__patientId__619B8048");
        });

    }

}
