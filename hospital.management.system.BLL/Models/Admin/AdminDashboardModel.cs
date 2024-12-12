using hospital.management.system.BLL.Models.Admin;

namespace hospital.management.system.Web.Models.Admin;

public class AdminDashboardModel
{
    public int PatientsCount { get; set; }
    public int DoctorsCount { get; set; }
    public int StaffCount { get; set; }
    public int AppointmentsCount { get; set; }
    public IEnumerable<GetUpcomingAppointmentResponseModel> upcomingAppointments { get; set; }
}