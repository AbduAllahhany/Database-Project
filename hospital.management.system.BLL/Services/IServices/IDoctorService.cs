
using hospital.management.system.BLL.Models.Doctors;
using hospital.management.system.Models.Entities;

namespace hospital.management.system.BLL.Services.IServices;

public interface IDoctorService
{
    public IEnumerable<Doctor> getPendingAppointments(Guid loggedDoctorId);

    public IEnumerable<Doctor> getUpcomingAppointments(Guid loggedDoctorId);
    
    public IEnumerable<Doctor> getDailyAppointments(Guid loggedDoctorId);
    
    public int approveNextAppointment(Guid loggedDoctorId);

    public int postponingAppointment(Guid loggedDoctorId);

    public int cancelingAppointment(DoctorCancelingAppointmentModel model);
    
    public int getNextAppointmentInfo(Guid loggedDoctorId);

    public IEnumerable<Doctor> getMonthlyAppointmentSummary(Guid loggedDoctorId);

    public int followUpAppointment(FollowUpAppointmentModel model);

    public int CreateMedicalRecord(MedicalRecordModel model);

    // public IEnumerable<Doctor> viewPatientHistory();



}

