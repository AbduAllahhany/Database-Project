
using hospital.management.system.BLL.Models.Doctors;
using hospital.management.system.Models.Entities;

namespace hospital.management.system.BLL.Services.IServices;

public interface IDoctorService
{
    public IEnumerable<Doctor> GetPendingAppointments(Guid loggedDoctorId);

    public IEnumerable<Doctor> GetUpcomingAppointments(Guid loggedDoctorId);
    
    public IEnumerable<Doctor> GetDailyAppointments(Guid loggedDoctorId);
    
    public IEnumerable<Doctor> GetNextAppointmentInfo(Guid loggedDoctorId);
    
    public IEnumerable<Doctor> GetMonthlyAppointmentSummary(Guid loggedDoctorId);
    
    public int ApproveNextAppointment(Guid loggedDoctorId);

    public int PostponingAppointment(Guid loggedDoctorId);

    public int CancelingAppointment(DoctorCancelingAppointmentModel model);

    public int FollowUpAppointment(FollowUpAppointmentModel model);

    public int CreateMedicalRecord(MedicalRecordModel model);
}

