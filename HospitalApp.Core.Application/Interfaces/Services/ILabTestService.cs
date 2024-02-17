using System;
using HospitalApp.Core.Application.ViewModels.LabTest;

namespace HospitalApp.Core.Application.Interfaces.Repositories
{
	public interface ILabTestService
	{
        Task<List<LabTestViewModel>> GetAllViewModel();
        Task<LabTestViewModel> GetByIdSaveViewModel(int id);
        Task Update(LabTestViewModel labTestToSave);
        Task Add(LabTestViewModel labTestToCreate);
        Task Delete(int id);
    }
}

