using System.Security.Claims;
using hospital.management.system.BLL.Models.Doctors;
using hospital.management.system.BLL.Services.IServices;
using hospital.management.system.DAL;
using hospital.management.system.DAL.Persistence;
using hospital.management.system.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace hospital.management.system.Web.Controllers;

public class DoctorController2 : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ApplicationDbContext _context;
    private readonly IDoctorService _doctorService;
    private readonly IAdminService _adminService;
    private readonly UserManager<ApplicationUser> _userManager;

    public DoctorController2(
        IUnitOfWork unitOfWork,
        ApplicationDbContext context,
        UserManager<ApplicationUser> userManager,
        IDoctorService doctorService,
        IAdminService adminService)
    {
        _unitOfWork = unitOfWork;
        _context = context;
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _doctorService = doctorService;
        _adminService = adminService;
    }

    private Guid GetDoctorId()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var id = Guid.Parse(userId);
        // return id;
        var DoctorId = _context.Doctors.FirstOrDefault(e => e.UserId == id);
        return DoctorId.Id;
    }

    public IActionResult IdToPendingAppointment()
    {
        var pendingAppointment = _doctorService.getPendingAppointments(GetDoctorId());
        return View("IdToPendingAppointment", pendingAppointment);
    }

    public IActionResult IdToDailyAppointment()
    {
        var dailyAppointment = _doctorService.getDailyAppointments(GetDoctorId());
        return View("IdToDailyAppointment", dailyAppointment);
    }

    public IActionResult IdToUpcomingAppointment()
    {
        var upcomingAppointment = _doctorService.getUpcomingAppointments(GetDoctorId());
        return View("IdToUpcomingAppointment", upcomingAppointment);
    }

    public IActionResult IdToApproveAppointment()
    {
        var approveAppointment = _doctorService.approveNextAppointment(GetDoctorId());
        return View("IdToApproveAppointment", approveAppointment);
    }

    // ==> what is the view should do ?!!!!!!!!!!!!!!!!!!
    public IActionResult IdToPostponeAppointment()
    {
        var postponeAppointment = _doctorService.postponingAppointment(GetDoctorId());
        return View(postponeAppointment);
    }

    public IActionResult CancelingAppointment(Guid patientId)
    {
        var model = new DoctorCancelingAppointmentModel
        {
            LoggedDoctorId = GetDoctorId(),
            SelectedPatientId = patientId
        };
        var cancelAppointment = _doctorService.cancelingAppointment(model);
        return View("CancelingAppointment", cancelAppointment);
    }

    public IActionResult FollowUpAppointment()
    {
        return View("FollowUpAppointment");
    }

    public IActionResult FollowUpAppointment(FollowUpAppointmentModel model)
    {
        model.DoctorId = GetDoctorId();
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
            var id = GetDoctorId(); // Assuming GetUserId() fetches the currently logged-in doctor's ID

            // Fetch the doctor details using LINQ
            var doctor = _context.Doctors
                .FirstOrDefault(d => d.Id == id);

            if (doctor == null) return NotFound("Doctor not found");

            // Fetch related data using LINQ
            var appointments = _context.PatientDoctorAppointments
                .Where(ap => ap.DoctorId == id)
                .Include(ap => ap.Patient) // Assuming Appointment has a related Patient entity
                .ToList();

            var medicalRecords = _context.MedicalRecords
                .Where(mr => mr.DoctorId == id)
                .Include(mr => mr.Patient) // Assuming MedicalRecord has a related Patient entity
                .ToList();

            var department = _context.Departments
                .FirstOrDefault(d => d.Id == doctor.DepartmentId); // Fetch department details for the doctor

            // Create the DoctorDashBoardModel object
            var doctorDashboard = new DoctorDashBoardModel()
            {
                DoctorName = $"{doctor.FirstName} {doctor.LastName}",
                Specialization = doctor.Specialization,
                DepartmentName = department?.Name, // Assuming the Department has a Name property
                WorkingHours = doctor.WorkingHours,
                Appointments = appointments,
                MedicalRecords = medicalRecords
            };

            // Pass the DoctorDashBoardModel object to the view
            return View("DashBoard", doctorDashboard);
        }
        catch (Exception ex)
        {
            // Log exception (optional).....
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Profile()
    {
        var res = await _doctorService.GetDoctorByIdAsync(GetDoctorId());
        var user = await _userManager.GetUserAsync(User);
        if (user == null) return View("Error");
        return View(new DoctorProfileModel()
        {
            Id = user.Id,
            //Address = user.Address,
            //DateOfBirth = user.DateOfbirth,
            Email = user.Email,
            IsEmailConfirmed = user.EmailConfirmed,
            IsTwoFactorEnabled = user.TwoFactorEnabled,
            NationalIdOrPassport = user.SSN,
            PhoneNumber = user.PhoneNumber,
            UserName = user.UserName,
            FirstName = res.FirstName,
            LastName = res.LastName,
            Gender = user.Gender
        });
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid? Id = null)
    {
        if (Id == null) Id = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var Doctor = await _doctorService.GetDoctorByIdAsync(GetDoctorId());

        var user = await _userManager.FindByIdAsync(Id.ToString());
        if (user == null) return View("Error");
        var model = new DoctorEditModel()
        {
            Id = Id,
            FirstName = Doctor.FirstName,
            LastName = Doctor.LastName,
            UserName = user.UserName
        };
        if (model.Id.ToString() == User.FindFirstValue(ClaimTypes.NameIdentifier))
            return View(model);

        return View("Error");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(DoctorEditModel model)
    {
        if (!ModelState.IsValid) return View(model);
        var res = await _doctorService.EditDoctorAsync(model);
        if (res == 1)
            return User.IsInRole(SD.Doctor) ? RedirectToAction("Index", "Profile") : RedirectToAction("Profile");

        return View("Error");
    }
}