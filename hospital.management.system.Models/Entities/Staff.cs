using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using hospital.management.system.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace hospital.management.system.Models.Entities;

public partial class Staff:BaseEntity
{
    [Key]
    public Guid Id { get; set; }

    [Column("firstName")]
    [StringLength(20)]
    [Unicode(false)]
    public string FirstName { get; set; } = null!;

    [Column("lastName")]
    [StringLength(20)]
    [Unicode(false)]
    public string LastName { get; set; } = null!;

    [Column("role")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Role { get; set; }

    [Column("deptId")]
    public Guid? DeptId { get; set; }

    [Column("startSchedule")]
    public TimeOnly StartSchedule { get; set; }

    [Column("endSchedule")]
    public TimeOnly EndSchedule { get; set; }

    [Column("dayOfWork")]
    public byte DayOfWork { get; set; }

    [ForeignKey("DeptId")]
    [InverseProperty("Staff")]
    public virtual Department? Dept { get; set; }
}
