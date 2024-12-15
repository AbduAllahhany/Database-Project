namespace hospital.management.system.BLL.Models.Doctors;

public class DoctorAppoinment
{
    
    public Guid AppId { get; set; }
    public Guid PatientId { get; set; }
    public string FullName { get; set; }
    public int DateOfBirth { get; set; }
    public string? Reason { get; set; }
    public DateOnly Date { get; set; }
    public string Status { get; set; }

}