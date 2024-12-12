using hospital.management.system.DAL.Persistence;
using hospital.management.system.DAL.Repositories;
using hospital.management.system.DAL.Repositories.IRepositories;



namespace hospital.management.system.DAL;

    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IPatientRepository Patients{ get; private set; }
        public IDoctorRepository Doctors{ get; private set; }
        public IStaffRepository Staff{ get; private set; }
        public IAppointmentRepository Appointments { get; private set; }
        public IMedicalRecordRepository MedicalRecords { get; private set; }


        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;

            Patients = new PatientRepository(_context);
            Doctors = new DoctorRepository(_context);
            Staff = new StaffRepository(_context);
            Appointments = new AppointmentRepository(_context);
            MedicalRecords = new MedicalRecordRepository(_context);
        }
        
        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }

