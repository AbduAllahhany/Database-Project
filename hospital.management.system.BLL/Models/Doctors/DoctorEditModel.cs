using System.ComponentModel.DataAnnotations;
using hospital.management.system.BLL.Filters;
using hospital.management.system.Models.Enums;
namespace hospital.management.system.BLL.Models.Doctors;

public class DoctorEditModel
{
    public string? Id { get; set; }
        
    public string Address { get; set; }
    
    [Username]
    [UniqueUsername]
    public string UserName { get; set; }
    
    [Display(Name = "First Name")]
    public string FirstName { get; set; }
    
    [Display(Name = "Last Name")]
    public string LastName { get; set; }
    
    [DataType(DataType.EmailAddress)]
    [UniqueEmail]
    public string Email { get; set; }
      
    [DateOfBirth]
    [Display(Name = "Date of Birth")]
    [Required]
    public DateOnly DateOfBirth { get; set; }
        
    [Display(Name = "Phone Number")]
    [EgyptianPhoneNumber]
    [UniquePhoneNumber]
    public string PhoneNumber { get; set; }
    
    [Required]
    [DataType(DataType.Password)]
    public string password { get; set; }
    
    [Display(Name = "Confirm Password")]
    [Required]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string confirmPassword { get; set; }
}