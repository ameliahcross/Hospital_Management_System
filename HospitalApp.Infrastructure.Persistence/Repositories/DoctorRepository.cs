using System;
using HospitalApp.Core.Application.Interfaces.Repositories;
using HospitalApp.Core.Domain.Entities;
using HospitalApp.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace HospitalApp.Infrastructure.Persistence.Repositories
{
	public class DoctorRepository : IDoctorRepository
	{
        private readonly ApplicationContext _dbContext;

        public DoctorRepository(ApplicationContext dbContext)
		{
            _dbContext = dbContext;
        }

        public async Task AddAsync(Doctor doctor)
        {
            await _dbContext.Set<Doctor>().AddAsync(doctor);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Doctor doctor)
        {
            _dbContext.Entry(doctor).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Doctor doctor)
        {
            _dbContext.Set<Doctor>().Remove(doctor);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Doctor>> GetAllAsync()
        {
            return await _dbContext.Set<Doctor>().ToListAsync();
        }

        public async Task<Doctor> GetByIdAsync(int id)
        {
            return await _dbContext.Set<Doctor>().FindAsync(id);
        }
    }
}

