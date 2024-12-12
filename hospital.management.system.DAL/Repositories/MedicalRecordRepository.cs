
using hospital.management.system.DAL.Persistence;
using hospital.management.system.DAL.Repositories.IRepositories;
using hospital.management.system.Models.Entities;

namespace hospital.management.system.DAL.Repositories;

    internal class MedicalRecordRepository: GenericRepository<MedicalRecord>,IMedicalRecordRepository
    {
        private readonly ApplicationDbContext _context;

        public MedicalRecordRepository(ApplicationDbContext context) : base(context)
        {
        }
    }

