using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hospital.management.system.Models.Entities;

[Table("Visit")]
public partial class Visit
{
    [Key]
    public Guid Id { get; set; }

    [Column("notes")]
    [StringLength(200)]
    [Unicode(false)]
    public string? Notes { get; set; }

    [Column("patientId")]
    public Guid PatientId { get; set; }

    [Column("reason")]
    [StringLength(200)]
    [Unicode(false)]
    public string Reason { get; set; } = null!;

    [Column("date")]
    public DateOnly Date { get; set; }

    [ForeignKey("PatientId")]
    [InverseProperty("Visits")]
    public virtual Patient Patient { get; set; } = null!;
}
