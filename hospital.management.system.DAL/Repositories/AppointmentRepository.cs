
using hospital.management.system.DAL.Persistence;
using hospital.management.system.DAL.Repositories.IRepositories;
using hospital.management.system.Models.Entities;

namespace hospital.management.system.DAL.Repositories;

public class AppointmentRepository : GenericRepository<Appointment>, IAppointmentRepository
{
    private readonly ApplicationDbContext _context;

    public AppointmentRepository(ApplicationDbContext context) : base(context)
    {
    }
}

