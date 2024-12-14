using System.ComponentModel.DataAnnotations;
using hospital.management.system.BLL.Filters;

namespace hospital.management.system.BLL.Models.Admin;

public class AdminEditPatientModel
{
    [Required] public Guid? PatientId { get; set; }
    [Required] public string? FirstName { get; set; }
    [Required] public string? LastName { get; set; }
    [Required] public string? Email { get; set; }
    [Required] public string? SSN { get; set; }
    [Required] [DateOfBirth] public DateOnly? DateOfBirth { get; set; }
    [Required] [Username] public string? UserName { get; set; }
    [Required] public string? Address { get; set; }
}