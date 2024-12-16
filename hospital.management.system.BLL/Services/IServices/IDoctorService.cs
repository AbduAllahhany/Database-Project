
using hospital.management.system.BLL.Models.Doctors;
using hospital.management.system.Models.Entities;

namespace hospital.management.system.BLL.Services.IServices;

public interface IDoctorService
{
    public IEnumerable<DoctorAppoinment> GetPendingAppointments(Guid loggedDoctorId);

    public IEnumerable<DoctorAppoinment> GetUpcomingAppointments(Guid loggedDoctorId);

    public IEnumerable<DoctorAppoinment> GetDailyAppointments(Guid loggedDoctorId);

    //???????????????????????????????????????
    public int ApproveNextAppointment(DoctorCancelingAppointmentModel loggedDoctorId);

    public int PostponingAppointment(Guid loggedDoctorId);

    public int CancelingAppointment(DoctorCancelingAppointmentModel model);

    public IEnumerable<DoctorAppoinment> GetNextAppointmentInfo(Guid loggedDoctorId);

    public IEnumerable<DoctorMonthlyAppointmentSummary> GetMonthlyAppointmentSummary(Guid loggedDoctorId);

    public int FollowUpAppointment(FollowUpAppointmentModel model);

    public int CreateMedicalRecord(MedicalRecordModel model);

    public Task<GetDoctorProfileModel> DocotorProfileDataByIdAsync(Guid Id);
    //public  Task<int> GetDoctorsCountAsync();
    public  Task<int> EditDoctorAsync(DoctorEditModel? model);

    // public IEnumerable<Doctor> viewPatientHistory();

    public List<Doctor> GetAllDoctors();
    
    public List<DoctorAppoinment> GetDoctorAppointments(Guid doctorId);
    
    public int DeleteDoctor(Guid doctorId);
    
    public Task<int> GetDoctorsCountAsync();
    public Task<ApplicationUser> GetUserByIdAsync(Guid docotorId);
}

