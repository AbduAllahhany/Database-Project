using hospital.management.system.BLL.Models.Patient;
using hospital.management.system.BLL.Services;
using hospital.management.system.BLL.Services.IServices;
using hospital.management.system.DAL;
using hospital.management.system.DAL.Persistence;
using hospital.management.system.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using hospital.management.system.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Session;
using Microsoft.EntityFrameworkCore;

namespace hospital.management.system.Web.Controllers;


public class PatientController : Controller
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly ApplicationDbContext _context;
    private readonly IPatientService _patientService;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IAdminService _adminService;

    public PatientController(
        IUnitOfWork unitOfWork,
        ApplicationDbContext context,
        IPatientService patientService,
        UserManager<ApplicationUser> userManager,
        IAdminService adminService)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _patientService = patientService ?? throw new ArgumentNullException(nameof(patientService));
        _userManager = userManager;
        _adminService = adminService;
    }
    
    private Guid GetPatientId()
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

        //var patientId = (_context.Patients.FirstOrDefault(e=>e.UserId==id));
        var patientId = _context.Patients.FromSqlInterpolated($@"select * from Patient where UserId={id}")
            .FirstOrDefault();
        if (patientId == null || patientId.Id == Guid.Empty)
        {
            throw new InvalidOperationException("Patient is not authenticated or the NameIdentifier claim is missing.");
        }

        return patientId.Id;
    }

    public IActionResult Index()
    {
        List<Patient> patients = _patientService.GetAllPetient();

        // Check if patients data is null or empty
        // if (patients == null || !patients.Any())  return Content("No patient Exist"); // Or show a message indicating no patients found


        // Pass the list of patients to the view
        return View("Index", patients);
    }

// ==> modify links's paramter 
    [Authorize(Roles = SD.Patient)]
    public IActionResult DashBoard()
    {
        var id = GetPatientId();
        // Fetch patient details using LINQ
        var patient = _context.Patients.FromSqlInterpolated($@"select * from patient where id={id}").FirstOrDefault();
        if (patient == null)
        {
            return NotFound("Patient not found");
        }

        var emergencyContacts = _context.EmergencyContacts
            .FromSqlInterpolated($@"select * from Emergency_Contact where PatientId={id}").ToList();

        var medicalRecords = _context.MedicalRecords
            .FromSqlInterpolated($@"select * from Medical_Record where PatientId={id}").ToList();

        var appointments = _context.PatientDoctorAppointments
            .FromSqlInterpolated($@"select * from Patient_Doctor_Appointment where PatientId={id}").ToList();
        var bills = _context.Bills
            .FromSqlInterpolated($@"select * from Bill where PatientId = {id}").ToList();
        var visits = _context.Visits
            .FromSqlInterpolated($@"select * from Visit where PatientId = {id}").ToList();
        var admissions = _context.Admissions
            .FromSqlInterpolated($@"select * from Admission where PatientId = {id}").ToList();
        var patientDashboard = new PatientDashBoardModel()
        {
            Patient = patient,
            EmergencyContacts = emergencyContacts,
            MedicalRecords = medicalRecords,
            Appointments = appointments,
            Bills = bills,
            Visits = visits,
            Admissions = admissions
        };
        return View("DashBoard", patientDashboard);
    }

    [HttpGet]
    [Authorize(Roles = SD.Admin+ "," +SD.Patient)]
    public IActionResult PatientAppointments(Guid? Id = null)
    {
        if (Id == null)
            Id = GetPatientId();
        List<PatientAppointment> PatientDoctorAppoinment = _patientService.GetPatientAppointments(Id.Value);
        // if(PatientDoctorAppoinment == null) return RedirectToAction("Index");
        return View("PatientAppointments", PatientDoctorAppoinment);
    }

// ==>> view will be changed 
    [HttpGet]
    [Authorize(Roles = SD.Admin + "," + SD.Patient)]
    public IActionResult PatientBills(Guid? Id = null)
    {
        if (Id == null)
            Id = GetPatientId();
        List<PatientBill> PatientBills = _patientService.GetPatientBills(Id.Value);
        // if (PatientBills == null) return RedirectToAction("Index");
        return View("PatientBills", PatientBills);
    }

    [HttpGet]
    [Authorize(Roles = SD.Admin + "," + SD.Patient)]
    public IActionResult PatientMedicalRecord(Guid? Id = null)
    {
        if (Id == null)
            Id = GetPatientId();
        List<PatientMedicalRecord> PatientMedicalRecord = _patientService.GetPatientMedicalRecord(Id.Value);
        //if (PatientMedicalRecord == null) return RedirectToAction("Index");
        return View("PatientMedicalRecord", PatientMedicalRecord);
    }

    [HttpGet]
    [Authorize(Roles = SD.Admin+ "," + SD.Patient)]
    public IActionResult PatientVisits(Guid? Id = null)
    {
        if (Id == null)
            Id = GetPatientId();
        List<PatientVisit> PatientVisits = _patientService.GetPatientVisits(Id.Value);

        //if (PatientVisits == null) return RedirectToAction("Index");
        return View("PatientVisits", PatientVisits);
    }

    [HttpGet]
    [Authorize(Roles = SD.Patient)]
    public async Task<IActionResult> Edit(Guid? Id = null)
    {
        if (Id == null)
        {
            Id = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        }

        var patient = await _patientService.GetPatientById(GetPatientId());

        var user = await _userManager.FindByIdAsync(Id.ToString());
        if (user == null) return View("Error");
        var model = new PatientEditModel()
        {
            Id = Id,
            FirstName = patient.FirstName,
            LastName = patient.LastName,
            PhoneNumber = user.PhoneNumber,
            Address = patient.Address,
            DateOfBirth = patient.DateOfBirth,
            UserName = user.UserName,
        };
        if (model.Id.ToString() == User.FindFirstValue(ClaimTypes.NameIdentifier))
            return View(model);
        return View("Error");
    }

   // [Authorize(Roles = SD.Admin)]
    public IActionResult RoomStatus(Guid patientId)
    {
        PatientRoom RoomStatus = _patientService.GetRoomStatus(patientId);

        //if (RoomStatus == null) return RedirectToAction("Index");
        return View("RoomStatus", RoomStatus);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = SD.Patient)]
    public async Task<IActionResult> Edit(PatientEditModel model)
    {
        if (!ModelState.IsValid) return View(model);
        var res = await _patientService.EditPatientAsync(model);
        if (res == 1)
        {
            return User.IsInRole(SD.Patient) ? RedirectToAction("Index", "Patient") : RedirectToAction("Profile");
        }

        return View("Error");
    }

    [HttpGet]
    public async Task<IActionResult> Profile()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null) return View("Error");


        var res = await _patientService.GetPatientById(GetPatientId());
        if (res == null) return View("Error");
        return View(new PatientProfileModel()
        {
            Id = user.Id,
            Email = user.Email,
            IsEmailConfirmed = user.EmailConfirmed,
            IsTwoFactorEnabled = user.TwoFactorEnabled,
            NationalIdOrPassport = user.SSN,
            PhoneNumber = user.PhoneNumber,
            BloodGroup = res.BloodGroup,
            Gender = user.Gender,
            Address = res.Address,
            Birthdate = res.DateOfBirth,
            ChronicDiseases = res.ChronicDiseases,
            UserName = user.UserName,
            Allergies = res.Allergies,
            Name = res.FirstName + " " + res.LastName,
        });
    }

    [Authorize]
    [Authorize(Roles = SD.Admin)]
    public IActionResult DeletePatient(Guid patientId)
    {
        var res = _patientService.DeletePatient(patientId);
        return res >= 1 ? RedirectToAction("patients", "Admin") : View("Error");
    }
    public IActionResult Cancel(Guid id)
    {
        var res = _context.Database.ExecuteSqlRaw("delete from Patient_Doctor_Appointment where Id=(@p0)" , id);
        return View("Cancel",res);
    }
    // no view 
    // public IActionResult GetPatientInsurance(Guid patientId)
    // {
    //     ViewInsurance PatientInsurance = _patientService.GetViewInsurance(patientId);
    //     
    //     if (PatientInsurance == null) return RedirectToAction("Index");
    //     return View("PatientInsurance",PatientInsurance);
    // }
    //
    // public IActionResult GetPatientEmergancyContacts(Guid patientId)
    // {
    //     List<PatientEmergancyContact> PatientEmergancyContacts = _patientService.GetPatientEmergancyContacts(patientId);
    //     
    //     if (PatientEmergancyContacts == null) return RedirectToAction("Index");
    //     return View("PatientEmergancyContacts",PatientEmergancyContacts);
    // }
}