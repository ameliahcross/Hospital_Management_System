using System;
using HospitalApp.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using HospitalApp.Infrastructure.Persistence.Contexts;
using HospitalApp.Core.Application.Interfaces.Repositories;

namespace HospitalApp.Infrastructure.Persistence.Repositories
{
	public class LabTestRepository : GenericRepository<LabTest>, ILabTestRepository
    {
        private readonly ApplicationContext _dbContext;

        public LabTestRepository(ApplicationContext dbContext) : base(dbContext)
		{
            _dbContext = dbContext;
        }
    }
}

