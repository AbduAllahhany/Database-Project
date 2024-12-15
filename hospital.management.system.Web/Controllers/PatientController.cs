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
using Microsoft.EntityFrameworkCore;

namespace hospital.management.system.Web.Controllers;

[Authorize(Roles = SD.Patient)]
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

    // =>>> view will be changed 
    // GET
    public IActionResult Index()
    {
        List<Patient> patients = _patientService.GetAllPetient();

        // Check if patients data is null or empty
        // if (patients == null || !patients.Any())  return Content("No patient Exist"); // Or show a message indicating no patients found


        // Pass the list of patients to the view
        return View("Index", patients);
    }
// method dash board 

// ==> modify links's paramter 
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


        // Create a PatientDashboard object
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

        // Pass the PatientDashboard object to the view
        return View("DashBoard", patientDashboard);

    }

    public IActionResult GetAllPetient()
    {

        List<Patient> patients = _patientService.GetAllPetient();
        return View("GetAllPetient", patients);
    }

    [HttpGet] // getall 
    public IActionResult GetPatientAppointments()
    {
        // add role

        List<PatientAppointment> PatientDoctorAppoinment = _patientService.GetPatientAppointments(GetPatientId());

        // if(PatientDoctorAppoinment == null) return RedirectToAction("Index");
        return View("GetPatientAppointments", PatientDoctorAppoinment);
    }

// ==>> view will be changed 
    public IActionResult GetPatientBills()
    {
        List<PatientBill> PatientBills = _patientService.GetPatientBills(GetPatientId());

        // if (PatientBills == null) return RedirectToAction("Index");
        return View("GetPatientBills", PatientBills);
    }

    public IActionResult GetPatientMedicalRecord()
    {
        List<PatientMedicalRecord> PatientMedicalRecord = _patientService.GetPatientMedicalRecord(GetPatientId());

        //if (PatientMedicalRecord == null) return RedirectToAction("Index");
        return View("GetPatientMedicalRecord", PatientMedicalRecord);
    }

    public IActionResult GetPatientVisits(Guid patientId)
    {
        List<PatientVisit> PatientVisits = _patientService.GetPatientVisits(patientId);

        //if (PatientVisits == null) return RedirectToAction("Index");
        return View("GetPatientVisits", PatientVisits);
    }

    [HttpGet]
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
            DateOfBirth = patient.Birthdate,
            UserName = user.UserName,
        };
        if (model.Id.ToString() == User.FindFirstValue(ClaimTypes.NameIdentifier))
            return View(model);
        return View("Error");
    }

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

    public IActionResult GetRoomStatus(Guid patientId)
    {
        PatientRoom RoomStatus = _patientService.GetRoomStatus(patientId);

        //if (RoomStatus == null) return RedirectToAction("Index");
        return View("GetRoomStatus", RoomStatus);
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
            Birthdate = res.Birthdate,
            ChronicDiseases = res.ChronicDiseases,
            UserName = user.UserName,
            Allergies = res.Allergies,
            Name = res.FirstName + " " + res.LastName,
        });

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
}