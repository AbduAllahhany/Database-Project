using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hospital.management.system.Models.Entities;

[PrimaryKey("PatientId", "Number")]
[Table("Patient_Phone")]
[Index("Number", Name = "UQ__Patient___FD291E414AD144B4", IsUnique = true)]
public partial class PatientPhone
{
    [Key]
    [Column("patientId")]
    public Guid PatientId { get; set; }

    [Key]
    [Column("number")]
    [StringLength(11)]
    [Unicode(false)]
    public string Number { get; set; } = null!;

    [ForeignKey("PatientId")]
    [InverseProperty("PatientPhones")]
    public virtual Patient Patient { get; set; } = null!;
}
