using System;
using HospitalApp.Core.Application.Interfaces.Repositories;
using HospitalApp.Core.Domain.Entities;
using HospitalApp.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace HospitalApp.Infrastructure.Persistence.Repositories
{
	public class UserRepository : IUserRepository
    {
        private readonly ApplicationContext _dbContext;

        public UserRepository(ApplicationContext dbContext)
		{
            _dbContext = dbContext;
        }

        public async Task AddAsync(User user)
        {
            await _dbContext.Set<User>().AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            _dbContext.Entry(user).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(User user)
        {
            _dbContext.Set<User>().Remove(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _dbContext.Set<User>().ToListAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _dbContext.Set<User>().FindAsync(id);
        }
    }
}

