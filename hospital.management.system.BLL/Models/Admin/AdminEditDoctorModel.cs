using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.JavaScript;
using hospital.management.system.BLL.Filters;

namespace hospital.management.system.BLL.Models.Admin;

public class AdminEditDoctorModel
{
    [Required] public decimal? Salary { get; set; }
    [Required] public Guid? DoctorId { get; set; }

    [Display(Name = "Doctor First Name")]
    [Required]
    public string? FirstName { get; set; }

    [Display(Name = "Doctor Last Name")]
    [Required]
    public string? LastName { get; set; }

    [Required]
    [UniqueEmail(userIdPropertyName: "UserId")]
    public string? Email { get; set; }


    [Required]
    [UniqueUsername("UserId")]
    [Username]
    public string? UserName { get; set; }

    [UniquePhoneNumber("UserId")]
    [EgyptianPhoneNumber]
    public string PhoneNumber { get; set; }

    public Guid UserId { get; set; }
    [Display(Name = "Working Hours")] public byte WorkingHours { get; set; }

    public TimeOnly StartSchedule { get; set; }
    public TimeOnly EndSchedule { get; set; }
}