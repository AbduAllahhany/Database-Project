using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using hospital.management.system.BLL.Filters;
using hospital.management.system.Models.Enums;

namespace hospital.management.system.BLL.Models.Admin;

public class PatientCreateModel
{

    [Display(Name = "First Name")]
    [Required]
    [OneWord]
    public string? FirstName { get; set; }

    [Required]
    [OneWord]
    [Display(Name = "Last Name")]
    public string LastName { get; set; }

    [Required] 
    [EmailAddress] 
    [UniqueEmail]
    public string Email { get; set; }

    [Required]
    [EgyptianPhoneNumber]
    [UniquePhoneNumber]
    [Display(Name = "Phone Number")]
    public string PhoneNumber { get; set; }

    [Required] public string? Address { get; set; }

    [Required]
    [NationalID]
    [Display(Name = "National ID")]
    public string? SSN { get; set; }

    [Required] public Gender? Gender { get; set; }

    [Required]
    [Username]
    [UniqueUsername]
    [Display(Name = "Username")]
    public string? UserName { get; set; }

    [Required]
    [DateOfBirth]
    [Display(Name = "Date Of Birth")]
    public DateOnly? DateOfBirth { get; set; }

    [Required]
    [EnumDataType(typeof(BloodGroup), ErrorMessage = "Invalid blood group.")]
    public int BloodGroup { get; set; }

    [Required] public string? Allergies { get; set; }
    [Required] public string? ChronicDiseases { get; set; }

}