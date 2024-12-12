using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hospital.management.system.Models.Entities;

[Table("Patient_Doctor_Appointment")]
public partial class Appointment
{
    [Key]
    public Guid Id { get; set; }

    [Column("patientId")]
    public Guid PatientId { get; set; }

    [Column("doctorId")]
    public Guid DoctorId { get; set; }

    [Column("status")]
    [StringLength(10)]
    [Unicode(false)]
    public string Status { get; set; } = null!;

    [Column("reason")]
    [StringLength(200)]
    [Unicode(false)]
    public string? Reason { get; set; }

    [Column("date")]
    public DateOnly Date { get; set; }

    [ForeignKey("DoctorId")]
    [InverseProperty("Appointments")]
    public virtual Doctor Doctor { get; set; } = null!;

    [ForeignKey("PatientId")]
    [InverseProperty("Appointments")]
    public virtual Patient Patient { get; set; } = null!;
}
