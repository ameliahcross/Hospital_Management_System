using System;
using HospitalApp.Core.Application.ViewModels;

namespace HospitalApp.Core.Application.Interfaces.Services
{
	public interface IGenericService<ViewModel, SaveViewModel>
                     where ViewModel : class
                     where SaveViewModel : class

    {
        Task<List<ViewModel>> GetAllViewModel();
        Task<SaveViewModel> GetByIdSaveViewModel(int id);
        Task Update(SaveViewModel appointmentToSave);
        Task Add(SaveViewModel appointmentToCreate);
        Task Delete(int id);
    }
}

