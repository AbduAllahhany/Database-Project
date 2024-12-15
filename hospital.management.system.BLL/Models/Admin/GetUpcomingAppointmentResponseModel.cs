using hospital.management.system.Models.Enums;

namespace hospital.management.system.BLL.Models.Admin;

public class GetUpcomingAppointmentResponseModel
{
    //public Guid Id { set; get; }
    public string PatientName { set; get; }
    public string DoctorName { set; get; }
    public DateOnly Date { set; get; }
    public TimeOnly Time { set; get; }
    //public string Status { set; get; }
}