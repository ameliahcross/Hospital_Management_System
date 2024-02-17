using System;
using HospitalApp.Core.Domain.Entities;

namespace HospitalApp.Core.Application.Interfaces.Repositories
{
	public interface IDoctorRepository
	{
        Task AddAsync(Doctor doctor);

        Task UpdateAsync(Doctor doctor);

        Task DeleteAsync(Doctor doctor);

        Task<List<Doctor>> GetAllAsync();

        Task<Doctor> GetByIdAsync(int id);
    }
}

