using hospital.management.system.BLL.Models.Patient;
using hospital.management.system.BLL.Services;
using hospital.management.system.BLL.Services.IServices;
using hospital.management.system.DAL;
using hospital.management.system.DAL.Persistence;
using hospital.management.system.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using hospital.management.system.Web.Models;

namespace hospital.management.system.Web.Controllers;

public class PatientController : Controller
{
    
    private readonly IUnitOfWork _unitOfWork;
    private readonly ApplicationDbContext _context;
   // private readonly UserManager<Patient> _userManager;
    private readonly IPatientService _patientService;

    public PatientController(
        IUnitOfWork unitOfWork, 
        ApplicationDbContext context, 
        //UserManager<Patient> userManager, 
        IPatientService patientService)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _context = context ?? throw new ArgumentNullException(nameof(context));
       // _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _patientService = patientService ?? throw new ArgumentNullException(nameof(patientService));
    }


    
    // GET
    public IActionResult Index()
    {
        
        return View("Index");
    }
// method dash board 

    public IActionResult GetAllPetient()
    {
        
        List<Patient> patients = _patientService.GetAllPetient();
        return View("GetAllPetient",patients );
    }
    
    [HttpGet] // getall 
    public IActionResult GetPatientAppointments(Guid patientId )
    {
       // add role
       // if(patientId == null ) patientId = //funtion //Guid.NewGuid();
       List<PatientAppointment> PatientDoctorAppoinment = _patientService.GetPatientAppointments(patientId);
       
        if(PatientDoctorAppoinment == null) return RedirectToAction("Index");
         return View("GetPatientAppointments",PatientDoctorAppoinment);
    }

    public IActionResult GetPatientBills(Guid patientId)
    {
        List<PatientBill> PatientBills = _patientService.GetPatientBills(patientId);
        
        if (PatientBills == null) return RedirectToAction("Index");
        return View("GetPatientBills",PatientBills);
    }

    public IActionResult GetPatientMedicalRecord(Guid patientId)
    {
        List<PatientMedicalRecord> PatientMedicalRecord = _patientService.GetPatientMedicalRecord(patientId);
        
        if (PatientMedicalRecord == null) return RedirectToAction("Index");
        return View("PatientMedicalRecord",PatientMedicalRecord);
    }

    public IActionResult GetPatientVisits(Guid patientId)
    {
        List<PatientVisit> PatientVisits = _patientService.GetPatientVisits(patientId);
        
        if (PatientVisits == null) return RedirectToAction("Index");
            return View("PatientVisits",PatientVisits);
    }

    public IActionResult GetRoomStatus(Guid patientId)
    {
        PatientRoom RoomStatus = _patientService.GetRoomStatus(patientId);
        
        if (RoomStatus == null) return RedirectToAction("Index");
        return View("RoomStatus",RoomStatus);
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