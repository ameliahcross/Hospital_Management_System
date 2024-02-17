using System;
using HospitalApp.Core.Domain.Entities;

namespace HospitalApp.Core.Application.Interfaces.Repositories
{
	public interface ILabTestRepository
	{
        Task AddAsync(LabTest labTest);
        Task UpdateAsync(LabTest labTest);
        Task DeleteAsync(LabTest labTest);
        Task<List<LabTest>> GetAllAsync();
        Task<LabTest> GetByIdAsync(int id);
    }
}

