using HospitalApp.Core.Application.Interfaces.Services;
using HospitalApp.Core.Application.ViewModels.Appointment;
using HospitalApp.Core.Domain.Entities;
using HospitalApp.Middlewares;
using Microsoft.AspNetCore.Mvc;

namespace HospitalApp.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly IAppointmentService _service;
        private readonly IPatientSevice _servicePatient;
        private readonly IDoctorService _serviceDr;
        private readonly ILabResultService _serviceResult;
        private readonly ValidateUserSession _validateUserSession;

        public AppointmentController(IAppointmentService service, IPatientSevice servicePatient, IDoctorService serviceDr, ValidateUserSession validateUserSession, ILabResultService serviceResult)
        {
            _service = service;
            _servicePatient = servicePatient;
            _serviceDr = serviceDr;
            _serviceResult = serviceResult;
            _validateUserSession = validateUserSession;
        }

        public async Task<IActionResult> Index()
        {
            if (!_validateUserSession.HasUser() || _validateUserSession.GetUserRole() != UserRole.Asistente)
            {
                return RedirectToRoute(new { controller = "User", action = "Permission"});
            }

            var list = await _service.GetAllViewModel();

            foreach (var appointment in list)
            {
                appointment.LabResultId = await _serviceResult.GetLabResultIdForAppointment(appointment.AppointmentId);
            }
            return View(list);
        }

        public async Task<IActionResult> Create()
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

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
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

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
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            var appointmentViewModel = await _service.GetByIdSaveViewModel(id);
            appointmentViewModel.Patients = await _servicePatient.GetAllViewModel();
            appointmentViewModel.Doctors = await _serviceDr.GetAllViewModel();
            return View("SaveAppointment", appointmentViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SaveAppointmentViewModel updatedAppointmentViewModel)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

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
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

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
            if (! _validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            await _service.Delete(id);
            return RedirectToRoute(new { controller = "Appointment", action = "Index" });
        }

        public async Task<IActionResult> CompleteAppointment(int appointmentId)
        {
            if (! _validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            if (ModelState.IsValid)
            {
                await _service.ChangeAppointmentStatusAsync(appointmentId, AppointmentStatus.Completada);
                return RedirectToAction("Index");
            }
            return RedirectToRoute(new { controller = "Appointment", action = "Index" });
        }


    }
}

