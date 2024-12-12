using hospital.management.system.Models.Enums;

namespace hospital.management.system.BLL.Models.Admin;

public class GetAllAppointmentsResponseModel
{
    public string PatientFirstName { get; set; }
    public string PatientLastName { get; set; }
    public string DoctorFirstName { get; set; }
    public string DoctorLastName { get; set; }
    public Status Status { get; set; }
    public string Reason { get; set; }
    public DateOnly Date { get; set; }

}