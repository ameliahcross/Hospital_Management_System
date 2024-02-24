using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using HospitalApp.Models;
using HospitalApp.Middlewares;

namespace HospitalApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ValidateUserSession _validateUserSession;

    public HomeController(ILogger<HomeController> logger, ValidateUserSession validateUserSession)
    {
        _logger = logger;
        _validateUserSession = validateUserSession;
    }

    public IActionResult Index()
    {
        if (!_validateUserSession.HasUser())
        {
            return RedirectToRoute(new { controller = "User", action = "UserMaintenance" });
        }
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

