using System.Security.Claims;
using hospital.management.system.BLL.Models.Doctors;
using hospital.management.system.BLL.Services;
using hospital.management.system.BLL.Services.IServices;
using hospital.management.system.DAL;
using hospital.management.system.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace hospital.management.system.Web.Controllers;

public class DoctorController : Controller
{
    private readonly IDoctorService _doctorService;
    private readonly UserManager<ApplicationUser> _userManager;

    public DoctorController(IUnitOfWork unitOfWork, IDoctorService doctorService, UserManager<ApplicationUser> userManager)
    {
        this._doctorService = doctorService;
        _userManager = userManager;

    }

    private Guid GetUserId()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        Guid id = Guid.Parse(userId);
        return id;
    }
    
    public IActionResult IdToPendingAppointment()
    {
        var pendingAppointment = _doctorService.GetPendingAppointments(GetUserId());
        return View(pendingAppointment);
    }
    
    public IActionResult IdToDailyAppointment()
    {
        var dailyAppointment = _doctorService.GetDailyAppointments(GetUserId());
        return View(dailyAppointment);
    }
    
    public IActionResult IdToUpcomingAppointment()
    {
        var upcomingAppointment = _doctorService.GetUpcomingAppointments(GetUserId());
        return View(upcomingAppointment);
    }
    
    public IActionResult IdToApproveAppointment()
    {
        var approveAppointment = _doctorService.ApproveNextAppointment(GetUserId());
        return View(approveAppointment);
    }
    
    public IActionResult IdToPostponeAppointment()
    {
        var postponeAppointment = _doctorService.PostponingAppointment(GetUserId());
        return View(postponeAppointment);
    }
    
    public IActionResult CancelingAppointment(Guid patientId)
    {
        var model = new DoctorCancelingAppointmentModel
        {
            LoggedDoctorId = GetUserId(),
            SelectedPatientId = patientId,             
        };
        var cancelAppointment = _doctorService.CancelingAppointment(model);
        return View(cancelAppointment);
    }
    
    public IActionResult FollowUpAppointment(FollowUpAppointmentModel model)
    {
        model.DoctorId = GetUserId();
        var followUpAppointment = _doctorService.FollowUpAppointment(model);
        return View(followUpAppointment);
    }
}