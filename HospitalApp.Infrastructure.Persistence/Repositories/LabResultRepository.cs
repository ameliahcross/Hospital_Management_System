using System;
using HospitalApp.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using HospitalApp.Infrastructure.Persistence.Contexts;
using HospitalApp.Core.Application.Interfaces.Repositories;

namespace HospitalApp.Infrastructure.Persistence.Repositories
{
	public class LabResultRepository : GenericRepository<LabResult>, ILabResultRepository
    {
        private readonly ApplicationContext _dbContext;

        public LabResultRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<LabResult>> GetAllAsyncWithInclude()
        {
            return await _dbContext.LabResults.Include(lr => lr.LabTest)
                           .Include(lr => lr.Appointment)
                                .ThenInclude(a => a.Patient)
                           .ToListAsync();
        }

        public async Task<List<LabResult>> GetLabResultByAppointmentIdAsync(int appointmentId)
        {
            return await _dbContext.LabResults
                                   .Include(lr => lr.LabTest)
                                   .Include(lr => lr.Appointment)
                                        .ThenInclude(app => app.Patient)
                                    .Where(lr => lr.AppointmentId == appointmentId)
                                   .ToListAsync();
        }

        public async Task<List<LabResult>> GetCompletedAsync()
        {
            return await _dbContext.LabResults.Where(lr => lr.Status == LabResultStatus.Completado)
                .Include(lr => lr.Appointment)
                .ToListAsync();
        }

        public async Task<List<LabResult>> GetAllFilteredAsync(string cedula)
        {
            IQueryable<LabResult> query = _dbContext.LabResults
                .Include(lr => lr.Appointment)
                    .ThenInclude(a => a.Patient)
                .Include(lr => lr.LabTest);

            if (!string.IsNullOrEmpty(cedula))
            {
                query = query.Where(lr => lr.Appointment.Patient.IdentificationNumber == cedula);
            }

            var labResults = await query.ToListAsync();
            labResults = labResults
                .Where(lr => lr.Appointment != null && lr.Appointment.Patient != null)
                .ToList();

            return labResults;
        }



    }
}

