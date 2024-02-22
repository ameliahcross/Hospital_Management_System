using System;
using HospitalApp.Core.Application.ViewModels;
using HospitalApp.Core.Application.ViewModels.Appointment;
using HospitalApp.Core.Application.ViewModels.LabResult;

namespace HospitalApp.Core.Application.Interfaces.Services
{
	public interface ILabResultService
        : IGenericService<LabResultViewModel, SaveLabResultViewModel>
    {
        Task CreateLabResultsAsync(List<SaveLabResultViewModel> labResultSaveViewModels);
        Task<int?> GetLabResultIdForAppointment(int appointmentId);
        Task<List<SaveLabResultViewModel>> GetLabResultsByAppointmentId(int appointmentId);
        //Task<List<SaveLabResultViewModel>> GetAllViewModel();
    }
}

