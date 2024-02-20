using System;
using HospitalApp.Core.Application.ViewModels;
using HospitalApp.Core.Application.ViewModels.Appointment;
using HospitalApp.Core.Application.ViewModels.LabResult;

namespace HospitalApp.Core.Application.Interfaces.Services
{
	public interface ILabResultService
        //: IGenericService<LabResultViewModel, SaveLabResultViewModel>
    {
        Task CreateLabResultsAsync(List<SaveLabResultViewModel> labResultSaveViewModels);
        Task<List<SaveLabResultViewModel>> GetAllViewModel();
    }
}

