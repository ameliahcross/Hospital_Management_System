using System;
using HospitalApp.Core.Application.Interfaces.Services;
using HospitalApp.Core.Application.ViewModels.LabTest;

namespace HospitalApp.Core.Application.Interfaces.Repositories
{
	public interface ILabTestService : IGenericService<LabTestViewModel, SaveLabTestViewModel>
    {
        Task<List<LabTestViewModel>> GetAvailableLabTestsAsync();
        //Task CreateLabResultsAsync(List<SaveLabResultViewModel> labResultSaveViewModels);
    }
}

