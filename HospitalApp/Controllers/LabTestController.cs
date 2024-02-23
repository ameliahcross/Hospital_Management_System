using HospitalApp.Core.Application.Interfaces.Repositories;
using HospitalApp.Core.Application.Interfaces.Services;
using HospitalApp.Core.Application.ViewModels.LabResult;
using HospitalApp.Core.Application.ViewModels.LabTest;
using HospitalApp.Core.Domain.Entities;
using HospitalApp.Middlewares;
using Microsoft.AspNetCore.Mvc;
using static HospitalApp.Core.Application.ViewModels.LabTest.SelectLabTestsViewModel;

namespace HospitalApp.Controllers
{
    public class LabTestController : Controller
    {
        private readonly ILabTestService _service;
        private readonly ILabResultService _serviceResult;
        private readonly IAppointmentService _serviceAppointment;
        private readonly ValidateUserSession _validateUserSession;

        public LabTestController(ILabTestService service, ILabResultService serviceResult, IAppointmentService serviceAppointment, ValidateUserSession validateUserSession)
        {
            _service = service;
            _serviceAppointment = serviceAppointment;
            _serviceResult = serviceResult;
            _validateUserSession = validateUserSession;
        }

        public async Task<IActionResult> Index()
        {
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

            return View("SaveLabTest", new SaveLabTestViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaveLabTestViewModel newLabTest)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            if (!ModelState.IsValid)
            {
                return View("SaveLabTest", newLabTest);
            }

            await _service.Add(newLabTest);
            return RedirectToRoute(new { controller = "LabTest", action = "Index" });
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            var labTestViewModel = await _service.GetByIdSaveViewModel(id);

            return View("SaveLabTest", labTestViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SaveLabTestViewModel updatedlabTestViewModel)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            if (!ModelState.IsValid)
            {
                return View("SaveLabTest", updatedlabTestViewModel);
            }

            await _service.Update(updatedlabTestViewModel);
            return RedirectToRoute(new { controller = "LabTest", action = "Index" });
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
            return RedirectToRoute(new { controller = "LabTest", action = "Index" });
        }

        public async Task<IActionResult> SelectLabTests(int AppointmentId)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            var availableLabTests = await _service.GetAvailableLabTestsAsync();

            var labTestSelections = availableLabTests.Select(labTest => new LabTestSelection
            {
                LabTestId = labTest.Id,
                Name = labTest.Name,
                IsSelected = false
            }).ToList();

            var model = new SelectLabTestsViewModel
            {
                AppointmentId = AppointmentId,
                AvailableLabTests = labTestSelections
            };

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> PerformLabTests(SelectLabTestsViewModel model)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            if (ModelState.IsValid)
            {
                var labResultsToCreate = model.AvailableLabTests
                    .Where(lt => lt.IsSelected)
                    .Select(lt => new SaveLabResultViewModel
                    {
                        LabTestId = lt.LabTestId,
                        Result = "Resultado pendiente",
                        Status = LabResultStatus.Pendiente,
                        AppointmentId = model.AppointmentId
                    }).ToList();

                await _serviceResult.CreateLabResultsAsync(labResultsToCreate);
                await _serviceAppointment.ChangeAppointmentStatusAsync(model.AppointmentId, AppointmentStatus.Pendiente_Resultados);
                return RedirectToRoute(new { controller = "Appointment", action = "Index" });
            }

            model.AvailableLabTests = (await _service.GetAvailableLabTestsAsync())
                     .Select(lt => new LabTestSelection
                     {
                         LabTestId = lt.Id,
                         Name = lt.Name,
                         IsSelected = false
                     }).ToList();

            return View("~/Views/LabTest/SelectLabTests.cshtml", model);
        }

    }
}

