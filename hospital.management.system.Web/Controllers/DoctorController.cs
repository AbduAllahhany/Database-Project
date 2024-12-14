using System.Security.Claims;
using hospital.management.system.BLL.Models.Doctors;
using hospital.management.system.BLL.Services;
using hospital.management.system.BLL.Services.IServices;
using hospital.management.system.DAL;
using hospital.management.system.DAL.Persistence;
using hospital.management.system.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace hospital.management.system.Web.Controllers;

public class DoctorController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ApplicationDbContext _context;
    private readonly IDoctorService _doctorService;

    public DoctorController(
        IUnitOfWork unitOfWork, 
        ApplicationDbContext context, 
        IDoctorService DoctorService)
    {
        _unitOfWork = unitOfWork;
        _context = context;
        // _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _doctorService = DoctorService;
    }

// ===>>> revise this function 
    private Guid GetUserId()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        Guid id = Guid.Parse(userId);
       // return id;
        Doctor DoctorId = _context.Doctors.FirstOrDefault(e=>e.UserId == id);
        return DoctorId.Id;
    }
    
    public IActionResult IdToPendingAppointment()
    {
        
        var pendingAppointment = _doctorService.getPendingAppointments(GetUserId());
        return View("IdToPendingAppointment",pendingAppointment);
    }
    
    public IActionResult IdToDailyAppointment()
    {
        var dailyAppointment = _doctorService.getDailyAppointments(GetUserId());
        return View("IdToDailyAppointment",dailyAppointment);
    }
    
    public IActionResult IdToUpcomingAppointment()
    {
        var upcomingAppointment = _doctorService.getUpcomingAppointments(GetUserId());
        return View("IdToUpcomingAppointment",upcomingAppointment);
    }
    
    public IActionResult IdToApproveAppointment()
    {
        var approveAppointment = _doctorService.approveNextAppointment(GetUserId());
        return View("IdToApproveAppointment",approveAppointment);
    }
    // ==> what is the view should do ?!!!!!!!!!!!!!!!!!!
    public IActionResult IdToPostponeAppointment()
    {
        var postponeAppointment = _doctorService.postponingAppointment(GetUserId());
        return View(postponeAppointment);
    }
    
    public IActionResult CancelingAppointment(Guid patientId)
    {
        var model = new DoctorCancelingAppointmentModel
        {
            LoggedDoctorId = GetUserId(),
            SelectedPatientId = patientId,             
        };
        var cancelAppointment = _doctorService.cancelingAppointment(model);
        return View("CancelingAppointment",cancelAppointment);
    }

    public IActionResult FollowUpAppointment()
    {
        return View("FollowUpAppointment");
    }
    public IActionResult FollowUpAppointment(FollowUpAppointmentModel model)
    {
        model.DoctorId = GetUserId();
        if (!ModelState.IsValid) return RedirectToAction("FollowUpAppointment");
        var followUpAppointment = _doctorService.followUpAppointment(model);
       // return View("FollowUpAppointment",followUpAppointment);
       // return View("FollowUpAppointment",followUpAppointment);

       return RedirectToAction("DashBoard");
    }

    public IActionResult DashBoard()
    {
        try
        {
            // Get the logged-in user's (doctor's) ID
            var id = GetUserId();  // Assuming GetUserId() fetches the currently logged-in doctor's ID

            // Fetch the doctor details using LINQ
            Doctor doctor =  _context.Doctors
                .FirstOrDefault(d => d.Id == id);

            if (doctor == null)
            {
                return NotFound("Doctor not found");
            }

            // Fetch related data using LINQ
            var appointments =  _context.PatientDoctorAppointments
                .Where(ap => ap.DoctorId == id)
                .Include(ap => ap.Patient)  // Assuming Appointment has a related Patient entity
                .ToList();

            var medicalRecords =  _context.MedicalRecords
                .Where(mr => mr.DoctorId == id)
                .Include(mr => mr.Patient)  // Assuming MedicalRecord has a related Patient entity
                .ToList();

            var department =  _context.Departments
                .FirstOrDefault(d => d.Id == doctor.DepartmentId);  // Fetch department details for the doctor

            // Create the DoctorDashBoardModel object
            var doctorDashboard = new DoctorDashBoardModel()
            {
                DoctorName = $"{doctor.FirstName} {doctor.LastName}",
                Specialization = doctor.Specialization,
                DepartmentName = department?.Name,  // Assuming the Department has a Name property
                WorkingHours = doctor.WorkingHours,
                Appointments = appointments,
                MedicalRecords = medicalRecords
            };

            // Pass the DoctorDashBoardModel object to the view
            return View("DashBoard", doctorDashboard);
        }
        catch (Exception ex)
        {
            // Log exception (optional)
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
    }
}