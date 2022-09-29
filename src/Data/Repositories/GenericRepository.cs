using Data.DbContexts;
using Data.IRepositories;
using Domain.Commons;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class GenericRepository<TSource> : IGenericRepository<TSource> where TSource : Auditable
    {
        private readonly UserDbContext _context;

        private readonly DbSet<TSource> db;

        public GenericRepository(UserDbContext context)
        {
            _context = context;
            db = _context.Set<TSource>();
        }

        public async Task<TSource> CreateAsync(TSource entity)
        {
            var created = db.Add(entity).Entity;

            await _context.SaveChangesAsync();

            return created;
        }

        public async Task DeleteAsync(TSource entity)
        {
            db.Remove(entity);

            await _context.SaveChangesAsync();
        }

        public IQueryable<TSource> GetAll(Expression<Func<TSource, bool>> expression, string include = null, bool isTracking = true)
        {
            IQueryable<TSource> query = expression is null
                ? db.Where(t => t.State != State.Deleted)
                : db.Where(t => t.State != State.Deleted).Where(expression);

            if (!string.IsNullOrEmpty(include) && query != null)
                query = query.Include(include);

            if (!isTracking && query != null)
                query = query.AsNoTracking();

            return query;
        }

        public async Task<TSource> GetAsync(Expression<Func<TSource, bool>> expression, string include = null)
            => await GetAll(expression, include).FirstOrDefaultAsync();

        public async Task<TSource> UpdateAsync(TSource entity)
        {
            var updated = db.Update(entity).Entity;

            await _context.SaveChangesAsync();

            return updated;
        }
    }
}
