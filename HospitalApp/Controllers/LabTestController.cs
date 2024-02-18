using HospitalApp.Core.Application.Interfaces.Repositories;
using HospitalApp.Core.Application.ViewModels.LabTest;
using Microsoft.AspNetCore.Mvc;

namespace HospitalApp.Controllers
{
    public class LabTestController : Controller
    {
        private readonly ILabTestService _service;

        public LabTestController(ILabTestService service)
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
            return View("SaveLabTest", new SaveLabTestViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaveLabTestViewModel newLabTest)
        {
            if (!ModelState.IsValid)
            {
                return View("SaveLabTest", newLabTest);
            }

            await _service.Add(newLabTest);
            return RedirectToRoute(new { controller = "LabTest", action = "Index" });
        }

        public async Task<IActionResult> Edit(int id)
        {
            var labTestViewModel = await _service.GetByIdSaveViewModel(id);

            return View("SaveLabTest", labTestViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SaveLabTestViewModel updatedlabTestViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("SaveLabTest", updatedlabTestViewModel);
            }

            await _service.Update(updatedlabTestViewModel);
            return RedirectToRoute(new { controller = "LabTest", action = "Index" });
        }

        public async Task<IActionResult> Delete(int id)
        {
            return View(await _service.GetByIdSaveViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            await _service.Delete(id);
            return RedirectToRoute(new { controller = "LabTest", action = "Index" });
        }

    }
}

