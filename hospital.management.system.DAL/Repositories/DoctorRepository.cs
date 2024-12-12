
using hospital.management.system.DAL.Persistence;
using hospital.management.system.DAL.Repositories.IRepositories;
using hospital.management.system.Models.Entities;

namespace hospital.management.system.DAL.Repositories;

    public class DoctorRepository: GenericRepository<Doctor>,IDoctorRepository
    {
        private readonly ApplicationDbContext _context;

        public DoctorRepository(ApplicationDbContext context) : base(context)
        {
        }
        public IEnumerable<Doctor> SpecialMethod()
        {
            throw new NotImplementedException();
        }
    }

