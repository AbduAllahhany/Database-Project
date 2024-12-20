using System.ComponentModel.DataAnnotations;
using hospital.management.system.BLL.Filters;
using hospital.management.system.DAL;

namespace hospital.management.system.BLL.Models.Staff;

public class StaffModel
{
    public Guid Id { get; set; }
    [Display(Name = "First Name")] public string FirstName { get; set; }
    [Display(Name = "Last Name")] public string LastName { get; set; }

    [ValidRole(SD.Nurse, SD.Intern, SD.OfficeBoy)]
    public string? Role { get; set; }

    [Display(Name = "StartSchedule")] public TimeOnly StartSchedule { get; set; }
    [Display(Name = "EndSchedule")] public TimeOnly EndSchedule { get; set; }
    [Display(Name = "Day of Work")] public byte DayOfWork { get; set; }
    [Required] public Guid UserId { get; set; }
}