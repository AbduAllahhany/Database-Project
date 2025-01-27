﻿
using hospital.management.system.BLL.Models.Staff;
using hospital.management.system.Models.Entities;

namespace hospital.management.system.BLL.Services.IServices;

public interface IStaffService
{
    public Task<int> EditStaffAsync(StaffEditModel model);
    public Task<int> GetStaffCountAsync();
    public Task<StaffModel> GetStaffByIdAsync(Guid? Id);
    public Task<IEnumerable<StaffModel>> GetAllTask();
    public Task<ApplicationUser> GetUserByIdAsync(Guid? staffId = null);
}

