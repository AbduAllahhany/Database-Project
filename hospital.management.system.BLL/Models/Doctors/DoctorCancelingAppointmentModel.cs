namespace hospital.management.system.BLL.Models.Doctors;

public class DoctorCancelingAppointmentModel
{
    public Guid LoggedDoctorId { get; set; }
    public Guid SelectedPatientId { get; set; }
}