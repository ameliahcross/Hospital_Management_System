using System;
using HospitalApp.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HospitalApp.Infrastructure.Persistence.Repositories
{
	public class LabTestRepository
	{
        private readonly ApplicationContext _dbContext;

        public LabTestRepository(ApplicationContext dbContext)
		{
            _dbContext = dbContext;
        }

        public async Task AddAsync(LabTest labTest)
        {
            await _dbContext.Set<LabTest>().AddAsync(labTest);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(LabTest labTest)
        {
            _dbContext.Entry(labTest).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(LabTest labTest)
        {
            _dbContext.Set<LabTest>().Remove(labTest);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<LabTest>> GetAllAsync()
        {
            return await _dbContext.Set<LabTest>().ToListAsync();
        }

        public async Task<LabTest> GetByIdAsync(int id)
        {
            return await _dbContext.Set<LabTest>().FindAsync(id);
        }
    }
}

