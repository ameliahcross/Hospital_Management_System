using HospitalApp.Core.Application.Interfaces.Services;
using HospitalApp.Core.Application.ViewModels.Patient;
using Microsoft.AspNetCore.Mvc;

namespace HospitalApp.Controllers
{
    public class PatientController : Controller
    {
        private readonly IPatientSevice _service;

        public PatientController(IPatientSevice service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _service.GetAllViewModel();
            return View(list);
        }

        public async Task<IActionResult> Create()
        {
            return View("SavePatient", new SavePatientViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(SavePatientViewModel newPatient)
        {
            if (!ModelState.IsValid)
            {
                return View("SavePatient", newPatient);
            }

            await _service.Add(newPatient);
            return RedirectToRoute(new { controller = "Patient", action = "Index" });
        }

        public async Task<IActionResult> Edit(int id)
        {
            var patientViewModel = await _service.GetByIdSaveViewModel(id);

            return View("SavePatient", patientViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SavePatientViewModel updatedShowViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("SavePatient", updatedShowViewModel);
            }

            await _service.Update(updatedShowViewModel);
            return RedirectToRoute(new { controller = "Patient", action = "Index" });
        }

        public async Task<IActionResult> Delete(int id)
        {
            return View(await _service.GetByIdSaveViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            await _service.Delete(id);
            return RedirectToRoute(new { controller = "Patient", action = "Index" });
        }

    }
}

