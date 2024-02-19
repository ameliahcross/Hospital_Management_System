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

        public async Task<IActionResult> SelectLabTests(int appointmentId)
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
                AppointmentId = appointmentId,
                AvailableLabTests = labTestSelections
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> PerformLabTests(SelectLabTestsViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Inicializa la lista de LabResult para crear
                var labResultsToCreate = new List<SaveLabResultViewModel>();

                // Itera a través de las pruebas seleccionadas y crea SaveLabResultViewModel para cada una
                foreach (var selectedTest in model.SelectedLabTests.Where(lt => lt.IsSelected))
                {
                    var labResultViewModel = new SaveLabResultViewModel
                    {
                        LabTestId = selectedTest.LabTestId,
                        PatientId = model.PatientId, // Asumiendo que tienes el PatientId disponible
                        Result = "Resultado pendiente", // Puedes establecer un valor predeterminado o permitir que el usuario ingrese uno
                        Status = LabResultStatus.Pendiente
                    };
                    labResultsToCreate.Add(labResultViewModel);
                }

                // Llama al servicio para crear los resultados del laboratorio
                await _serviceResult.CreateLabResultsAsync(labResultsToCreate);

                // Cambia el estado de la cita a Pendiente de Resultados
                await _service.ChangeAppointmentStatusAsync(model.AppointmentId, AppointmentStatus.Pendiente_Resultados);

                // Redirige al usuario a la vista de índice
                return RedirectToAction("Index");
            }

            // Si el modelo no es válido, vuelve a cargar la vista SelectLabTests con el modelo actual
            // para que el usuario pueda corregir cualquier error
            var availableLabTests = await _serviceTest.GetAvailableLabTestsAsync();

            // Convierte LabTestViewModel a LabTestSelection
            model.AvailableLabTests = availableLabTests.Select(lt => new SelectLabTestsViewModel.LabTestSelection
            {
                LabTestId = lt.Id, // Asumiendo que tienes una propiedad Id en tu LabTestViewModel
                Name = lt.Name,    // Asumiendo que tienes una propiedad Name en tu LabTestViewModel
                IsSelected = false // Inicialmente no seleccionado
            }).ToList();

            return View("SelectLabTests", model);
        }
    }
}

