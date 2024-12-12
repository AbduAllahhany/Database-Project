using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using hospital.management.system.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace hospital.management.system.DAL.Persistence;

public partial class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid> 
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

    public virtual DbSet<Appointment> Appointments { get; set; }

    public virtual DbSet<PatientPhone> PatientPhones { get; set; }

    public virtual DbSet<Room> Rooms { get; set; }

    public virtual DbSet<Staff> Staff { get; set; }

    public virtual DbSet<Visit> Visits { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.LogTo(log => Debug.WriteLine(log));
        //optionsBuilder.UseLazyLoadingProxies(true);
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Admission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Admissio__3214EC07F9F67A67");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

            entity.HasOne(d => d.Patient).WithMany(p => p.Admissions)
                .HasConstraintName("FK__Admission__patie__5CD6CB2B");

            entity.HasOne(d => d.Room).WithMany(p => p.Admissions).HasConstraintName("FK__Admission__roomI__5BE2A6F2");
        });

        modelBuilder.Entity<Bill>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Bill__3214EC07DFB7A6B7");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Remaining).HasComputedColumnSql("([TotalAmount]-[PaidAmount])", false);

            entity.HasOne(d => d.Patient).WithMany(p => p.Bills)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Bill__patientId__60A75C0F");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Departme__3214EC0703F31D13");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
        });

        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Doctor__3214EC07E46BA33D");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

            entity.HasOne(d => d.Department).WithMany(p => p.Doctors)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Doctor__departme__3F466844");

            entity.HasOne(p => p.User)
                .WithOne()
                .HasForeignKey<Doctor>(p => p.UserId)
                .OnDelete(DeleteBehavior.NoAction);


        });

        modelBuilder.Entity<EmergencyContact>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Emergenc__3214EC07D3920CA0");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Phone).IsFixedLength();

            entity.HasOne(d => d.Patient).WithMany(p => p.EmergencyContacts)
                .HasConstraintName("FK__Emergency__patie__6A30C649");
        });

        modelBuilder.Entity<Insurance>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Insuranc__3214EC07B1B44E9D");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CoverageMoney).HasDefaultValue(0m);

            entity.HasOne(d => d.Patient).WithMany(p => p.Insurances)
                .HasConstraintName("FK__Insurance__patie__656C112C");
        });

        modelBuilder.Entity<MedicalRecord>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Medical___3214EC0795637247");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

            entity.HasOne(d => d.Doctor).WithMany(p => p.MedicalRecords)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Medical_R__docto__6E01572D");

            entity.HasOne(d => d.Patient).WithMany(p => p.MedicalRecords)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Medical_R__patie__6EF57B66");
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Patient__3214EC0709422D8D");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

            entity.HasOne(p => p.User)
                .WithOne()
                .HasForeignKey<Patient>(p => p.UserId)
                .OnDelete(DeleteBehavior.NoAction);


        });

        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Patient___3214EC0727C414B6");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Status).HasDefaultValue("Pending");

            entity.HasOne(d => d.Doctor).WithMany(p => p.Appointments)
                .HasConstraintName("FK__Patient_D__docto__47DBAE45");

            entity.HasOne(d => d.Patient).WithMany(p => p.Appointments)
                .HasConstraintName("FK__Patient_D__patie__46E78A0C");
        });

        modelBuilder.Entity<PatientPhone>(entity =>
        {
            entity.HasKey(e => new { e.PatientId, e.Number }).HasName("PK__Patient___BEA2940887B58440");

            entity.Property(e => e.Number).IsFixedLength();

            entity.HasOne(d => d.Patient).WithMany(p => p.PatientPhones)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Patient_P__patie__4316F928");
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Room__3214EC074D99D6C5");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Type).HasDefaultValue("general");
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Staff__3214EC07AE5EC5F8");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Role).HasDefaultValue("intern");

            entity.HasOne(d => d.Dept).WithMany(p => p.Staff)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Staff__dayOfWork__5812160E");
        });

        modelBuilder.Entity<Visit>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Visit__3214EC0727034B10");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

            entity.HasOne(d => d.Patient).WithMany(p => p.Visits).HasConstraintName("FK__Visit__patientId__4D94879B");
        });

    }

}
