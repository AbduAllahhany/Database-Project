using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using hospital.management.system.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace hospital.management.system.Models.Entities;

[Table("Doctor")]
public partial class Doctor : BaseEntity
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

    [Column("specialization")]
    [StringLength(50)]
    [Unicode(false)]
    public string Specialization { get; set; } = null!;

    [Column("departmentId")]
    public Guid? DepartmentId { get; set; }

    [Column("salary", TypeName = "money")]
    public decimal? Salary { get; set; }

    [Column("workingHours")]
    public int WorkingHours { get; set; }

    [Column("startSchedule")]
    public TimeOnly StartSchedule { get; set; }

    [Column("endSchedule")]
    public TimeOnly EndSchedule { get; set; }

    [ForeignKey("DepartmentId")]
    [InverseProperty("Doctors")]
    public virtual Department? Department { get; set; }

    [InverseProperty("Doctor")]
    public virtual ICollection<MedicalRecord> MedicalRecords { get; set; } = new List<MedicalRecord>();

    [InverseProperty("Doctor")]
    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
