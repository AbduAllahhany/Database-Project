using hospital.management.system.DAL.Persistence;
using hospital.management.system.DAL.Repositories.IRepositories;
using hospital.management.system.Models.Entities;

namespace hospital.management.system.DAL.Repositories;

public class StaffRepository : GenericRepository<Staff>, IStaffRepository
{
    private readonly ApplicationDbContext _context;

    public StaffRepository(ApplicationDbContext context) : base(context)
    {
        
    }
}