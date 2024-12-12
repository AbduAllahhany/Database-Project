
using hospital.management.system.BLL.Services.IServices;
using hospital.management.system.DAL;

namespace hospital.management.system.BLL.Services;

public class StaffService : IStaffService
{
    private readonly IUnitOfWork unitOfWork;

    public StaffService(IUnitOfWork _unitOfWork)
    {
        unitOfWork = _unitOfWork;
    }
}

