using Data.IRepositories;
using Domain.Configurations;
using Domain.Entities;
using Domain.Enums;
using Services.DTOs;
using Services.Exceptions;
using Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unit;
        private readonly IAttachmentService _attachmentService;
        public UserService(IUnitOfWork unit, IAttachmentService attachmentService)
        {
            _unit = unit;
            _attachmentService = attachmentService;
        }

        public async Task<User> CreateAsync(UserForCreation dto)
        {
            // uploading file to wwwroot
            var attachment = await _attachmentService.UploadAsync(dto.File.OpenReadStream(), dto.File.FileName);


            // store user to database
            return await _unit.Users.CreateAsync(new User()
            {
                Name = dto.Name,
                Age = dto.Age,
                Login = dto.Login,
                Password = dto.Password,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                FileId = attachment.Id,
                Role = UserRole.User
            });
        }

        public async Task<bool> DeleteAsync(Expression<Func<User, bool>> expression)
        {
            var res = await _unit.Users.GetAsync(expression);

            if (res == null)
            {
                throw new UserException(404, "Not Found");
            }

            await _unit.Users.DeleteAsync(res);

            return true;
        }

        public async Task<IEnumerable<User>> GetAllAsync(PaginationParams @params, Expression<Func<User, bool>> expression = null)
        {
            var all = _unit.Users.GetAll(expression).ToList();

            if (all != null)/*
                return all.Skip((@params.PageIndex - 1) * @params.PageSize)
                     .Take(@params.PageSize).ToList();*/
                return all;

            return null;
        }

        public Task<User> GetAsync(Expression<Func<User, bool>> expression)
            => _unit.Users.GetAsync(expression, "Attachment");

        public async Task<User> UpdateAsync(Expression<Func<User, bool>> expression, UserForCreation user)
        {
            var res = await _unit.Users.GetAsync(expression);

            if (res == null)
                return null;

            res.Name = user.Name;
            res.Age = user.Age;
            res.Login = user.Login;
            res.Password = user.Password;

            return await _unit.Users.UpdateAsync(res);
        }
    }
}
