using System.ComponentModel.DataAnnotations;
using hospital.management.system.BLL.Filters;
using hospital.management.system.Models.Enums;

namespace hospital.management.system.BLL.Models.Admin;

public class AdminEditModel
{
    public Guid? Id { get; set; }

    [Display(Name = "National Id Or Passpart")]
    [NationalID(ErrorMessage = "Please enter a valid SSN")]
    public string SSN { get; set; }

    [DateOfBirth]
    [Display(Name = "Date of Birth")]
    [Required]
    public DateOnly DateOfBirth { get; set; }

    [Display(Name = "Username")]
    [Username]
    [UniqueUsername("")]
    public string UserName { get; set; }

    [Display(Name = "Email")]
    [DataType(DataType.EmailAddress)]
    [UniqueEmail]
    public string Email { get; set; }

    [UniquePhoneNumber("")]
    [Display(Name = "Phone Number")]
    public string PhoneNumber { get; set; }


}