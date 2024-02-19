using HospitalApp.Core.Application.Interfaces.Services;
using HospitalApp.Core.Application.Services;
using HospitalApp.Core.Application.ViewModels.Doctor;
using HospitalApp.Core.Application.ViewModels.LabResult;
using Microsoft.AspNetCore.Mvc;

namespace HospitalApp.Controllers
{
    public class LabResultController : Controller
    {
        private readonly ILabResultService _service;

        public LabResultController(ILabResultService service)
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
            return View("SaveLabResult", new SaveLabResultViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaveLabResultViewModel newResult)
        {
            if (!ModelState.IsValid)
            {
                return View("SaveLabResult", newResult);
            }

            await _service.Add(newResult);
            return RedirectToRoute(new { controller = "LabResult", action = "Index" });
        }

        public async Task<IActionResult> Edit(int id)
        {
            var resultViewModel = await _service.GetByIdSaveViewModel(id);

            return View("SaveLabResult", resultViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SaveLabResultViewModel updatedResultViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("SaveLabResult", updatedResultViewModel);
            }

            await _service.Update(updatedResultViewModel);
            return RedirectToRoute(new { controller = "LabResult", action = "Index" });
        }

        public async Task<IActionResult> Delete(int id)
        {
            return View(await _service.GetByIdSaveViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            await _service.Delete(id);
            return RedirectToRoute(new { controller = "LabResult", action = "Index" });
        }

    }
}

