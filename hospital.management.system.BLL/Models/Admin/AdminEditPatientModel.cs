using System.ComponentModel.DataAnnotations;
using hospital.management.system.BLL.Filters;

namespace hospital.management.system.BLL.Models.Admin;

public class AdminEditPatientModel
{
    [Required] public Guid? PatientId { get; set; }

    [Display(Name = "Patient First Name")]
    [Required]
    public string? FirstName { get; set; }

    [Display(Name = "Patient Last Name")]
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

    [Required] public string? Address { get; set; }
    public string? BloodGroup { get; set; }
    public string? Allergies { get; set; }
    public string? ChronicDiseases { get; set; }
    [UniquePhoneNumber("UserId")] public string PhoneNumber { get; set; }
    public Guid UserId { get; set; }
}