using System.ComponentModel.DataAnnotations;
using hospital.management.system.BLL.Filters;
using hospital.management.system.Models.Enums;
namespace hospital.management.system.BLL.Models.Patient;

public class PatientEditModel
{
    public Guid? UserId { get; set; }

    public string Address { get; set; }

    [Username] [UniqueUsername("UserId")] public string UserName { get; set; }

    [Display(Name = "First Name")] public string FirstName { get; set; }

    [Display(Name = "Last Name")] public string LastName { get; set; }

    [DateOfBirth]
    [Display(Name = "Date of Birth")]
    [Required]
    public DateOnly DateOfBirth { get; set; }

    [UniquePhoneNumber("UserId")]
    [Display(Name = "Phone Number")]
    public string PhoneNumber { get; set; }


}