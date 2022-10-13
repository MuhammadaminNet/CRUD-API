using src.Domain.Configurations;
using src.Domain.Entities;
using src.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace src.Services.IServices
{
    public interface IUserService
    {
        Task<User> CreateAsync(UserForCreation user);
        Task<User> UpdateAsync(Expression<Func<User, bool>> expression, UserForCreation user);
        Task<bool> DeleteAsync(Expression<Func<User, bool>> expression);
        Task<User> GetAsync(Expression<Func<User, bool>> expression);
        Task<IEnumerable<User>> GetAllAsync(PaginationParams @params = null, Expression<Func<User, bool>> expression = null);
    }
}
