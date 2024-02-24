using System;
using HospitalApp.Core.Application.ViewModels.User;
using HospitalApp.Core.Domain.Entities;

namespace HospitalApp.Core.Application.Interfaces.Services
{
	public interface IUserService : IGenericService<UserViewModel, SaveUserViewModel>
    {
        Task<UserViewModel> Login(LoginViewModel loginVm);
        Task<bool> ValidateUsername(string username);
        Task<User> GetById(int id);
    }
}

