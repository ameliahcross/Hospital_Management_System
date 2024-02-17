using System;
using HospitalApp.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using HospitalApp.Infrastructure.Persistence.Contexts;
using HospitalApp.Core.Application.Interfaces.Repositories;

namespace HospitalApp.Infrastructure.Persistence.Repositories
{
	public class LabResultRepository : ILabResultRepository
    {
        private readonly ApplicationContext _dbContext;

        public LabResultRepository(ApplicationContext dbContext)
		{
            _dbContext = dbContext;
        }

        public async Task AddAsync(LabResult labResult)
        {
            await _dbContext.Set<LabResult>().AddAsync(labResult);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(LabResult labResult)
        {
            _dbContext.Entry(labResult).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(LabResult labResult)
        {
            _dbContext.Set<LabResult>().Remove(labResult);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<LabResult>> GetAllAsync()
        {
            return await _dbContext.Set<LabResult>().ToListAsync();
        }

        public async Task<LabResult> GetByIdAsync(int id)
        {
            return await _dbContext.Set<LabResult>().FindAsync(id);
        }
    }
}

