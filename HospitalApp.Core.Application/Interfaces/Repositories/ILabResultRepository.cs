using HospitalApp.Core.Domain.Entities;

namespace HospitalApp.Core.Application.Interfaces.Repositories
{
	public interface ILabResultRepository : IGenericRepository<LabResult>
    {
        Task<List<LabResult>> GetAllAsyncWithInclude();
        Task<List<LabResult>> GetLabResultByAppointmentIdAsync(int appointmentId);
        Task<List<LabResult>> GetCompletedAsync();
        Task<List<LabResult>> GetAllFilteredAsync(string cedula);
    }
}

