namespace hospital.management.system.BLL.Models.Patient;

public class PatientProfileModel : ProfileModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? BloodGroup { get; set; }
    public string? Allergies { get; set; }
    public string ChronicDiseases { get; set; }
    public string? Address { get; set; }
    public DateOnly? Birthdate { get; set; }
}