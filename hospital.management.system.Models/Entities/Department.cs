using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hospital.management.system.Models.Entities;

[Table("Department")]
public partial class Department
{
    [Key]
    public Guid Id { get; set; }

    [Column("description")]
    [StringLength(200)]
    [Unicode(false)]
    public string Description { get; set; } = null!;

    [Column("name")]
    [StringLength(20)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [InverseProperty("Department")]
    public virtual ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();

    [InverseProperty("Dept")]
    public virtual ICollection<Staff> Staff { get; set; } = new List<Staff>();
}
