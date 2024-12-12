using System.ComponentModel.DataAnnotations;
using hospital.management.system.BLL.Filters;
using hospital.management.system.DAL;
using hospital.management.system.Models.Enums;

namespace hospital.management.system.BLL.Models.Admin;

public class DoctorCreateModel
{
    [Required] [OneWord] public string FirstName { get; set; }
    [Required] public string LastName { get; set; }

    [Required]
    [EmailAddress]
    [UniqueEmail]
    public string Email { get; set; }

    [Required] [EgyptianPhoneNumber] public string PhoneNumber { get; set; }
    [Required] public string Address { get; set; }
    [Required] [NationalID] public string SSN { get; set; }
    [Required] public Gender Gender { get; set; }
    [Required] [Username] [UniqueUsername] public string UserName { get; set; }

    [Required]
    [OneWord]
    [DoctorSpecialization]
    public string Specialization { get; set; }

    [Range(4, 8, ErrorMessage = "Working hours must be between 4 and 8.")]
    [Required(ErrorMessage = "Working hours are required.")]
    public int WorkingHours { get; set; }

    public Guid? DepartmentId { get; set; }
    [Required] public string DepartmentName { get; set; }
    public IEnumerable<string> Departments { get; set; } = SD.ValidDepartments;
}