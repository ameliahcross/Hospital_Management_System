using HospitalApp.Core.Application.Interfaces.Repositories;
using HospitalApp.Core.Application.Interfaces.Services;
using HospitalApp.Core.Application.Services;
using HospitalApp.Core.Application.ViewModels.Appointment;
using HospitalApp.Core.Application.ViewModels.LabResult;
using HospitalApp.Core.Application.ViewModels.LabTest;
using HospitalApp.Core.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
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

            foreach (var appointment in list)
            {
                appointment.LabResultId = await _serviceResult.GetLabResultIdForAppointment(appointment.AppointmentId);
            }
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

        

        public async Task<IActionResult> CompleteAppointment(int appointmentId)
        {
            if (ModelState.IsValid)
            {
                await _service.ChangeAppointmentStatusAsync(appointmentId, AppointmentStatus.Pendiente_Resultados);
                return RedirectToAction("Index");
            }
            return RedirectToRoute(new { controller = "Appointment", action = "Index" });
        }






    }
}

