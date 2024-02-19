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

        public async Task ChangeAppointmentStatusAsync(int appointmentId, AppointmentStatus newStatus)
        {
            var appointment = await _dbContext.Appointments.FindAsync(appointmentId);
            if (appointment != null)
            {
                appointment.Status = newStatus;
                _dbContext.Update(appointment);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<List<LabTest>> GetAvailableLabTestsAsync()
        {
            return await _dbContext.LabTests.ToListAsync();
        }

        public async Task CreateLabResultsAsync(List<LabResult> labResults)
        {
            foreach (var labResult in labResults)
            {
                _dbContext.LabResults.Add(labResult);
            }
            await _dbContext.SaveChangesAsync();
        }

    }
}
