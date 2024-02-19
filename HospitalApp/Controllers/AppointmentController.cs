using HospitalApp.Core.Application.Interfaces.Repositories;
using HospitalApp.Core.Application.Interfaces.Services;
using HospitalApp.Core.Application.Services;
using HospitalApp.Core.Application.ViewModels.Appointment;
using HospitalApp.Core.Application.ViewModels.LabResult;
using HospitalApp.Core.Application.ViewModels.LabTest;
using HospitalApp.Core.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static HospitalApp.Core.Application.ViewModels.LabTest.SelectLabTestsViewModel;

namespace HospitalApp.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly IAppointmentService _service;
        private readonly IPatientSevice _servicePatient;
        private readonly IDoctorService _serviceDr;
        private readonly ILabTestService _serviceTest;
        private readonly ILabResultService _serviceResult;

        public AppointmentController
        (IAppointmentService service, IPatientSevice servicePatient, IDoctorService serviceDr, ILabTestService serviceTest, ILabResultService serviceResult)
        {
            _service = service;
            _servicePatient = servicePatient;
            _serviceDr = serviceDr;
            _serviceTest = serviceTest;
            _serviceResult = serviceResult;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _service.GetAllViewModel();
            return View(list);
        }

        public async Task<IActionResult> Create()
        {
            var patients = await _servicePatient.GetAllViewModel();
            var doctors = await _serviceDr.GetAllViewModel();
            var AppointmentViewModel = new SaveAppointmentViewModel
            {
                Patients = patients,
                Doctors = doctors
            };
            return View("SaveAppointment", AppointmentViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaveAppointmentViewModel newAppointment)
        {
            if (!ModelState.IsValid)
            {
                newAppointment.Patients = await _servicePatient.GetAllViewModel();
                newAppointment.Doctors = await _serviceDr.GetAllViewModel();
                return View("SaveAppointment", newAppointment);
            }

            await _service.Add(newAppointment);
            return RedirectToRoute(new { controller = "Appointment", action = "Index" });
        }

        public async Task<IActionResult> Edit(int id)
        {
            var appointmentViewModel = await _service.GetByIdSaveViewModel(id);
            appointmentViewModel.Patients = await _servicePatient.GetAllViewModel();
            appointmentViewModel.Doctors = await _serviceDr.GetAllViewModel();
            return View("SaveAppointment", appointmentViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SaveAppointmentViewModel updatedAppointmentViewModel)
        {
            if (!ModelState.IsValid)
            {
                updatedAppointmentViewModel.Patients = await _servicePatient.GetAllViewModel();
                updatedAppointmentViewModel.Doctors = await _serviceDr.GetAllViewModel();
                return View("SaveAppointment", updatedAppointmentViewModel);
            }

            await _service.Update(updatedAppointmentViewModel);
            return RedirectToRoute(new { controller = "Appointment", action = "Index" });
        }

        public async Task<IActionResult> Delete(int id)
        {
            var appointmentViewModel =  await _service.GetByIdSaveViewModel(id);

            var patient = await _servicePatient.GetByIdSaveViewModel(appointmentViewModel.PatientId);
            var doctor = await _serviceDr.GetByIdSaveViewModel(appointmentViewModel.DoctorId);

            var deleteAppointmentViewModel = new DeleteAppointmentViewModel
            {
                Id = appointmentViewModel.Id,
                PatientName = $"{patient.FirstName} {patient.LastName}",
                DoctorName = $"{doctor.FirstName} {doctor.LastName}"
            };

            return View(deleteAppointmentViewModel); 
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            await _service.Delete(id);
            return RedirectToRoute(new { controller = "Appointment", action = "Index" });
        }

        //manejo de pruebas y resultados

        public async Task<IActionResult> SelectLabTests(int Id)
        {
            var availableLabTests = await _serviceTest.GetAvailableLabTestsAsync();

            var labTestSelections = availableLabTests.Select(labTest => new LabTestSelection
            {
                LabTestId = labTest.Id,
                Name = labTest.Name,
                IsSelected = false
            }).ToList();

            var model = new SelectLabTestsViewModel
            {
                Id = Id,
                //AppointmentId = AppointmentId,
                AvailableLabTests = labTestSelections
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> PerformLabTests(SelectLabTestsViewModel model)
        {
            if (ModelState.IsValid)
            {
                var labResultsToCreate = model.AvailableLabTests
                    .Where(lt => lt.IsSelected)
                    .Select(lt => new SaveLabResultViewModel
                    {
                        LabTestId = lt.LabTestId,
                        Result = "Resultado pendiente",
                        Status = LabResultStatus.Pendiente, 
                    }).ToList();

                await _serviceResult.CreateLabResultsAsync(labResultsToCreate);
                await _service.ChangeAppointmentStatusAsync(model.Id, AppointmentStatus.Pendiente_Resultados);
                return RedirectToAction("Index");
            }

            // Si el modelo no es válido, recargar la vista con el modelo para corrección
            model.AvailableLabTests = (await _serviceTest.GetAvailableLabTestsAsync())
                     .Select(lt => new LabTestSelection
                     {
                         LabTestId = lt.Id, // Asumiendo que la clase LabTestViewModel tiene una propiedad Id.
                         Name = lt.Name,    // Asumiendo que la clase LabTestViewModel tiene una propiedad Name.
                         IsSelected = false // Inicializar como no seleccionado.
                     }).ToList();

            return View("SelectLabTests", model);
        }

    }
}

