using System;
using HospitalApp.Core.Application.ViewModels;

namespace HospitalApp.Core.Application.Services
{
	public class AppointmentService
	{
		private readonly AppointmentRepository _appointmentRepository;

		public AppointmentService(ApplicationContext dbContext)
		{
            _appointmentRepository = new(dbContext);
        }

        public async Task<List<AppointmentViewModel>> GetAllViewModel()
        {
            var appointmentsList = await _appointmentRepository.GetAllAsync();

            return appointmentsList.Select(appointment => new AppointmentViewModel
            {
                Id = appointment.Id,
                Date = appointment.Date,
                Time = appointment.Time,
                Reason = appointment.Reason,
                Status = appointment.Status,
                DoctorName = appointment.DoctorName

            }).ToList();
        }
    }
}

