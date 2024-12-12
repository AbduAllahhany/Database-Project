using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hospital.management.system.Models.Entities;

[Table("Admission")]
public partial class Admission
{
    [Key]
    public Guid Id { get; set; }

    [Column("startDate")]
    public DateOnly StartDate { get; set; }

    [Column("endDate")]
    public DateOnly EndDate { get; set; }

    [Column("roomId")]
    public Guid RoomId { get; set; }

    [Column("patientId")]
    public Guid PatientId { get; set; }

    [ForeignKey("PatientId")]
    [InverseProperty("Admissions")]
    public virtual Patient Patient { get; set; } = null!;

    [ForeignKey("RoomId")]
    [InverseProperty("Admissions")]
    public virtual Room Room { get; set; } = null!;
}
