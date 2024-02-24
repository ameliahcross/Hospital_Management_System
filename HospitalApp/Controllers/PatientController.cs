using HospitalApp.Core.Application.Interfaces.Services;
using HospitalApp.Core.Application.ViewModels.Patient;
using HospitalApp.Core.Domain.Entities;
using HospitalApp.Middlewares;
using Microsoft.AspNetCore.Mvc;

namespace HospitalApp.Controllers
{
    public class PatientController : Controller
    {
        private readonly IPatientSevice _service;
        private readonly ValidateUserSession _validateUserSession;

        public PatientController(IPatientSevice service, ValidateUserSession validateUserSession)
        {
            _service = service;
            _validateUserSession = validateUserSession;
        }

        public async Task<IActionResult> Index()
        {
            if (!_validateUserSession.HasUser() || _validateUserSession.GetUserRole() != UserRole.Asistente)
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
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            return View("SavePatient", new SavePatientViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(SavePatientViewModel newPatient)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            if (!ModelState.IsValid)
            {
                return View("SavePatient", newPatient);
            }

            await _service.Add(newPatient);
            return RedirectToRoute(new { controller = "Patient", action = "Index" });
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            var patientViewModel = await _service.GetByIdSaveViewModel(id);

            return View("SavePatient", patientViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SavePatientViewModel updatedShowViewModel)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            if (!ModelState.IsValid)
            {
                return View("SavePatient", updatedShowViewModel);
            }

            await _service.Update(updatedShowViewModel);
            return RedirectToRoute(new { controller = "Patient", action = "Index" });
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            return View(await _service.GetByIdSaveViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            await _service.Delete(id);
            return RedirectToRoute(new { controller = "Patient", action = "Index" });
        }

    }
}

