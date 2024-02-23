using System;
using HospitalApp.Core.Application.Interfaces.Repositories;
using HospitalApp.Core.Application.Interfaces.Services;
using HospitalApp.Core.Application.ViewModels.User;
using HospitalApp.Core.Domain.Entities;

namespace HospitalApp.Core.Application.Services
{
	public class UserService : IUserService
    {
		private readonly IUserRepository _repository;

		public UserService(IUserRepository useRepository)
		{
            _repository = useRepository;
        }

        public async Task<List<UserViewModel>> GetAllViewModel()
        {
            var usersList = await _repository.GetAllAsync();

            return usersList.Select(user => new UserViewModel
            {
                Id = user.Id,
                Username = user.Username,
                Password = user.Password,
                Email = user.Email,
                Name = user.Name,
                LastName = user.LastName,
                
            }).ToList();
        }

        public async Task<SaveUserViewModel> GetByIdSaveViewModel(int id)
        {
            var user = await _repository.GetByIdAsync(id);
            SaveUserViewModel userViewModel = new();
            userViewModel.Id = userViewModel.Id;
            userViewModel.Username = user.Username;
            userViewModel.Password = user.Password;
            userViewModel.Email = user.Email;
            userViewModel.Name = user.Name;
            userViewModel.LastName = user.LastName;

            return userViewModel;
        }

        public async Task Update(SaveUserViewModel userToSave)
        {
            User user = new();
            user.Id = userToSave.Id;
            user.Username = userToSave.Username;
            user.Password = userToSave.Password;
            user.Email = userToSave.Email;
            user.Name = userToSave.Name;
            user.LastName = userToSave.LastName;

            await _repository.UpdateAsync(user);
        }

        public async Task Add(SaveUserViewModel userToCreate)
        {
            var user = new User
            {
                Username = userToCreate.Username,
                Password = userToCreate.Password,
                Email = userToCreate.Email,
                Name = userToCreate.Name,
                LastName = userToCreate.LastName
            };

            await _repository.AddAsync(user);
        }

        public async Task Delete(int id)
        {
            var user = await _repository.GetByIdAsync(id);
            await _repository.DeleteAsync(user);
        }

        public async Task<UserViewModel> Login(LoginViewModel loginVm)
        {
            User user = await _repository.LoginAsync(loginVm);

            if (user == null)
            {
                return null;
            }

            UserViewModel userVm = new();
            userVm.Id = user.Id;
            userVm.Name = user.Name;
            userVm.Email = user.Email;
            userVm.Username = user.Username;
            userVm.Password = user.Password;

            return userVm;
        }

    }
}

