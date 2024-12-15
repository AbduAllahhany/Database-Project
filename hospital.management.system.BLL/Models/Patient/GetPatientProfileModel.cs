using hospital.management.system.Models.Enums;

namespace hospital.management.system.BLL.Models.Patient;

public class GetPatientProfileModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? BloodGroup { get; set; }
    public string? Allergies { get; set; }
    public string? ChronicDiseases { get; set; }
    public string? Address { get; set; }
    public DateOnly DateOfBirth { get; set; }

}