using System;
using HospitalApp.Core.Domain.Entities;

namespace HospitalApp.Core.Application.Interfaces.Repositories
{
	public interface IPatientRepository
	{
        Task AddAsync(Patient patient);
        Task UpdateAsync(Patient patient);
        Task DeleteAsync(Patient patient);
        Task<List<Patient>> GetAllAsync();
        Task<Patient> GetByIdAsync(int id);
    }
}

