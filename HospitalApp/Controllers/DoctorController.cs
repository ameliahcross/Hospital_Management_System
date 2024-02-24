
using HospitalApp.Middlewares;
using HospitalApp.Core.Application.Interfaces.Services;
using HospitalApp.Core.Application.ViewModels.Doctor;
using Microsoft.AspNetCore.Mvc;
using HospitalApp.Core.Domain.Entities;

namespace HospitalApp.Controllers
{
    public class DoctorController : Controller
    {
        private readonly IDoctorService _service;
        private readonly ValidateUserSession _validateUserSession;

        public DoctorController(IDoctorService service, ValidateUserSession validateUserSession)
        {
            _service = service;
            _validateUserSession = validateUserSession;
        }

        public async Task<IActionResult> Index()
        {
            if (!_validateUserSession.HasUser() || _validateUserSession.GetUserRole() != UserRole.Administrador)
            {
                return RedirectToRoute(new { controller = "User", action = "Permission" });
            }

            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            var list = await _service.GetAllViewModel();
            return View(list);
        }

        public async Task<IActionResult> Create()
        {
            if (! _validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            return View("SaveDoctor", new SaveDoctorViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaveDoctorViewModel newDr)
        {
            if (! _validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            if (!ModelState.IsValid)
            {
                return View("SaveDoctor", newDr);
            }

            await _service.Add(newDr);
            return RedirectToRoute(new { controller = "Doctor", action = "Index" });
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (! _validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            var doctorViewModel = await _service.GetByIdSaveViewModel(id);

            return View("SaveDoctor", doctorViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SaveDoctorViewModel updatedDrViewModel)
        {
            if (! _validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            if (!ModelState.IsValid)
            {
                return View("SaveDoctor", updatedDrViewModel);
            }

            await _service.Update(updatedDrViewModel);
            return RedirectToRoute(new { controller = "Doctor", action = "Index" });
        }

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
            return RedirectToRoute(new { controller = "Doctor", action = "Index" });
        }

    }
}

