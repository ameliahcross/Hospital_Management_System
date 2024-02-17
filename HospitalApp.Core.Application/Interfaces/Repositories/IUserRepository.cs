using System;
using HospitalApp.Core.Domain.Entities;

namespace HospitalApp.Core.Application.Interfaces.Repositories
{
	public interface IUserRepository
	{
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(User user);
        Task<List<User>> GetAllAsync();
        Task<User> GetByIdAsync(int id);
    }
}

