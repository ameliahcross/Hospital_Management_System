using System;
using HospitalApp.Core.Application.ViewModels.User;

namespace HospitalApp.Core.Application.Interfaces.Services
{
	public interface IUserService : IGenericService<UserViewModel, SaveUserViewModel>
    {

    }
}

