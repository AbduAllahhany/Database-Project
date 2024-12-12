namespace hospital.management.system.BLL.Models.Doctors;

public class MedicalRecordModel
{
    public Guid LoggedDoctorId { get; set; }
    public Guid SelectedPatientId { get; set; }
    public string? Diagnostic { get; set; }
    public string? Prescription { get; set; }
}