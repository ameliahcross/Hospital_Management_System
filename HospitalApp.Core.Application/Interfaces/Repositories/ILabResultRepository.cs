using System;
using HospitalApp.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HospitalApp.Core.Application.Interfaces.Repositories
{
	public interface ILabResultRepository : IGenericRepository<LabResult>
    {
        // Agregar las firmas de los methods nuevos que no tiene GenericRepository
        Task<List<LabResult>> GetAllAsyncWithInclude();
        Task<List<LabResult>> GetLabResultByAppointmentIdAsync(int appointmentId);
    }
}

