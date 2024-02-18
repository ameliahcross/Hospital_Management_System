﻿using HospitalApp.Core.Application.Interfaces.Services;
using HospitalApp.Core.Application.Services;
using HospitalApp.Core.Application.ViewModels.Appointment;
using Microsoft.AspNetCore.Mvc;

namespace HospitalApp.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly IAppointmentService _service;
        private readonly IPatientSevice _servicePatient;
        private readonly IDoctorService _serviceDr;

        public AppointmentController(IAppointmentService service, IPatientSevice servicePatient, IDoctorService serviceDr)
        {
            _service = service;
            _servicePatient = servicePatient;
            _serviceDr = serviceDr;
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
    }
}
