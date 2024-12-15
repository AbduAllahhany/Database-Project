
using hospital.management.system.BLL.Models.Staff;
using hospital.management.system.BLL.Services.IServices;
using hospital.management.system.DAL;
using hospital.management.system.DAL.Persistence;
using hospital.management.system.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace hospital.management.system.BLL.Services;

public class StaffService : IStaffService
{
    private readonly IUnitOfWork unitOfWork;
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public StaffService(IUnitOfWork _unitOfWork, ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        unitOfWork = _unitOfWork;
        _context = context;
        _userManager = userManager;
    }

    public async Task<int> EditStaffAsync(StaffEditModel model)
    {
        if (model == null) return 0;
        Guid? staffId = model.Id;
        if (staffId == null) return 0;
        var user = await _userManager.FindByIdAsync(model.Id.ToString());
        //update
        user.UserName = model.Username;


        string sqlcommand1 =
            $"""
              Update Staff SET firstName =@p0,lastName=@p1,role=@p2,
              where Id=@p3
             """;
        var res = await _context.Database.ExecuteSqlRawAsync(sqlcommand1,
            model.FirstName,
            model.LastName,
            user.Id);
        return res;
    }

    public async Task<StaffModel> GetStaffByIdAsync(Guid? Id)
    {
        var res = _context.Database.SqlQuery<StaffModel>($"""
                                                          SELECT Top(1) firstName as FirstName, lastName as LastName,phoneNumber as PhoneNumber,
                                                          startSchedule as StartSchedule, endSchedule as EndSchedule,
                                                          dayOfWork as DayOfWork
                                                          from Staff
                                                          where Id = {Id} 
                                                          """);
        
        var temp = await res.ToListAsync();
        return res.FirstOrDefault() ?? null;
    }

    public async Task<int> GetStaffCountAsync()
    {
        var res = _context.Database.SqlQuery<int>($@"select count(*) from Staff");
        var count = await res.ToListAsync();
        return count.FirstOrDefault();
    }

    public async Task<IEnumerable<StaffModel>> GetAllTask()
    {
        var res = _context.Database.SqlQuery<StaffModel>($"""
                            select Id as Id,firstName as FirstName, lastName as LastName, role as Role, endSchedule as EndSchedule, startSchedule as StartSchedule,dayOfWork as DayOfWork
                            from Staff
                            """);
        var temp = await res.ToListAsync();
        return temp ?? null;
    }
}

