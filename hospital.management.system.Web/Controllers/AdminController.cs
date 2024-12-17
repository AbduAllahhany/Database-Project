using System.Security.Claims;
using hospital.management.system.BLL.Services.IServices;
using hospital.management.system.DAL;
using hospital.management.system.DAL.Persistence;
using hospital.management.system.Models.Entities;
using hospital.management.system.Web.Models.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace hospital.management.system.Web.Controllers;

[Authorize(Roles = SD.Admin)]
public class AdminController : Controller
{
    private readonly IAdminService _adminService;
    private readonly IPatientService _patientService;
    private readonly IDoctorService _doctorService;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IStaffService _staffService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ApplicationDbContext _context;

    public AdminController(IUnitOfWork unitOfWork,
        IAdminService adminService,
        IPatientService patientService,
        IDoctorService doctorService,
        UserManager<ApplicationUser> userManager,
        IStaffService staffService)
    {
        _adminService = adminService;
        _patientService = patientService;
        _doctorService = doctorService;
        _userManager = userManager;
        _unitOfWork = unitOfWork;
        _staffService = staffService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var model = await _adminService.GetAllAdminsAsync();
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Dashboard()
    {
        var upcomingAppointments = await _adminService.GetUpcomingAppointmentAsync();
        var model = new AdminDashboardModel
        {
            upcomingAppointments = upcomingAppointments,
            AppointmentsCount = await _adminService.GetAppointmentCountAsync(),
            StaffCount = await _staffService.GetStaffCountAsync(),
            PatientsCount = await _patientService.GetPatientsCountAsync(),
            DoctorsCount = await _doctorService.GetDoctorsCountAsync()
        };
        return View(model);
    }
    
    [HttpGet]
    public IActionResult PatientActions(Guid Id)
    {
        var model = _adminService.GetPatientById(Id);
        return View("PatientActions", model); // Pass patient object to the view
        // return View();
    }

    [HttpGet]
    public IActionResult CreatePatient()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(AdminCreateModel model)
    {
        if (!ModelState.IsValid) return View(model);
        var res = await _adminService.AdminCreateAsync(model);
        return res == 1 ? RedirectToAction(nameof(Index)) : View("Error");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreatePatient(PatientCreateModel model)
    {
        if (!ModelState.IsValid) return View(model);
        var res = await _adminService.CreatePatientAsync(model);
        return res == 1 ? RedirectToAction("Patients", "Admin") : View("Error");
    }

    [HttpGet]
    public IActionResult CreateDoctor()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateDoctor(DoctorCreateModel model)
    {
        // TimeSpan? timeDifference = model.EndSchedule - model.StartSchedule;
        // if (timeDifference)
        // {
        //     ModelState.AddModelError("EndSchedule", "The End Schedule must be at least 5 hours greater than the Start Schedule.");
        //     return View(model);
        // }
        if (!ModelState.IsValid) return View(model);
        var deptid = SD.Departments[model.DepartmentName];
        model.DepartmentId = deptid;
        var res = await _adminService.CreateDoctorAsync(model);
        return res == 1 ? RedirectToAction("Doctors", "Admin") : View("Error");
    }

    [HttpGet]
    public IActionResult CreateBill(Guid? PatientId = null)
    {
        if (PatientId == null) return View("Error");
        var model = new BillCreateModel
        {
            PatientId = PatientId
        };
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateBill(BillCreateModel model)
    {
        if (!ModelState.IsValid) return View(model);
        var res = await _adminService.CreateBillAsync(model);
        return res == 1 ? RedirectToAction("Patients", "Admin") : View("Error");
    }
    
    [HttpGet]
    public IActionResult CreateEmergencyContact(Guid? PatientId = null)
    {
        if (PatientId == null) return View("Error");
        var model = new EmergencyContactCreateModel
        {
            PatientId = PatientId
        };
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateEmergencyContact(EmergencyContactCreateModel model)
    {
        if (!ModelState.IsValid) return View(model);
        var res = await _adminService.CreateEmergencyContactAsync(model);
        return res == 1 ? RedirectToAction("Patients", "Admin") : View("Error");
    }

    [HttpGet]
    public IActionResult CreateVisit(Guid? PatientId = null)
    {
        if (PatientId == null) return View("Error");
        var model = new VisitCreateModel
        {
            PatientId = PatientId
        };
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateVisit(VisitCreateModel model)
    {
        if (!ModelState.IsValid) return View(model);
        var res = await _adminService.CreateVisitAsync(model);
        return res == 1 ? RedirectToAction("Patients", "Admin") : View("Error");
    }

    public IActionResult CreateInsurance(Guid? PatientId = null)
    {
        if (PatientId == null) return View("Error");
        var model = new InsuranceCreateModel
        {
            PatientId = PatientId
        };
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateInsurance(InsuranceCreateModel model)
    {
        if (!ModelState.IsValid) return View(model);
        var res = await _adminService.CreateInsuranceAsync(model);
        return res == 1 ? RedirectToAction("Patients", "Admin") : View("Error");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid? Id = null)
    {
        string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (Id == null) Id = Guid.Parse(userId);
        var user = await _userManager.FindByIdAsync(Id.Value.ToString());
        if (user == null) return View("Error");
        var model = new AdminEditModel()
        {
            UserId = Id,
            SSN = user.SSN,
            Email = user.Email,
            UserName = user.UserName,
            PhoneNumber = user.PhoneNumber
        };
        return View(model);
    }
    
    public async Task<IActionResult> CreateAppointment()
    {
        var patients = await _adminService.GetAllPatientsAsync();
        var doctors = await _adminService.GetAllDoctorsAsync();
        
        var model = new AppointmentModel
        {
            PatientUsernameId = patients,
            DoctorUsernameId = doctors,
        };
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateAppointment(AppointmentModel model)
    {
        if (!ModelState.IsValid) return View(model);
        await _adminService.CreateAppointmentAsync(new AppointmentAddModel()
        {
            PatientUserId = model.PatientUserId,
            DoctorUserId = model.DoctorUserId,
            Date = model.Date,
            Time = model.Time,
            Reason = model.Reason,
            Status = SD.Pending
        });
        return RedirectToAction("Appointments", "Admin");
    }

    
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(AdminEditModel model)
    {
        if (model == null) return View("Error");
        if (!ModelState.IsValid) return View(model);
        var user = await _userManager.FindByIdAsync(model.UserId.Value.ToString());
        if (user == null) return View("Error");

        user.UserName = model.UserName;
        user.PhoneNumber = model.PhoneNumber;
        user.Email = model.Email;
        user.NormalizedEmail = model.Email.ToUpper();
        user.NormalizedUserName = model.UserName.ToUpper();
        user.SSN = model.SSN;

        await _userManager.UpdateAsync(user);
        await _unitOfWork.CompleteAsync();
        if (model.UserId.Value.ToString() == User.FindFirstValue(ClaimTypes.Name))
            return RedirectToAction("Profile", "Admin");
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Profile()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null) return View("Error");
        return View(new AdminProfileModel()
        {
            UserId = user.Id,
            //Address = user.Address,
            //DateOfBirth = user.DateOfbirth,
            Email = user.Email,
            IsEmailConfirmed = user.EmailConfirmed,
            IsTwoFactorEnabled = user.TwoFactorEnabled,
            NationalIdOrPassport = user.SSN,
            PhoneNumber = user.PhoneNumber,
            UserName = user.UserName,
            Gender = user.Gender
        });
    }

    //patient List --> get Available room --> (button) choose room --> choose date(Create Admission) --> return to Patient list
    [HttpGet]
    // performance issue
    public async Task<IActionResult> ChooseRoom(Guid? Id = null)
    {
        if (Id == null) return View("Error");
        var rooms = await _adminService.GetAvailableRoomsAsync();
        var model = new ChooseRoomModel
        {
            PatientId = Id,
            AvailableRooms = rooms
        };
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    //to be solved later 
    public async Task<IActionResult> ChooseRoom(ChooseRoomModel model)
    {
        if (!ModelState.IsValid) return View(model);
        if (model.StartDate > model.EndDate)
        {
            ModelState.AddModelError("date", "Start date cannot be greater than the end date");
            model.AvailableRooms = await _adminService.GetAvailableRoomsAsync();
            return View(model);
        }

        var admissionModel = new AdmissionCreateModel
        {
            RoomId = model.RoomId,
            PatientId = model.PatientId,
            EndDate = model.EndDate,
            StartDate = model.StartDate
        };
        var res2 = await _adminService.CreateAdmissionAsync(admissionModel);
        var res1 = await _adminService.ConfirmRoomAsync(model.RoomId);

        return res1 == 1 && res2 == 1 ? RedirectToAction("Patients", "Admin") : View("Error");
    }
    [HttpGet]
    public async Task<IActionResult> Staff()
    {
        var res = await _staffService.GetAllTask();
        return View(res);
    }
    [HttpGet]
    public IActionResult CreateStaff()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateStaff(StaffCreateModel model)
    {
        if (!ModelState.IsValid) return View(model);
        var deptId = SD.Departments[model.DepartmentName];
        model.DepartmentId = deptId;
        var res = await _adminService.CreateStaffAsync(model);
        return res == 1 ? RedirectToAction("Staff", "Admin") : View("Error");
    }

    public async Task<IActionResult> Appointments()
    {
        var appointments = await _adminService.GetAppointmentsByUsernamesAsync();
        return appointments != null ? View(appointments) : View("Error");
    }

    [HttpGet]
    public IActionResult Patients()
    {
        IEnumerable<Patient> patients = _patientService.GetAllPetient();
        return View(patients);
    }

    [HttpGet]
    public async Task<IActionResult> EditPatient(Guid? Id = null)
    {
        if (Id == null) return View("Error");
        var user = await _patientService.GetUserByIdAsync(Id.Value);
        if (user == null) return View("Error");
        var temp = await _patientService.PatientProfileDataByIdAsync(Id);
        var model = new AdminEditPatientModel()
        {
            PatientId = Id.Value,
            PhoneNumber = user.PhoneNumber,
            Email = user.Email,
            UserId = user.Id,
            DateOfBirth = temp.DateOfBirth,
            ChronicDiseases = temp.ChronicDiseases,
            Address = temp.Address,
            Allergies = temp.Allergies,
            BloodGroup = temp.BloodGroup,
            FirstName = temp.FirstName,
            LastName = temp.LastName,
            UserName = user.UserName,
        };
        return View(model);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditPatient(AdminEditPatientModel model)
    {
        if (!ModelState.IsValid) return View(model);
        var res = await _adminService.AdminEditPatientAsync(model);
        return res == 1 ? RedirectToAction("Patients", "Admin") : View("Error");
    }
    [HttpGet]
    public async Task<IActionResult> EditDoctor(Guid? Id = null)
    {
        if (Id == null) return View("Error");
        var user = await _doctorService.GetUserByIdAsync(Id.Value);
        if (user == null) return View("Error");
        var temp = await _doctorService.DocotorProfileDataByIdAsync(Id.Value);
        var model = new AdminEditDoctorModel()
        {
            DoctorId = Id.Value,
            PhoneNumber = user.PhoneNumber,
            Email = user.Email,
            UserId = user.Id,
            FirstName = temp.FirstName,
            Salary = temp.Salary,
            LastName = temp.LastName,
            UserName = user.UserName,
            StartSchedule = temp.StartSchedule,
            EndSchedule = temp.EndSchedule
        };
        return View(model);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditDoctor(AdminEditDoctorModel model)
    {
        if (!ModelState.IsValid) return View(model);
        var res = await _adminService.AdminEditDoctorAsync(model);
        return res == 1 ? RedirectToAction("Doctors", "Admin") : View("Error");
    }

    [HttpGet]
    public async Task<IActionResult> EditStaff(Guid? Id = null)
    {
        if (Id == null) return View("Error");
        var staffUser = await _staffService.GetUserByIdAsync(Id);
        var staff = await _staffService.GetStaffByIdAsync(Id);
        var model = new AdminEditStaffModel()
        {
            StaffId = Id.Value,
            EndSchedule = staff.EndSchedule,
            StartSchedule = staff.StartSchedule,
            FirstName = staff.FirstName,
            LastName = staff.LastName,
            DayOfWork = staff.DayOfWork,
            PhoneNumber = staffUser.PhoneNumber,
            Email = staffUser.Email,
            UserId = staff.UserId,
            UserName = staffUser.UserName,

        };
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditStaff(AdminEditStaffModel model)
    {
        if (model.StaffId == null) return View("Error");
        var res = await _adminService.AdminEditStaffAsync(model);
        return res == 1 ? RedirectToAction("Staff", "Admin") : View("Error");
    }

    public IActionResult Doctors()
    {
        IEnumerable<Doctor> doctors = _doctorService.GetAllDoctors();
        return View(doctors);
    }
    
    public IActionResult DeleteStaff(Guid staffId)
    {
        var res = _adminService.DeleteStaff(staffId);
        return res == 1 ? RedirectToAction("Staff", "Admin") : View("Error");
    }

}