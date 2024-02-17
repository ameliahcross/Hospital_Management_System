using System;
using HospitalApp.Core.Domain.Entities;

namespace HospitalApp.Core.Application.Interfaces.Repositories
{
	public interface IPatientRepository : IGenericRepository<Patient>
    {
        // Agregar las firmas de los methods nuevos que no tiene GenericRepository
    }
}

