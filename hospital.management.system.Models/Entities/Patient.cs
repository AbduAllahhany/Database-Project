using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hospital.management.system.Models.Entities;

[Table("Patient")]
public partial class Patient : BaseEntity
{
    [Key]
    public Guid Id { get; set; }

    [Column("firstName")]
    [StringLength(20)]
    [Unicode(false)]
    public string FirstName { get; set; } = null!;

    [Column("lastName")]
    [StringLength(20)]
    [Unicode(false)]
    public string LastName { get; set; } = null!;

    [Column("dateOfBirth")]
    public DateOnly DateOfBirth { get; set; }

    [Column("address")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Address { get; set; }

    [InverseProperty("Patient")]
    public virtual ICollection<Admission> Admissions { get; set; } = new List<Admission>();

    [InverseProperty("Patient")]
    public virtual ICollection<Bill> Bills { get; set; } = new List<Bill>();

    [InverseProperty("Patient")]
    public virtual ICollection<EmergencyContact> EmergencyContacts { get; set; } = new List<EmergencyContact>();

    [InverseProperty("Patient")]
    public virtual ICollection<Insurance> Insurances { get; set; } = new List<Insurance>();

    [InverseProperty("Patient")]
    public virtual ICollection<MedicalRecord> MedicalRecords { get; set; } = new List<MedicalRecord>();

    [InverseProperty("Patient")]
    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    [InverseProperty("Patient")]
    public virtual ICollection<PatientPhone> PatientPhones { get; set; } = new List<PatientPhone>();

    [InverseProperty("Patient")]
    public virtual ICollection<Visit> Visits { get; set; } = new List<Visit>();
}
