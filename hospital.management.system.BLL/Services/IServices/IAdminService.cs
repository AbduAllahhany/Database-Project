using hospital.management.system.BLL.Models.Admin;
using hospital.management.system.BLL.Models.Doctors;
using hospital.management.system.BLL.Models.Patient;
using hospital.management.system.Models.Entities;

namespace hospital.management.system.BLL.Services.IServices;

public interface IAdminService
{
    public Task<int> CreatePatientAsync(PatientCreateModel model);
    public Task<int> CreateDoctorAsync(DoctorCreateModel model);
    public Task<int> CreateStaffAsync(StaffCreateModel model);
    public Task<int> CreateVisitAsync(VisitCreateModel model);
    public Task<int> CreateBillAsync(BillCreateModel model);
    public Task<int> CreateAdmissionAsync(AdmissionCreateModel model);
    public Task<int> CreateInsuranceAsync(InsuranceCreateModel model);
    public Task<int> CreateEmergencyContactAsync(EmergencyContactCreateModel model);
    public Task<IEnumerable<GetUpcomingAppointmentResponseModel>> GetUpcomingAppointmentAsync();
    public Task<IEnumerable<GetAllAdminsResponseModel>> GetAllAdminsAsync();
    public Task<IEnumerable<GetAllAppointmentsResponseModel>> GetAllAppointmentsByNamesAsync();

    public Task<int> AdminCreateAsync(AdminCreateModel model);

    public Task<int> CreateAppointmentAsync(AppointmentAddModel model);

    //public Task<int> EditStaffAsync(StaffEditModel model);
    public Task<int> GetAppointmentCountAsync();
    public Task<int> ConfirmRoomAsync(Guid? roomId);
    public Task<IEnumerable<AvailableRoomsModel>> GetAvailableRoomsAsync();
    public Task<IEnumerable<GetAllAppointmentsResponseModel>> GetAppointmentsByUsernamesAsync();
    public Task<int> AdminEditPatientAsync(AdminEditPatientModel model);
    public Task<int> AdminEditDoctortAsync(AdminEditDoctorModel model);
    public Task<int> AdminEditStaffAsync(AdminEditStaffModel model);
    public Task<IEnumerable<UsernameIdModel>> GetAllPatientsAsync();
    public Task<IEnumerable<UsernameIdModel>> GetAllDoctorsAsync();
    public Task<int> AdminEditDoctorAsync(AdminEditDoctorModel model);


}