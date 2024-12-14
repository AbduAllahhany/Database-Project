using System.ComponentModel.DataAnnotations;
using hospital.management.system.BLL.Filters;
using hospital.management.system.Models.Enums;

namespace hospital.management.system.BLL.Models.Admin;

public class AdminEditModel
{
    public string? Id { get; set; }
        
   // public string Address { get; set; }
        
    [Display(Name = "National Id Or Passpart")]
    [NationalID(ErrorMessage = "Please enter a valid SSN")]
    public string SSN { get; set; }
      
    [DateOfBirth]
    [Display(Name = "Date of Birth")]
    [Required]
    public DateOnly DateOfBirth { get; set; }
        
    [Display(Name = "User Name")]
    [Username]
    public string UserName { get; set; }
        
    [Display(Name = "Email")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
        
    [Display(Name = "Phone Number")]

    public string PhoneNumber { get; set; }
        
    public Gender Gender { get; set; }
    
}