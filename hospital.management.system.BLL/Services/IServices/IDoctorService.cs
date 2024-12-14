
using hospital.management.system.BLL.Models.Doctors;
using hospital.management.system.Models.Entities;

namespace hospital.management.system.BLL.Services.IServices;

public interface IDoctorService
{
<<<<<<< HEAD
    public IEnumerable<DoctorAppoinment> getPendingAppointments(Guid loggedDoctorId);

    public IEnumerable<DoctorAppoinment> getUpcomingAppointments(Guid loggedDoctorId);
    
    public IEnumerable<DoctorAppoinment> getDailyAppointments(Guid loggedDoctorId);
=======
    public IEnumerable<Doctor> GetPendingAppointments(Guid loggedDoctorId);

    public IEnumerable<Doctor> GetUpcomingAppointments(Guid loggedDoctorId);
    
    public IEnumerable<Doctor> GetDailyAppointments(Guid loggedDoctorId);
>>>>>>> b84c21a079197baf689b2e900cc3f5fad9e671e3
    
    public IEnumerable<Doctor> GetNextAppointmentInfo(Guid loggedDoctorId);
    
<<<<<<< HEAD
    public IEnumerable<DoctorAppoinment> getNextAppointmentInfo(Guid loggedDoctorId);

    public IEnumerable<DoctorMonthlyAppointmentSummary> getMonthlyAppointmentSummary(Guid loggedDoctorId);
=======
    public IEnumerable<Doctor> GetMonthlyAppointmentSummary(Guid loggedDoctorId);
    
    public int ApproveNextAppointment(Guid loggedDoctorId);

    public int PostponingAppointment(Guid loggedDoctorId);
>>>>>>> b84c21a079197baf689b2e900cc3f5fad9e671e3

    public int CancelingAppointment(DoctorCancelingAppointmentModel model);

    public int FollowUpAppointment(FollowUpAppointmentModel model);

    public int CreateMedicalRecord(MedicalRecordModel model);
}

