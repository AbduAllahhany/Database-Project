
using System.ComponentModel.DataAnnotations;
using hospital.management.system.BLL.Filters;
using hospital.management.system.Models.Enums;

namespace hospital.management.system.BLL.Models.Accounts;

public class RegisterModel
{
    [Required]
    public string UserName { get; set; }

    [Required] 
    [EmailAddress] 
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
    public DateTime DateOfbirth { get; set; } = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Compare("Password")]
    public string ConfirmPassword { get; set; }

    public Gender Gender { get; set; }

}

public class RegisterResponseModel
{
    public string UserName { get; set; }

    public string Email { get; set; }

}

