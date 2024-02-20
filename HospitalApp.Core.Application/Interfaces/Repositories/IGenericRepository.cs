﻿using System;
using Microsoft.EntityFrameworkCore;

namespace HospitalApp.Core.Application.Interfaces.Repositories
{
	public interface IGenericRepository<Entity> where Entity : class
    {
        Task AddAsync(Entity entity);
        Task UpdateAsync(Entity entity);
        Task DeleteAsync(Entity entity);
        Task<List<Entity>> GetAllAsync();
        Task<Entity> GetByIdAsync(int id);
    }
}

