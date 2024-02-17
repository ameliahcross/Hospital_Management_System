﻿using System;
using HospitalApp.Core.Application.Interfaces.Repositories;
using HospitalApp.Core.Application.Interfaces.Services;
using HospitalApp.Core.Application.ViewModels;
using HospitalApp.Core.Application.ViewModels.Appointment;
using HospitalApp.Core.Domain.Entities;

namespace HospitalApp.Core.Application.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _repository;

       // Este constructor dice que esta clase depende de IGenericRepository<Appointment>
        public AppointmentService(IAppointmentRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<AppointmentViewModel>> GetAllViewModel()
        {
            var appointmentsList = await _repository.GetAllAsync();

            return appointmentsList.Select(appointment => new AppointmentViewModel
            {
                Id = appointment.Id,
                Date = appointment.Date,
                Time = appointment.Time,
                Reason = appointment.Reason,
                Status = appointment.Status,
                PatientName = appointment.Patient.FirstName + " " + appointment.Patient.LastName,
                DoctorName = appointment.Doctor.FirstName + " " + appointment.Doctor.LastName,
            }).ToList();
        }

        public async Task<SaveAppointmentViewModel> GetByIdSaveViewModel(int id)
        {
            var appointment = await _repository.GetByIdAsync(id);
            SaveAppointmentViewModel appointmentViewModel = new();
            appointmentViewModel.Date = appointment.Date;
            appointmentViewModel.Time = appointment.Time;
            appointmentViewModel.Reason = appointment.Reason;
            appointmentViewModel.Status = appointment.Status;
            appointmentViewModel.PatientId = appointment.PatientId;
            appointmentViewModel.DoctorId = appointment.DoctorId;
            return appointmentViewModel;
        }

        public async Task Update(SaveAppointmentViewModel appointmentToSave)
        {
            Appointment appointment = new();
            appointment.Id = appointmentToSave.Id;
            appointment.Date = appointmentToSave.Date;
            appointment.Time = appointmentToSave.Time;
            appointment.Reason = appointmentToSave.Reason;
            appointment.Status = appointmentToSave.Status;
            appointment.PatientId = appointmentToSave.PatientId;
            appointment.DoctorId = appointmentToSave.DoctorId;

            await _repository.UpdateAsync(appointment);
        }

        public async Task Add(SaveAppointmentViewModel appointmentToCreate)
        {
            Appointment appointment = new();
            appointment.Id = appointmentToCreate.Id;
            appointment.Date = appointmentToCreate.Date;
            appointment.Time = appointmentToCreate.Time;
            appointment.Reason = appointmentToCreate.Reason;
            appointment.Status = appointmentToCreate.Status;
            appointment.PatientId = appointmentToCreate.PatientId;
            appointment.DoctorId = appointmentToCreate.DoctorId;

            await _repository.AddAsync(appointment);
        }

        public async Task Delete(int id)
        {
            var appointment = await _repository.GetByIdAsync(id);
            await _repository.DeleteAsync(appointment);
        }

    }
}

