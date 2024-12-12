using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;
using hospital.management.system.BLL.Models.Admin;
using hospital.management.system.BLL.Services.IServices;
using hospital.management.system.DAL;
using hospital.management.system.DAL.Persistence;
using hospital.management.system.Models.Entities;
using hospital.management.system.Web.Models.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace hospital.management.system.Web.Controllers;

[Authorize(Roles = SD.Admin)]
public class AdminController : Controller
{
    private readonly IAdminService _adminService;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ApplicationDbContext _context;

    public AdminController(IUnitOfWork unitOfWork,
        IAdminService adminService,
        UserManager<ApplicationUser> userManager,
        ApplicationDbContext context)
    {
        _adminService = adminService;
        _userManager = userManager;
        _context = context;
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
            StaffCount = await _adminService.GetStaffCountAsync()
            // PatientsCount = , 
            //DoctorCount = ,
        };
        return View(model);
    }

    [HttpGet]
    public IActionResult CreatePatient()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreatePatient(PatientCreateModel model)
    {
        if (!ModelState.IsValid) return View(model);
        int res = await _adminService.CreatePatientAsync(model);
        return res == 1 ? RedirectToAction("PatientList", "Admin") : View("Error");
    }

    [HttpGet]
    public async Task<IActionResult> PatientList()
    {
        throw new NotImplementedException();
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
        if (!ModelState.IsValid) return View(model);
        Guid deptid = SD.departments[model.DepartmentName];
        model.DepartmentId = deptid;
        int res = await _adminService.CreateDoctorAsync(model);
        return res == 1 ? RedirectToAction("DoctorList", "Admin") : View("Error");
    }

    [HttpGet]
    public async Task<IActionResult> DoctorList()
    {
        throw new NotImplementedException();
    }

    [HttpGet]
    public async Task<IActionResult> AddBill(Guid PatientId)
    {
        return View(PatientId);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddBill(BillCreateModel model)
    {
        var res = await _adminService.CreateBillAsync(model);
        return res == 1 ? RedirectToAction("PatientList", "Admin") : View("Error");
    }

    public async Task<IActionResult> AddVisit()
    {
        throw new NotImplementedException();
    }

    public async Task<IActionResult> AddAdmission()
    {
        throw new NotImplementedException();

    }

    public async Task<IActionResult> EditPatient(Guid id)
    {
        throw new NotImplementedException();

    }

    public async Task<IActionResult> EditDoctor(Guid id)
    {
        throw new NotImplementedException();

    }
    public async Task<IActionResult> EditStaff(Guid id)
    {
        throw new NotImplementedException();

    }
    


    [HttpGet]
    public async Task<IActionResult> test()
    {
        await _adminService.Test();
        return View();
    }
}