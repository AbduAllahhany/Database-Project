
using hospital.management.system.DAL.Repositories.IRepositories;

namespace hospital.management.system.DAL;
    public interface IUnitOfWork : IDisposable
    {

        IStaffRepository Staff { get; }
        IDoctorRepository Doctors { get; }
        IPatientRepository Patients { get; }
        IAppointmentRepository Appointments { get; }
        IMedicalRecordRepository MedicalRecords { get; }

        Task<int> CompleteAsync();
    }

