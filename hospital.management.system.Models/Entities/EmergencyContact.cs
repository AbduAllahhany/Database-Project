using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hospital.management.system.Models.Entities;

[Table("Emergency_Contact")]
[Index("Phone", Name = "UQ__Emergenc__B43B145F0626F054", IsUnique = true)]
public partial class EmergencyContact
{
    [Key]
    public Guid Id { get; set; }

    [Column("name")]
    [StringLength(20)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [Column("phone")]
    [StringLength(11)]
    [Unicode(false)]
    public string? Phone { get; set; }

    [Column("relationship")]
    [StringLength(20)]
    [Unicode(false)]
    public string Relationship { get; set; } = null!;

    [Column("patientId")]
    public Guid PatientId { get; set; }

    [ForeignKey("PatientId")]
    [InverseProperty("EmergencyContacts")]
    public virtual Patient Patient { get; set; } = null!;
}
