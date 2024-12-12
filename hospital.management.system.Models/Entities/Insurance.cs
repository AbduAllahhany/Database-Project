using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hospital.management.system.Models.Entities;

[Table("Insurance")]
public partial class Insurance
{
    [Key]
    public Guid Id { get; set; }

    [Column("providerName")]
    [StringLength(20)]
    [Unicode(false)]
    public string ProviderName { get; set; } = null!;

    [Column("policyNumber")]
    [StringLength(20)]
    [Unicode(false)]
    public string PolicyNumber { get; set; } = null!;

    [Column("coverageMoney", TypeName = "money")]
    public decimal? CoverageMoney { get; set; }

    [Column("expiryDate")]
    public DateOnly ExpiryDate { get; set; }

    [Column("patientId")]
    public Guid PatientId { get; set; }

    [ForeignKey("PatientId")]
    [InverseProperty("Insurances")]
    public virtual Patient Patient { get; set; } = null!;
}
