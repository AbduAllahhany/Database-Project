
using hospital.management.system.BLL.Models.Doctors;
using hospital.management.system.Models.Entities;

namespace hospital.management.system.BLL.Services.IServices;

public interface IDoctorService
{
    public IEnumerable<DoctorAppoinment> getPendingAppointments(Guid loggedDoctorId);

    public IEnumerable<DoctorAppoinment> getUpcomingAppointments(Guid loggedDoctorId);

    public IEnumerable<DoctorAppoinment> getDailyAppointments(Guid loggedDoctorId);

    public int approveNextAppointment(Guid loggedDoctorId);

    public int postponingAppointment(Guid loggedDoctorId);

    public int cancelingAppointment(DoctorCancelingAppointmentModel model);

    public IEnumerable<DoctorAppoinment> getNextAppointmentInfo(Guid loggedDoctorId);

    public IEnumerable<DoctorMonthlyAppointmentSummary> getMonthlyAppointmentSummary(Guid loggedDoctorId);

    public int followUpAppointment(FollowUpAppointmentModel model);

    public int CreateMedicalRecord(MedicalRecordModel model);

    public Task<GetDoctorProfileModel> GetDoctorByUserId(Guid Id);

    // public IEnumerable<Doctor> viewPatientHistory();

    public List<Doctor> GetAllDoctors();
    
    public List<DoctorAppoinment> GetDoctorAppointments(Guid doctorId);
    
    public int deleteDoctor(Guid doctorId);
}

