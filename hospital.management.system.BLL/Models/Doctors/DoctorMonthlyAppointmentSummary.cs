namespace hospital.management.system.BLL.Models.Doctors;

public class DoctorMonthlyAppointmentSummary
{
    public int TotalAppointments { get; set; }
    public int ApprovedAppointments { get; set; }
    public int PendingAppointments { get; set; }
    public int RejectedAppointments { get; set; }
    public int CompletedAppointments { get; set; }
}