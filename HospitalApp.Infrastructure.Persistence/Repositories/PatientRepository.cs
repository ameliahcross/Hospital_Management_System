using System;
using HospitalApp.Core.Application.Interfaces.Repositories;
using HospitalApp.Core.Domain.Entities;
using HospitalApp.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace HospitalApp.Infrastructure.Persistence.Repositories
{
	public class PatientRepository : IPatientRepository
	{
        private readonly ApplicationContext _dbContext;

        public PatientRepository(ApplicationContext dbContext)
		{
            _dbContext = dbContext;
        }

        public async Task AddAsync(Patient patient)
        {
            await _dbContext.Set<Patient>().AddAsync(patient);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Patient patient)
        {
            _dbContext.Entry(patient).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Patient patient)
        {
            _dbContext.Set<Patient>().Remove(patient);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Patient>> GetAllAsync()
        {
            return await _dbContext.Set<Patient>().ToListAsync();
        }

        public async Task<Patient> GetByIdAsync(int id)
        {
            return await _dbContext.Set<Patient>().FindAsync(id);
        }
    }
}

