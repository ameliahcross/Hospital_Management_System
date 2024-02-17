using System;
using HospitalApp.Core.Application.ViewModels.LabResult;

namespace HospitalApp.Core.Application.Interfaces.Services
{
	public interface ILabResultService
	{
        Task<List<LabResultViewModel>> GetAllViewModel();
        Task<LabResultViewModel> GetByIdSaveViewModel(int id);
        Task Update(LabResultViewModel labResultToSave);
        Task Add(LabResultViewModel labResultToCreate);
        Task Delete(int id);
    }
}

