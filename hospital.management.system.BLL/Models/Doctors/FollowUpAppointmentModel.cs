namespace hospital.management.system.BLL.Models.Doctors;

public class FollowUpAppointmentModel
{
    public Guid DoctorId { get; set; }
    public Guid PatientId { get; set; }
    public DateOnly AppointmentDate { get; set; }
    public string? Reason { get; set; }
}