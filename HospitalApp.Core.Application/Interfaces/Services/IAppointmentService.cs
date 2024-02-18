using System;
using HospitalApp.Core.Application.ViewModels;
using HospitalApp.Core.Application.ViewModels.Appointment;

namespace HospitalApp.Core.Application.Interfaces.Services
{
	public interface IAppointmentService : IGenericService<AppointmentViewModel, SaveAppointmentViewModel>
	{
        
    }
}

