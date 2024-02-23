using HospitalApp.Core.Application.Interfaces.Services; 
using HospitalApp.Core.Application.ViewModels.User;
using Microsoft.AspNetCore.Mvc;
using HospitalApp.Core.Application.Helpers;

namespace HospitalApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        //login 
        public IActionResult Index()
        {   
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

        //register
        public IActionResult Register()
        {
            return View(new SaveUserViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Register(SaveUserViewModel userViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(userViewModel);
            }
            await _service.Add(userViewModel);
            return RedirectToRoute(new { controller = "User", action = "Index" });
        }



    }
}

