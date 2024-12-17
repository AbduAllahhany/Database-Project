using System.Security.Claims;
using hospital.management.system.BLL.Models.Staff;
using hospital.management.system.BLL.Services.IServices;
using hospital.management.system.DAL;
using hospital.management.system.DAL.Persistence;
using hospital.management.system.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace hospital.management.system.Web.Controllers;

//[Authorize(Roles = SD.Nurse)]     ====>>>> modify this 
public class StaffController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ApplicationDbContext _context;
    private readonly IStaffService _staffService;


    public StaffController(
        IUnitOfWork unitOfWork,
        ApplicationDbContext context, IStaffService staffService)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _staffService = staffService;
    }


    private Guid GetStaffId()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userId))
        {
            throw new InvalidOperationException("User is not authenticated or the NameIdentifier claim is missing.");
        }

        if (!Guid.TryParse(userId, out Guid id))
        {
            throw new FormatException("User ID is not a valid GUID.");
        }

        var staff = (_context.Staff.FirstOrDefault(e => e.UserId == id));
        if (staff == null || staff.Id == Guid.Empty)
        {
            throw new InvalidOperationException("Patient is not authenticated or the NameIdentifier claim is missing.");
        }

        return staff.Id;
    }

    // GET

    public IActionResult Dashboard(Guid staffId)
    {
        try
        {
            ;
            // Fetch staff details using LINQ
            Staff staff = _context.Staff
                .FirstOrDefault(s => s.Id == staffId);

            if (staff == null)
            {
                return NotFound("Staff member not found");
            }

            // Fetch related department details
            var department = _context.Departments
                .FirstOrDefault(d => d.Id == staff.DeptId);

            // Create a list of relevant staff schedules
            var staffSchedule = new
            {
                StartTime = staff.StartSchedule,
                EndTime = staff.EndSchedule,
                DayOfWork = staff.DayOfWork
            };

            // Build the StaffDashboardModel object
            var staffDashboard = new StaffDashboardModel()
            {
                Staff = staff,
                department = department,

            };

            // Pass the StaffDashboardModel object to the view
            return View("Dashboard", staffDashboard);
        }
        catch (Exception ex)
        {
            // Log exception (optional)
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
    }

    public async Task<IActionResult> Profile()
    {
        var staff = await _staffService.GetStaffByIdAsync(GetStaffId());
        var user = await _staffService.GetUserByIdAsync(GetStaffId());
        return View();
    }

    public async Task<IActionResult> Edit()
    {
        var staff = await _staffService.GetStaffByIdAsync(GetStaffId());
        var user = await _staffService.GetUserByIdAsync(GetStaffId());
        var model = new StaffEditModel()
        {
            Id = staff.Id,
            FirstName = staff.FirstName,
            LastName = staff.LastName,
            PhoneNumber = user.PhoneNumber,
            Username = user.UserName,
        };
        return View(model);
    }

    public async Task<IActionResult> Edit(StaffEditModel model)
    {
        if (!ModelState.IsValid) return View(model);
        var res = await _staffService.EditStaffAsync(model);
        return res == 1 ? RedirectToAction("Profile") : View("Error");
    }
}
    
