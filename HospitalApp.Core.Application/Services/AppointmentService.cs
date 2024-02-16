using System;
using HospitalApp.Core.Application.Interfaces.Repositories;
using HospitalApp.Core.Application.ViewModels;

namespace HospitalApp.Core.Application.Services
{
    public class AppointmentService
    {
        private readonly IAppointmentRepository _repository;

        public AppointmentService(IAppointmentRepository repository)
        {
            _repository = (repository);
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
    }
}

