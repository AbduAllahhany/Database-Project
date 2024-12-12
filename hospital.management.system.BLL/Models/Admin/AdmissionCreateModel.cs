namespace hospital.management.system.BLL.Models.Admin;

public class AdmissionCreateModel
{
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public Guid RoomId { get; set; }
    public Guid PatientId { get; set; }
}