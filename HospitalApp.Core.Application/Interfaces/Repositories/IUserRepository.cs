using System;
using HospitalApp.Core.Domain.Entities;

namespace HospitalApp.Core.Application.Interfaces.Repositories
{
	public interface IUserRepository : IGenericRepository<User>
    {
        // Agregar las firmas de los methods nuevos que no tiene GenericRepository
    }
}

