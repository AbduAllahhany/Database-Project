using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using hospital.management.system.BLL.Filters;
using hospital.management.system.DAL;
using hospital.management.system.Models.Enums;

namespace hospital.management.system.BLL.Models.Admin;

public class StaffCreateModel
{
    [Display(Name = "First Name")] public string FirstName { get; set; }
    [Display(Name = "Last Name")] public string LastName { get; set; }
    [UniqueEmail("")]
    [Display(Name = "Email")]
    [EmailAddress]
    public string Email { get; set; }

    [Display(Name = "Phone Number")]
    [EgyptianPhoneNumber]
    [UniquePhoneNumber("")]
    public string PhoneNumber { get; set; }

    [ValidRole(SD.Nurse, SD.Intern, SD.OfficeBoy)]
    public string Role { get; set; }

    [Display(Name = "StartSchedule")] public TimeOnly StartSchedule { get; set; }
    [Display(Name = "EndSchedule")] public TimeOnly EndSchedule { get; set; }
    [Display(Name = "Day of Work")] public byte DayOfWork { get; set; }
    [UniqueUsername("")] public string Username { get; set; }
    [Display(Name = "Dempartment Name")] public string DepartmentName { get; set; }
    public Guid DepartmentId { get; set; }
    public string SSN { get; set; }
}