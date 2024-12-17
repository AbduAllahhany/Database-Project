using System.Diagnostics;
using hospital.management.system.DAL;
using Microsoft.AspNetCore.Mvc;
using hospital.management.system.Web.Models;

namespace hospital.management.system.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        if (User.IsInRole(SD.Admin))
            return RedirectToAction("Dashboard", "Admin");
        else if (User.IsInRole(SD.Patient))
            return RedirectToAction("Dashboard", "Patient");
        else if (User.IsInRole(SD.Doctor))
            return RedirectToAction("DashBoard", "Doctor");
        else if (User.IsInRole(SD.Nurse) || User.IsInRole(SD.OfficeBoy) || User.IsInRole(SD.Intern))
            return RedirectToAction("Dashboard", "Staff");
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View();
    }
}