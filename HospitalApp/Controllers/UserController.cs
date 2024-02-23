using HospitalApp.Core.Application.Interfaces.Services; 
using HospitalApp.Core.Application.ViewModels.User;
using Microsoft.AspNetCore.Mvc;
using HospitalApp.Core.Application.Helpers;
using HospitalApp.Middlewares;

namespace HospitalApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _service;
        private readonly ValidateUserSession _validateUserSession;

        public UserController(IUserService service, ValidateUserSession validateUserSession)
        {
            _service = service;
            _validateUserSession = validateUserSession;
        }

        //login 
        public IActionResult Index()
        {
            if (_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel loginVm)
        {
            if (!ModelState.IsValid)
            {
                return View(loginVm);
            }

            UserViewModel userVm = await _service.Login(loginVm);
 
            if (userVm != null)
            {
                HttpContext.Session.Set<UserViewModel>("user", userVm);
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            else
            {
                ModelState.AddModelError("userValidation", "Datos de inicio de sesión incorrectos");
            }

            return View(loginVm);
        }

        //log out
        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("user");
            return RedirectToRoute(new { controller = "User", action = "Index" });
        }

        //register
        public IActionResult Register()
        {
            if (_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            return View(new SaveUserViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Register(SaveUserViewModel userViewModel)
        {
            if (_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            if (!ModelState.IsValid)
            {
                return View(userViewModel);
            }
            await _service.Add(userViewModel);
            return RedirectToRoute(new { controller = "User", action = "Index" });
        }

        


    }
}

