using Application.Contracts;
using Domain.Entities.Base;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Persistence
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private ApplicationDbContext _context;
        private DbSet<T> _dbSet;
        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<T> AddAsync(T entity, CancellationToken cancellationToken)
        {
             await _dbSet.AddAsync(entity, cancellationToken);
            return  await Task.FromResult(entity);
        }

        public async void Delete(T entity , CancellationToken cancellationToken)
        {
            var record = await GetByIdAsync(entity.Id, cancellationToken);
            record.IsDeleted = true;
            await UpdateAsync(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _dbSet.ToListAsync(cancellationToken);
        }

        public async Task<T> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Id == id  , cancellationToken); 
        }

        public Task<T> UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            return Task.FromResult(entity);
        }
        public async Task<bool> AnyAsync(Expression<Func<T , bool >> expression , CancellationToken cancellationToken)
        {
            return await _dbSet.AnyAsync(expression , cancellationToken);
        }
        public async Task<bool> AnyAsync(CancellationToken cancellationToken)
        {
            return await _dbSet.AnyAsync(cancellationToken);
        }
    }
}
