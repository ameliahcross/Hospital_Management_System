using System;
using HospitalApp.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HospitalApp.Core.Application.Interfaces.Repositories
{
	public interface ILabResultRepository
	{
        Task AddAsync(LabResult labResult);

        Task UpdateAsync(LabResult labResult);

        Task DeleteAsync(LabResult labResult);

        Task<List<LabResult>> GetAllAsync();

        Task<LabResult> GetByIdAsync(int id);
    }
}

