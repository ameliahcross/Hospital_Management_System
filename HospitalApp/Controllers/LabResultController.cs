using HospitalApp.Core.Application.Interfaces.Services;
using HospitalApp.Core.Application.ViewModels.LabResult;
using HospitalApp.Core.Domain.Entities;
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

        public async Task<IActionResult> Consult(int? labResultId = null, int? appointmentId = null)
        {
            if (appointmentId.HasValue)
            {
                var labResults = await _service.GetLabResultsByAppointmentId(appointmentId.Value);
                return View("AppointmentLabResult", labResults);
                //return View(labResults);
            }
            else if (labResultId.HasValue)
            {
                var labResult = await _service.GetByIdSaveViewModel(labResultId.Value);
                var labResultsViewModelList = new List<SaveLabResultViewModel> { labResult };
                return RedirectToAction("AppointmentLabResult", labResultsViewModelList);
            }
            else
            {
                var labResults = await _service.GetAllViewModel();

                var labResultsViewModel = labResults.Select(result => new SaveLabResultViewModel
                {
                    AppointmentId = result.AppointmentId,
                    PatientName = result.PatientName,
                    LabTestName = result.LabTestName,
                    Status = result.Status,
                }).ToList();

                return View("AppointmentLabResult", labResultsViewModel);
                //return View(labResultsViewModel);
            }
        }


        public async Task<IActionResult> Create()
        {
            return View("SaveLabResult", new SaveLabResultViewModel());
        }

        //[HttpPost]
        //public async Task<IActionResult> Create(SaveLabResultViewModel newResult)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View("SaveLabResult", newResult);
        //    }

        //    await _service.Add(newResult);
        //    return RedirectToRoute(new { controller = "LabResult", action = "Index" });
        //}

        public async Task<IActionResult> Report(int id, int appointmentId)
        {
            var resultViewModel = await _service.GetByIdSaveViewModel(id);
            return View("SaveLabResult", resultViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Report(SaveLabResultViewModel updatedResultViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("SaveLabResult", updatedResultViewModel);
            }

            await _service.ChangeLabResultStatusAsync(updatedResultViewModel.Id, LabResultStatus.Completado);
            return RedirectToAction("Index");
        }

        //public async Task<IActionResult> Delete(int id)
        //{
        //    return View(await _service.GetByIdSaveViewModel(id));
        //}

        //[HttpPost]
        //public async Task<IActionResult> DeletePost(int id)
        //{
        //    await _service.Delete(id);
        //    return RedirectToRoute(new { controller = "LabResult", action = "Index" });
        //}



    }
}

