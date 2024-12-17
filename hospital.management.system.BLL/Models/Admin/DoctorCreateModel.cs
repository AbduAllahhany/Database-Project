using System.ComponentModel.DataAnnotations;
using hospital.management.system.BLL.Filters;
using hospital.management.system.DAL;
using hospital.management.system.Models.Enums;

namespace hospital.management.system.BLL.Models.Admin;

public class DoctorCreateModel
{
    [Required]
    [OneWord]
    [Display(Name = "First Name")]
    public string FirstName { get; set; }

    [Required]
    [OneWord]
    [Display(Name = "Last Name")]
    public string LastName { get; set; }

    [Required]
    [EmailAddress]
    [UniqueEmail("")]
    [Display(Name = "Email Address")]
    public string Email { get; set; }
    

    [Required] [EgyptianPhoneNumber] 
    [Display(Name = "Phone Number")]
    [UniquePhoneNumber("UserId")]
    public string PhoneNumber { get; set; }
    
    [Display(Name = "National Id")]
    [Required] [NationalID] 
    public string SSN { get; set; }
    
    [Required] public Gender Gender { get; set; }
    [Required] [Username] 
    [UniqueUsername("UserId")]
    [Display(Name = "Username")]public string UserName { get; set; }

    [Required]
    [OneWord]
    [DoctorSpecialization]
    public string Specialization { get; set; }

    [Range(4, 8, ErrorMessage = "Working hours must be between 4 and 8.")]
    [Required(ErrorMessage = "Working hours are required.")]
    public int WorkingHours { get; set; }

    [Required]
    [Display(Name = "Start Schedule")]
    public TimeOnly? StartSchedule { get; set; }

    [Required]
    [Display(Name = "End Schedule")]
    public TimeOnly? EndSchedule { get; set; }
    
    public Guid? DepartmentId { get; set; }
    
    public string? DepartmentName { get; set; }
    public IEnumerable<string> Departments { get; set; } = SD.Departments.Keys;
}