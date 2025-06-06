﻿using DataAccessLayer.DatabaseContext;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly CrowdfundingDbContext _ctx;

        public Repository(CrowdfundingDbContext ctx) 
        { 
            _ctx = ctx;
        }

        public async Task<ICollection<T>> GetAllAsync()
        {
            return await _ctx.Set<T>().ToListAsync();
        }

        public async Task<ICollection<T>> GetAllWithPageAsync(int pageNumber, int pageSize)
        {
            return await _ctx.Set<T>()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<T?> GetAsyncById(Guid id)
        {
            return await _ctx.Set<T>().FindAsync(id);
        }

        public async Task<Guid> CreateAsync(T entity)
        {
            await _ctx.Set<T>().AddAsync(entity);
            await _ctx.SaveChangesAsync();

            return entity.Id;
        }

        public async Task<Guid> UpdateAsync(T entity)
        {
            _ctx.Set<T>().Update(entity);
            await _ctx.SaveChangesAsync();

            return entity.Id;
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _ctx.Set<T>().FindAsync(id);

            if(entity == null) return;

            _ctx.Set<T>().Remove(entity);
            await _ctx.SaveChangesAsync();
        }

        
    }
}
