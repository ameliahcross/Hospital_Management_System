using System;
using System.Numerics;
using HospitalApp.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HospitalApp.Infrastructure.Persistence.Repositories
{
	public class AppointmentRepository
	{
        private readonly ApplicationContext _dbContext;

        public AppointmentRepository(ApplicationContext dbContext)
		{
            _dbContext = dbContext;
        }

        public async Task AddAsync(Appointment appointment)
        {
            await _dbContext.Set<Appointment>().AddAsync(appointment);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Appointment appointment)
        {
            _dbContext.Entry(appointment).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Appointment appointment)
        {
            _dbContext.Set<Appointment>().Remove(appointment);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Appointment>> GetAllAsync()
        {
            return await _dbContext.Set<Appointment>().ToListAsync();
        }

        public async Task<Appointment> GetByIdAsync(int id)
        {
            return await _dbContext.Set<Appointment>().FindAsync(id);
        }
    }
}

