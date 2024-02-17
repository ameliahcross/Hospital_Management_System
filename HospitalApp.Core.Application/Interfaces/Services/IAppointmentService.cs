using System;
using HospitalApp.Core.Application.ViewModels;
using HospitalApp.Core.Application.ViewModels.Appointment;

namespace HospitalApp.Core.Application.Interfaces.Services
{
	public interface IAppointmentService
	{
        Task<List<AppointmentViewModel>> GetAllViewModel();
        Task<SaveAppointmentViewModel> GetByIdSaveViewModel(int id);
        Task Update(SaveAppointmentViewModel appointmentToSave);
        Task Add(SaveAppointmentViewModel appointmentToCreate);
        Task Delete(int id);
    }
}

