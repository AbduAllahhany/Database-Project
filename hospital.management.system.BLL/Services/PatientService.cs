

using hospital.management.system.BLL.Services.IServices;
using hospital.management.system.DAL;

namespace hospital.management.system.BLL.Services;

public class PatientService : IPatientService
    {
        private readonly IUnitOfWork unitOfWork;

        public PatientService(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }
    }

