using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hospital.management.system.Models.Entities;

[Table("Room")]
public partial class Room
{
    [Key]
    public Guid Id { get; set; }

    [Column("costPerDay", TypeName = "money")]
    public decimal CostPerDay { get; set; }

    [Column("roomNumber")]
    public int RoomNumber { get; set; }

    [Column("type")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Type { get; set; }

    [Column("status")]
    public bool Status { get; set; }

    [InverseProperty("Room")]
    public virtual ICollection<Admission> Admissions { get; set; } = new List<Admission>();
}
