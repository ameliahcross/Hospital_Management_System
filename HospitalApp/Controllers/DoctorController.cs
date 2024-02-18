using HospitalApp.Core.Application.Interfaces.Services;
using HospitalApp.Core.Application.ViewModels.Doctor;
using Microsoft.AspNetCore.Mvc;

namespace HospitalApp.Controllers
{
    public class DoctorController : Controller
    {
        private readonly IDoctorService _service;

        public DoctorController(IDoctorService service)
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
            return View("SaveDoctor", new SaveDoctorViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaveDoctorViewModel newDr)
        {
            if (!ModelState.IsValid)
            {
                return View("SaveDoctor", newDr);
            }

            await _service.Add(newDr);
            return RedirectToRoute(new { controller = "Doctor", action = "Index" });
        }

        public async Task<IActionResult> Edit(int id)
        {
            var doctorViewModel = await _service.GetByIdSaveViewModel(id);

            return View("SaveDoctor", doctorViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SaveDoctorViewModel updatedDrViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("SaveDoctor", updatedDrViewModel);
            }

            await _service.Update(updatedDrViewModel);
            return RedirectToRoute(new { controller = "Doctor", action = "Index" });
        }

        public async Task<IActionResult> Delete(int id)
        {
            return View(await _service.GetByIdSaveViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            await _service.Delete(id);
            return RedirectToRoute(new { controller = "Doctor", action = "Index" });
        }

    }
}

