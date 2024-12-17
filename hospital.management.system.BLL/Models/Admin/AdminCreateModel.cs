using System.ComponentModel.DataAnnotations;
using hospital.management.system.BLL.Filters;
using hospital.management.system.Models.Enums;

namespace hospital.management.system.BLL.Models.Admin;

public class AdminCreateModel
{

    [Required]
    [EmailAddress]
    [UniqueEmail]
    public string Email { get; set; }

    [Required]
    [Username]
    [UniqueUsername("UserId")]
    public string UserName { get; set; }

    [UniquePhoneNumber("UserId")]
    [EgyptianPhoneNumber]
    public string PhoneNumber { get; set; }

    public string SSN { get; set; }
    public Gender Gender { get; set; }
}