using System;
using HospitalApp.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HospitalApp.Core.Application.Interfaces.Repositories
{
	public interface IAppointmentRepository : IGenericRepository<Appointment>
	{
        
    }
}

