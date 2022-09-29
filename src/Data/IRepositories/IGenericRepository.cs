using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Data.IRepositories
{
    public interface IGenericRepository<TSource>
    {
        public Task<TSource> CreateAsync(TSource entity);
        public Task<TSource> UpdateAsync(TSource entity);
        public Task DeleteAsync(TSource entity);
        public Task<TSource> GetAsync(Expression<Func<TSource, bool>> expression, string include = null);
        public IQueryable<TSource> GetAll(Expression<Func<TSource, bool>> expression, string include = null, bool isTracking = true);
    }
}
