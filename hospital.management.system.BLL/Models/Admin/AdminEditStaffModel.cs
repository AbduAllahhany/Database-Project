using System.ComponentModel.DataAnnotations;

namespace hospital.management.system.BLL.Models.Admin;

public class AdminEditStaffModel
{
    public Guid Id { get; set; }
    [Display(Name = "First Name")] public string FirstName { get; set; }
    [Display(Name = "Last Name")] public string LastName { get; set; }
    public string role { get; set; }
    [Display(Name = "Start Schedule")] public TimeOnly StartSchedule { get; set; }
    public TimeOnly EndSchedule { get; set; }
    [Display(Name = "Phone number")] public string PhoneNumber { get; set; }
    [Display(Name = "Day Of Work")] public byte DayOfWork { get; set; }
}