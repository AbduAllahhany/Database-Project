using hospital.management.system.Models.Enums;

namespace hospital.management.system.BLL.Models.Admin;

public class GetUpcomingAppointmentResponseModel
{
    
    public Guid Id { set; get; }
    public string PatientFirstName { set; get; }
    public string PatientLastName { set; get; }
    public string DoctorFirstName { set; get; }
    public string DoctorLastName { set; get; }
    public DateOnly Date { set; get; }
    public Status Status { set; get; }
}