using System;
using System.Numerics;
using HospitalApp.Core.Application.Interfaces.Repositories;
using HospitalApp.Core.Domain.Entities;
using HospitalApp.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace HospitalApp.Infrastructure.Persistence.Repositories
{
	public class AppointmentRepository : GenericRepository<Appointment>, IAppointmentRepository
	{
        private readonly ApplicationContext _dbContext;

        public AppointmentRepository(ApplicationContext dbContext) : base(dbContext)
		{
            _dbContext = dbContext;
        }

        public async Task<List<Appointment>> GetAllAsyncWithRelations()
        {
            return await _dbContext.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .ToListAsync();
        }

        public async Task<Appointment> GetByIdAsyncWithRelations(int id)
        {
            return await _dbContext.Appointments
                 .Include(a => a.Doctor)
                 .Include(a => a.Patient)
                 .FirstOrDefaultAsync(a => a.Id == id);
        }

    }
}
