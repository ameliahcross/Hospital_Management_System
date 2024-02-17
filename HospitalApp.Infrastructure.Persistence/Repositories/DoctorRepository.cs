﻿using System;
using HospitalApp.Core.Application.Interfaces.Repositories;
using HospitalApp.Core.Domain.Entities;
using HospitalApp.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace HospitalApp.Infrastructure.Persistence.Repositories
{
	public class DoctorRepository : GenericRepository<Doctor>, IDoctorRepository
	{
        private readonly ApplicationContext _dbContext;

        public DoctorRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}

