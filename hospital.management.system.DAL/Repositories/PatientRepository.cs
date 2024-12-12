

using hospital.management.system.DAL.Persistence;
using hospital.management.system.DAL.Repositories.IRepositories;
using hospital.management.system.Models.Entities;

namespace hospital.management.system.DAL.Repositories;

public class PatientRepository : GenericRepository<Patient>, IPatientRepository
{
    private readonly ApplicationDbContext _context;

    public PatientRepository(ApplicationDbContext context) : base(context)
    {
    }
}

