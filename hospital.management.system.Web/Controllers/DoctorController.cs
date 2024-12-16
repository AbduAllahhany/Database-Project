using System.Security.Claims;
using hospital.management.system.BLL.Models.Doctors;
using hospital.management.system.BLL.Services.IServices;
using hospital.management.system.DAL;
using hospital.management.system.DAL.Persistence;
using hospital.management.system.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace hospital.management.system.Web.Controllers;

public class DoctorController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IDoctorService _doctorService;
    private readonly UserManager<ApplicationUser> _userManager;

    public DoctorController(
        IUnitOfWork unitOfWork,
        ApplicationDbContext context,
        UserManager<ApplicationUser> userManager,
        IDoctorService doctorService,
        IAdminService adminService)
    {
        _context = context;
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _doctorService = doctorService;
    }

    private Guid GetDoctorId()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var id = Guid.Parse(userId);
        // return id;
        var DoctorId = _context.Doctors.FirstOrDefault(e => e.UserId == id);
        return DoctorId.Id;
    }

    [HttpGet]
    // [Authorize(Roles = "Doctor")]
    public IActionResult Appointments()
    {
        // add role

        List<DoctorAppoinment> PatientDoctorAppoinment = _doctorService.GetDoctorAppointments(GetDoctorId());

        // if(PatientDoctorAppoinment == null) return RedirectToAction("Index");
        return View("Appointments", PatientDoctorAppoinment);
    }

    [HttpGet]
    //  [Authorize(Roles = SD.Doctor + "," + SD.Admin)]
    public IActionResult PendingAppointment()
    {

        var pendingAppointment = _doctorService.GetPendingAppointments(GetDoctorId());
        return View("PendingAppointment", pendingAppointment);
    }

    [HttpGet]
    //[Authorize(Roles = SD.Doctor)]
    public IActionResult DailyAppointment()
    {
        var dailyAppointment = _doctorService.GetDailyAppointments(GetDoctorId());
        return View("DailyAppointment", dailyAppointment);
    }

    [HttpGet]
    // [Authorize(Roles = SD.Doctor)]
    public IActionResult UpcomingAppointment()
    {
        var upcomingAppointment = _doctorService.GetUpcomingAppointments(GetDoctorId());
        return View("UpcomingAppointment", upcomingAppointment);
    }

    [HttpGet]
    //  [Authorize(Roles = SD.Doctor)]
    public IActionResult ApproveAppointment(Guid id)
    {

        var model = new DoctorCancelingAppointmentModel
        {
            LoggedDoctorId = GetDoctorId(),
            SelectedPatientId = id,
        };
        if (id == Guid.Empty || id == null) return View("Error");
        var approveAppointment = _doctorService.ApproveNextAppointment(model);
        return View("ApproveAppointment", approveAppointment);


    }

    // ==> what is the view should do ?!!!!!!!!!!!!!!!!!!
    // ===> this action doesn't work  
    [HttpGet]
    // [Authorize(Roles = SD.Doctor)]
    /*
    public IActionResult PostponeAppointment()
     {
         var postponeAppointment = _doctorService.PostponingAppointment(GetDoctorId());
         return View(postponeAppointment);
     }
 */
    [HttpGet]
    // [Authorize(Roles = SD.Doctor)]
    public IActionResult CancelingAppointment(Guid id)
    {
        var model = new DoctorCancelingAppointmentModel
        {
            LoggedDoctorId = GetDoctorId(),
            SelectedPatientId = id,
        };
        if (id == Guid.Empty || id == null) return View("Error");
        var cancelAppointment = _doctorService.CancelingAppointment(model);
        return View("CancelingAppointment", cancelAppointment);
    }

    [HttpGet]
    // [Authorize(Roles = SD.Doctor)]
    public IActionResult FollowUpAppointment()
    {
        return View("FollowUpAppointment");
    }

    [HttpGet]
    // //[Authorize(Roles = SD.Doctor)]
    /*
      public IActionResult SaveFollowUpAppointment(FollowUpAppointmentModel model)
      {

          model.DoctorId = GetDoctorId();
          if (!ModelState.IsValid) return RedirectToAction("FollowUpAppointment");
          model.PatientId= _context.Patients.FirstOrDefault(e=>e.UserId==model.PatientId).Id;
          var followUpAppointment = _doctorService.FollowUpAppointment(model);
          // return View("FollowUpAppointment",followUpAppointment);
          // return View("FollowUpAppointment",followUpAppointment);

          return RedirectToAction("DashBoard");
      }
  */
    [Authorize]
    //[Authorize(Roles = SD.Admin)]
    public IActionResult DeleteDoctor(Guid doctorId)
    {
        var res = _doctorService.DeleteDoctor(doctorId);
        return res == 1 ? RedirectToAction("Doctors", "Admin") : View("Error");
    }


    [HttpGet]
    // [Authorize(Roles = SD.Admin)]
    public IActionResult DashBoard()
    {
        try
        {
            // Get the logged-in user's (doctor's) ID
            var id = GetDoctorId(); // Assuming GetUserId() fetches the currently logged-in doctor's ID

            // Fetch the doctor details using LINQ
            //   Doctor doctor = _context.Doctors.FirstOrDefault(d => d.Id == id);
            Doctor doctor = _context.Doctors.FromSqlInterpolated($@"select * from Doctor where Id ={id}")
                .FirstOrDefault();
            if (doctor == null)
            {
                return NotFound("Doctor not found");
            }

            // Fetch related data using LINQ
            var appointments = _context.PatientDoctorAppointments
                .Where(ap => ap.DoctorId == id)
                .Include(ap => ap.Patient) // Assuming Appointment has a related Patient entity
                .Take(3).ToList();

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
            // Log exception (optional)
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
    }

    [HttpGet]
    //[Authorize(Roles = SD.Admin)]
    public async Task<IActionResult> Profile()
    {
        var res = await _doctorService.DocotorProfileDataByIdAsync(GetDoctorId());
        if(res == null) return View("Error");
        var user = await _userManager.GetUserAsync(User);
        if (user == null) return View("Error");
        return View(new DoctorProfileModel()
        {
            Id = user.Id,
            Email = user.Email,
            IsEmailConfirmed = user.EmailConfirmed,
            IsTwoFactorEnabled = user.TwoFactorEnabled,
            NationalIdOrPassport = user.SSN,
            PhoneNumber = user.PhoneNumber,
            UserName = user.UserName,
            Name = res.FirstName + " " + res.LastName,
            Gender = user.Gender,
            DepartmentName = res.DepartmentName,
            Specialization = res.Specialization
        });
    }

    [HttpGet]
    //[Authorize(Roles = SD.Doctor)]
    public async Task<IActionResult> Edit(Guid? Id = null)
    {
        if (Id == null) Id = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var Doctor = await _doctorService.DocotorProfileDataByIdAsync(GetDoctorId());

        var user = await _userManager.FindByIdAsync(Id.ToString());
        if (user == null) return View("Error");
        var model = new DoctorEditModel()
        {
            UserId = Id.Value,
            FirstName = Doctor.FirstName,
            LastName = Doctor.LastName,
            UserName = user.UserName
        };
        if (model.UserId.ToString() == User.FindFirstValue(ClaimTypes.NameIdentifier))
            return View(model);

        return View("Error");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    // [Authorize(Roles = SD.Doctor)]
    public async Task<IActionResult> Edit(DoctorEditModel model)
    {
        if (!ModelState.IsValid) return View(model);
        var res = await _doctorService.EditDoctorAsync(model);
        if (res == 1)
          return RedirectToAction(nameof(Profile));

        return View("Error");
    }

    [HttpGet]
    //[Authorize(Roles = SD.Doctor)]

    [HttpGet]
    // [Authorize(Roles = SD.Doctor)]
    public IActionResult AddMedicalRecord()
    {
        return View("AddMedicalRecord");
    }

    public IActionResult SaveMedicalRecord(MedicalRecordModel model)
    {
        model.LoggedDoctorId = GetDoctorId();
        if (!ModelState.IsValid) return RedirectToAction("AddMedicalRecord");
        model.SelectedPatientId = _context.Patients.FirstOrDefault(e => e.UserId == model.SelectedPatientId).Id;
        var SaveChanges = _doctorService.CreateMedicalRecord(model);
        if (SaveChanges > 0) return RedirectToAction("DashBoard");
        else return RedirectToAction("AddMedicalRecord");

    }

    [HttpPost]
    //[Authorize(Roles = SD.Doctor)]
    public IActionResult SaveFollowUpAppointment(FollowUpAppointmentModel model)
    {
        model.DoctorId = GetDoctorId();
        if (!ModelState.IsValid) return RedirectToAction("FollowUpAppointment");
        model.PatientId = _context.Patients.FirstOrDefault(e => e.UserId == model.PatientId).Id;
        var followUpAppointment = _doctorService.FollowUpAppointment(model);
        // return View("FollowUpAppointment",followUpAppointment);
        // return View("FollowUpAppointment",followUpAppointment);

        return RedirectToAction("DashBoard");
    }

    public IActionResult DoctorSummary()
    {
        Guid doctorId = GetDoctorId();
        DoctorMonthlyAppointmentSummary
            summary = _doctorService.GetMonthlyAppointmentSummary(doctorId).FirstOrDefault();
        return View("DoctorSummary", summary);
    }

    public IActionResult ViewMedicalRecords()
    {
        Guid doctorId = GetDoctorId();
        List<MedicalRecord> medicalrecord = _context.MedicalRecords
            .FromSqlInterpolated($@"select * from Medical_Record where doctorId = {doctorId}").ToList();
        medicalrecord = _context.MedicalRecords
            .Where(mr => mr.DoctorId == doctorId)
            .Include(mr => mr.Patient) // Assuming MedicalRecord has a related Patient entity
            .ToList();
        return View("ViewMedicalRecords", medicalrecord);
    }

}