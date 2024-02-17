using System;
using HospitalApp.Core.Domain.Entities;

namespace HospitalApp.Core.Application.Interfaces.Repositories
{
	public interface IDoctorRepository : IGenericRepository<Doctor>
    {
        // Agregar las firmas de los methods nuevos que no tiene GenericRepository
    }
}

