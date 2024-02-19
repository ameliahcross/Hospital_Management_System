using System;
using HospitalApp.Core.Application.ViewModels;
using HospitalApp.Core.Application.ViewModels.Appointment;
using HospitalApp.Core.Application.ViewModels.LabResult;
using HospitalApp.Core.Application.ViewModels.LabTest;
using HospitalApp.Core.Domain.Entities;

namespace HospitalApp.Core.Application.Interfaces.Services
{
	public interface IAppointmentService : IGenericService<AppointmentViewModel, SaveAppointmentViewModel>
	{
        Task ChangeAppointmentStatusAsync(int appointmentId, AppointmentStatus newStatus);
        //Task<List<LabTestViewModel>> GetAvailableLabTestsAsync();
    }
}

