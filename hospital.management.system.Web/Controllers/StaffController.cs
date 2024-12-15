using System.Security.Claims;
using hospital.management.system.BLL.Models.Staff;
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

    public StaffController(
        IUnitOfWork unitOfWork,
        ApplicationDbContext context)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _context = context ?? throw new ArgumentNullException(nameof(context));
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
        //var staff =_context.Patients.FromSqlInterpolated($@"select * from Staff where UserId={id}").FirstOrDefault();
        if (staff == null || staff.Id == Guid.Empty)
        {
            throw new InvalidOperationException("Patient is not authenticated or the NameIdentifier claim is missing.");
        }

        return staff.Id;
    }

    // GET
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult StaffDashboard()
    {
        try
        {
            // Get the logged-in staff's ID
            var staffId = GetStaffId();

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
}
    
