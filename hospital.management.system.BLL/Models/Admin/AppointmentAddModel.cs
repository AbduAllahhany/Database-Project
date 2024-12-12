using hospital.management.system.Models.Enums;

namespace hospital.management.system.BLL.Models.Admin;

public class AppointmentAddModel
{
    public Guid PatientId { get; set; }
    public Guid DoctorId { get; set; }
    public DateOnly Date { get; set; }
    public DateTime Time { get; set; }
    public string Reason { get; set; }
    public Status Status { get; set; }


}