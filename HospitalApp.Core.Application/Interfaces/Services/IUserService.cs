using System;
using HospitalApp.Core.Application.ViewModels.User;

namespace HospitalApp.Core.Application.Interfaces.Services
{
	public interface IUserService
	{
        Task<List<UserViewModel>> GetAllViewModel();
        Task<UserViewModel> GetByIdSaveViewModel(int id);
        Task Update(UserViewModel userToSave);
        Task Add(UserViewModel userToCreate);
        Task Delete(int id);
    }
}

