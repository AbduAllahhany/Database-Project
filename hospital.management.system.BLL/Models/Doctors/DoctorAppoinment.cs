namespace hospital.management.system.BLL.Models.Doctors;

public class DoctorAppoinment
{
    public Guid PatientId { get; set; }
    public string FullName { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string? Reason { get; set; }
    public DateOnly Date { get; set; }

}