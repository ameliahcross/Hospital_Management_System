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
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Username = user.Username,
                Password = user.Password,
                UserType = user.UserType
            }).ToList();
        }

        public async Task<SaveUserViewModel> GetByIdSaveViewModel(int id)
        {
            var user = await _repository.GetByIdAsync(id);
            SaveUserViewModel userViewModel = new();
            userViewModel.Id = userViewModel.Id;
            userViewModel.FirstName = user.FirstName;
            userViewModel.LastName = user.LastName;
            userViewModel.Email = user.Email;
            userViewModel.Username = user.Username;
            userViewModel.Password = user.Password;
            userViewModel.UserType = user.UserType;

            return userViewModel;
        }

        public async Task Update(SaveUserViewModel userToSave)
        {
            User user = new();
            user.Id = userToSave.Id;
            user.FirstName = userToSave.FirstName;
            user.LastName = userToSave.LastName;
            user.Email = userToSave.Email;
            user.Username = userToSave.Username;
            user.Password = userToSave.Password;
            user.UserType = userToSave.UserType;

            await _repository.UpdateAsync(user);
        }

        public async Task Add(SaveUserViewModel userToCreate)
        {
            User user = new();
            user.Id = userToCreate.Id;
            user.FirstName = userToCreate.FirstName;
            user.LastName = userToCreate.LastName;
            user.Email = userToCreate.Email;
            user.Username = userToCreate.Username;
            user.Password = userToCreate.Password;
            user.UserType = userToCreate.UserType;

            await _repository.AddAsync(user);
        }

        public async Task Delete(int id)
        {
            var user = await _repository.GetByIdAsync(id);
            await _repository.DeleteAsync(user);
        }
    }
}

