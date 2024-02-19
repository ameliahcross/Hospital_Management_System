using System;
using HospitalApp.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HospitalApp.Core.Application.Interfaces.Repositories
{
	public interface IAppointmentRepository : IGenericRepository<Appointment>
	{
        Task<List<Appointment>> GetAllAsyncWithRelations();
        Task<Appointment> GetByIdAsyncWithRelations(int id);

        Task ChangeAppointmentStatusAsync(int appointmentId, AppointmentStatus newStatus);
        Task<List<LabTest>> GetAvailableLabTestsAsync();
        Task CreateLabResultsAsync(List<LabResult> labResults);
    }
}

