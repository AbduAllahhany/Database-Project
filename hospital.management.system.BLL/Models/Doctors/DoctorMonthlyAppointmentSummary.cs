namespace hospital.management.system.BLL.Models.Doctors;

public class DoctorMonthlyAppointmentSummary
{
    public int? TotalAppointments { get; set; } = 0;
    public int? ApprovedAppointments { get; set; } = 0;
    public int? PendingAppointments { get; set; } = 0;
    public int? RejectedAppointments { get; set; } = 0;
    public int? CompletedAppointments { get; set; } = 0;
}