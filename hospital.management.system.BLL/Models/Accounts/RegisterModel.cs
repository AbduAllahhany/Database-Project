
using System.ComponentModel.DataAnnotations;
using hospital.management.system.BLL.Filters;
using hospital.management.system.Models.Enums;

namespace hospital.management.system.BLL.Models.Accounts;

public class RegisterModel
{
    [Required]
    [Display(Name = "Username")]
    [UniqueUsername]
    public string UserName { get; set; }

    [Required] 
    [EmailAddress] 
    [UniqueEmail]
    public string Email { get; set; }

    [Required]
    [Display(Name = "First Name")]
    public string FirstName { get; set; }

    [Required]
    [Display(Name = "Last Name")]
    public string LastName { get; set; }

    [Required]
    [Display(Name = "Date of birth")]
    [DateOfBirth]
    public DateOnly DateOfBirth { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    
    [Required] 
    [NationalID] 
    public string SSN { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }

    [Required]
    public Gender Gender { get; set; }
    [Required]
    [EgyptianPhoneNumber]
    public string Phone { get; set; }

}

public class RegisterResponseModel
{
    public string UserName { get; set; }

    public string Email { get; set; }

}

