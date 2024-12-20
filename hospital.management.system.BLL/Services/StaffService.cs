﻿
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
        user.PhoneNumber = model.PhoneNumber;
        await _context.SaveChangesAsync();

        string sqlcommand1 =
            $"""
              Update Staff SET firstName =@p0,lastName=@p1
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
                                                          SELECT Top(1) Id as Id ,firstName as FirstName, lastName as LastName,
                                                          startSchedule as StartSchedule, endSchedule as EndSchedule, role As Role,dayOfWork As DayOfWork,UserID
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
                                                          select Id as Id,firstName as FirstName, lastName as LastName, role as Role, endSchedule as EndSchedule, 
                                                                 startSchedule as StartSchedule,dayOfWork as DayOfWork,UserID
                                                          from Staff
                                                          """);
        var temp = await res.ToListAsync();
        return temp ?? null;
    }

    public async Task<ApplicationUser> GetUserByIdAsync(Guid? staffId = null)
    {
        var userId = await _context.Database.SqlQuery<Guid>($"""
                                                             select UserId from dbo.Staff
                                                             where Id = {staffId}
                                                             """).ToListAsync();
        if (userId == null) return null;
        var user = await _userManager.FindByIdAsync(userId.FirstOrDefault().ToString());
        return user;
    }

}

