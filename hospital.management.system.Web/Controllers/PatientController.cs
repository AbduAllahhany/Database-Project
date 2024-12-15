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

public class PatientController : Controller
{
    
    private readonly IUnitOfWork _unitOfWork;
    private readonly ApplicationDbContext _context;
    private readonly IPatientService _patientService;

    public PatientController(
        IUnitOfWork unitOfWork, 
        ApplicationDbContext context, 
        IPatientService patientService)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _context = context ?? throw new ArgumentNullException(nameof(context));
       // _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _patientService = patientService ?? throw new ArgumentNullException(nameof(patientService));
    }


    private Guid GetUserId()
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
        
        var patientId = (_context.Patients.FirstOrDefault(e=>e.UserId==id));
        //var patientId =_context.Patients.FromSqlInterpolated($@"select * from Patients where UserId={id}").FirstOrDefault();
        if (patientId==null || patientId.Id==Guid.Empty)
        {
            throw new InvalidOperationException("Patient is not authenticated or the NameIdentifier claim is missing.");
        }
        return patientId.Id;
    }

    
// method dash board 

// ==> modify links's paramter 
    public IActionResult DashBoard()
    {

            var id = GetUserId();
            // Fetch patient details using LINQ
            var patient = _context.Patients
                .FirstOrDefault(p => p.Id == id);

            if (patient == null)
            {
                return NotFound("Patient not found");
            }

            // Fetch related data using LINQ
            var emergencyContacts = _context.EmergencyContacts
                .Where(ec => ec.PatientId == id)
                .ToList();

            var medicalRecords = _context.MedicalRecords
                .Where(mr => mr.PatientId == id)
                .ToList();

            var appointments = _context.PatientDoctorAppointments
                .Where(ap => ap.PatientId == id)
                .ToList();

            var bills = _context.Bills
                .Where(b => b.PatientId == id)
                .ToList();

            var visits = _context.Visits
                .Where(v => v.PatientId == id)
                .ToList();

            var admissions = _context.Admissions
                .Where(ad => ad.PatientId == id)
                .ToList();

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
        return View("GetAllPetient",patients );
    }
    
    [HttpGet] // getall 
    public IActionResult GetPatientAppointments( )
    {
       // add role
       
       List<PatientAppointment> PatientDoctorAppoinment = _patientService.GetPatientAppointments(GetUserId());
       
       // if(PatientDoctorAppoinment == null) return RedirectToAction("Index");
         return View("GetPatientAppointments",PatientDoctorAppoinment);
    }
    
// ==>> view will be changed 
    public IActionResult GetPatientBills()
    {
        List<PatientBill> PatientBills = _patientService.GetPatientBills(GetUserId());
        
       // if (PatientBills == null) return RedirectToAction("Index");
        return View("GetPatientBills",PatientBills);
    }

    public IActionResult GetPatientMedicalRecord()
    {
        List<PatientMedicalRecord> PatientMedicalRecord = _patientService.GetPatientMedicalRecord(GetUserId());
        
        //if (PatientMedicalRecord == null) return RedirectToAction("Index");
        return View("GetPatientMedicalRecord",PatientMedicalRecord);
    }

    public IActionResult GetPatientVisits(Guid patientId)
    {
        List<PatientVisit> PatientVisits = _patientService.GetPatientVisits(patientId);
        
        //if (PatientVisits == null) return RedirectToAction("Index");
            return View("GetPatientVisits",PatientVisits);
    }

    public IActionResult GetRoomStatus(Guid patientId)
    {
        PatientRoom RoomStatus = _patientService.GetRoomStatus(patientId);
        
        //if (RoomStatus == null) return RedirectToAction("Index");
        return View("GetRoomStatus",RoomStatus);
    }

    [Authorize]
    [Authorize(Roles = SD.Admin)]
    public IActionResult deletePatient(Guid patientId)
    {
        var res = _patientService.deletePatient(patientId);
        return res == 1 ? RedirectToAction("patients", "Admin") : View("Error");
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