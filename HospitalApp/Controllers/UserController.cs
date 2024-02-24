using HospitalApp.Core.Application.Interfaces.Services; 
using HospitalApp.Core.Application.ViewModels.User;
using Microsoft.AspNetCore.Mvc;
using HospitalApp.Core.Application.Helpers;
using HospitalApp.Middlewares;
using HospitalApp.Core.Domain.Entities;

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
            if (_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

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

        // user maintenance
        public async Task<IActionResult> UserMaintenance()
        {
            if (!_validateUserSession.HasUser() || _validateUserSession.GetUserRole() != UserRole.Administrador)
            {
                return RedirectToRoute(new { controller = "User", action = "Permission" });
            }

            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            var userList = await _service.GetAllViewModel();
            return View(userList);
        }


        [HttpPost]
        public async Task<IActionResult> UserMaintenance(SaveUserViewModel userViewModel)
        {
            if (!_validateUserSession.HasUser())
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

        //log out
        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("user");
            return RedirectToRoute(new { controller = "User", action = "Index" });
        }

        //delete
 
        public async Task<IActionResult> Delete(int id)
        {
            if (! _validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            return View(await _service.GetByIdSaveViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            if (! _validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            await _service.Delete(id);
            return RedirectToRoute(new { controller = "User", action = "UserMaintenance" });
        }


        //edit 
        public async Task<IActionResult> Edit(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            var userViewModel = await _service.GetByIdSaveViewModel(id);

            return View("EditUser", userViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SaveUserViewModel updatedUserViewModel)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            var isUsernameValid = await _service.ValidateUsername(updatedUserViewModel.Username);
            if (!isUsernameValid)
            {
                ModelState.AddModelError("Username", "El nombre de usuario ya está en uso");
                return View("EditUser", updatedUserViewModel);
            }

            if (!ModelState.IsValid)
            {
                return View("EditUser", updatedUserViewModel);
            }

            await _service.Update(updatedUserViewModel);
            return RedirectToRoute(new { controller = "User", action = "UserMaintenance" });
        }

        public async Task<IActionResult> Permission()
        {
            return View();
        }


    }
}
