using HospitalApp.Core.Application.Interfaces.Services;
using HospitalApp.Core.Application.ViewModels.LabResult;
using HospitalApp.Core.Domain.Entities;
using HospitalApp.Middlewares;
using Microsoft.AspNetCore.Mvc;

namespace HospitalApp.Controllers
{
    public class LabResultController : Controller
    {
        private readonly ILabResultService _service;
        private readonly ValidateUserSession _validateUserSession;

        public LabResultController(ILabResultService service, ValidateUserSession validateUserSession)
        {
            _service = service;
            _validateUserSession = validateUserSession;
        }

        public async Task<IActionResult> Index(string? cedula, int? appointmentId)
        {
            if (!_validateUserSession.HasUser() || _validateUserSession.GetUserRole() != UserRole.Asistente)
            {
                return RedirectToRoute(new { controller = "User", action = "Permission" });
            }

            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            if (cedula == null)
            {
                var list = await _service.GetAllViewModel();
                return View(list);

            } else
            {
                var filtered = await _service.GetAllViewModelFiltered(cedula);
                ViewBag.theAppointment = appointmentId;
                return View(filtered);
            }
        }

        public async Task<IActionResult> Consult(int? labResultId = null, int? appointmentId = null)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            if (appointmentId.HasValue)
            {
                var labResults = await _service.GetLabResultsByAppointmentId(appointmentId.Value);
                return View("AppointmentLabResult", labResults);
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
            }
        }


        public async Task<IActionResult> Create()
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            return View("SaveLabResult", new SaveLabResultViewModel());
        }

        public async Task<IActionResult> Report(int id, int appointmentId)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            var resultViewModel = await _service.GetByIdSaveViewModel(id);
            return View("SaveLabResult", resultViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Report(SaveLabResultViewModel updatedResultViewModel)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            if (!ModelState.IsValid)
            {
                return View("SaveLabResult", updatedResultViewModel);
            }

            await _service.Update(updatedResultViewModel);
            await _service.ChangeLabResultStatusAsync(updatedResultViewModel.Id, LabResultStatus.Completado);

            var all = await _service.GetAllViewModel();

            return View("Index", all);
        }


        public async Task<IActionResult> FinalResults(int appointmentId, string labResultIds)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            var labResultIdsList = !string.IsNullOrEmpty(labResultIds) ? labResultIds.Split(',').Select(int.Parse).ToList() : new List<int>();
            var labResults = await _service.GetLabResultsByAppointmentId(appointmentId);

            var completedResults = await _service.GetCompletedAsync(appointmentId);

            var labResultsViewModel = completedResults.Select(result => new LabResultViewModel
            {
                ResultadoDigitado = result.ResultadoDigitado,
                LabTestName = result.LabTestName,
                PatientName = result.PatientName,
                AppointmentId = result.AppointmentId,
                Status = result.Status
            }).ToList();

            return View("CompletedLabResult", labResultsViewModel);
        }

    }
}

