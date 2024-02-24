using HospitalApp.Core.Application.ViewModels.User;
using HospitalApp.Core.Domain.Entities;

namespace HospitalApp.Core.Application.Interfaces.Repositories
{
	public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> LoginAsync(LoginViewModel loginVm);
        Task<User> GetByUsername(string username);
    }
}

