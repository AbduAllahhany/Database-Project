using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hospital.management.system.Models.Entities;

[Table("Bill")]
public partial class Bill
{
    [Key]
    public Guid Id { get; set; }

    [Column("date")]
    public DateTimeOffset? Date { get; set; }

    [Column("totalAmount", TypeName = "money")]
    public decimal TotalAmount { get; set; }

    [Column("paidAmount", TypeName = "money")]
    public decimal PaidAmount { get; set; }

    [Column("remaining", TypeName = "money")]
    public decimal? Remaining { get; set; }

    [Column("patientId")]
    public Guid? PatientId { get; set; }

    [ForeignKey("PatientId")]
    [InverseProperty("Bills")]
    public virtual Patient? Patient { get; set; }
}
