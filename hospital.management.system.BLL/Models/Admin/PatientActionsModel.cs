namespace hospital.management.system.BLL.Models.Admin;

public class PatientActionsModel
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string? BloodGroup { get; set; }
    public string? ChronicDiseases { get; set; }
}