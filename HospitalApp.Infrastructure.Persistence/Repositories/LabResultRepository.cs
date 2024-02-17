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
    }
}

