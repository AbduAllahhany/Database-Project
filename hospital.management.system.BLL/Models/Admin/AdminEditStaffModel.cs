using System.ComponentModel.DataAnnotations;
using hospital.management.system.BLL.Filters;

namespace hospital.management.system.BLL.Models.Admin;

public class AdminEditStaffModel
{
    [Required] public Guid? StaffId { get; set; }

    [Display(Name = "staff First Name")]
    [Required]
    public string? FirstName { get; set; }

    [Display(Name = "staff Last Name")]
    [Required]
    public string? LastName { get; set; }

    [Required]
    [UniqueEmail(userIdPropertyName: "UserId")]
    public string? Email { get; set; }

    [Display(Name = "Date Of Birth")]
    [Required]
    [DateOfBirth]
    public DateOnly? DateOfBirth { get; set; }

    [Required]
    [UniqueUsername("UserId")]
    [Username]
    public string? UserName { get; set; }

    [UniquePhoneNumber("UserId")] public string PhoneNumber { get; set; }
    public Guid UserId { get; set; }
    [Display(Name = "Start Schedule")] public TimeOnly StartSchedule { get; set; }
    public TimeOnly EndSchedule { get; set; }
    [Display(Name = "Day Of Work")] public byte DayOfWork { get; set; }
}