using System;
using HospitalApp.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HospitalApp.Core.Application.Interfaces.Repositories
{
	public interface IAppointmentRepository
	{
        Task AddAsync(Appointment appointment);

        Task UpdateAsync(Appointment appointment);

        Task DeleteAsync(Appointment appointment);

        Task<List<Appointment>> GetAllAsync();

        Task<Appointment> GetByIdAsync(int id);
    }
}

