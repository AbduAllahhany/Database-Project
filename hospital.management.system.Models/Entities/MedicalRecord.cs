using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hospital.management.system.Models.Entities;

[Table("Medical_Record")]
public partial class MedicalRecord
{
    [Key]
    public Guid Id { get; set; }

    [Column("dateOfRecording")]
    public DateOnly DateOfRecording { get; set; }

    [Column("diagnostic")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Diagnostic { get; set; }

    [Column("prescription")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Prescription { get; set; }

    [Column("doctorId")]
    public Guid? DoctorId { get; set; }

    [Column("patientId")]
    public Guid? PatientId { get; set; }

    [ForeignKey("DoctorId")]
    [InverseProperty("MedicalRecords")]
    public virtual Doctor? Doctor { get; set; }

    [ForeignKey("PatientId")]
    [InverseProperty("MedicalRecords")]
    public virtual Patient? Patient { get; set; }
}
