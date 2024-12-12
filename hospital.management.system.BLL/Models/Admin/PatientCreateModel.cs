using System.ComponentModel.DataAnnotations;
using hospital.management.system.BLL.Filters;
using hospital.management.system.Models.Enums;

namespace hospital.management.system.BLL.Models.Admin;

public class PatientCreateModel
{
    [Required]
    [OneWord]
    public string FirstName { get; set; }
    [Required]
    [OneWord]
    public string LastName{ get; set; }
    
    [Required]
    [EmailAddress]
    [UniqueEmail]
    public string Email { get; set; }
    
    [Required]
    [EgyptianPhoneNumber]
    public string PhoneNumber { get; set; }
    [Required]
    public string Address { get; set; }
    [Required]
    [NationalID]
    public string SSN { get; set; }
    [Required]
    public Gender Gender { get; set; }
    [Required]
    [Username]
    [UniqueUsername]
    public string UserName { get; set; }
    
   [Required]
   [DateOfBirth]
    public DateOnly DateOfBirth { get; set; }
    
}