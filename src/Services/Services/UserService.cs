using AutoMapper;
using src.Data.IRepositories;
using src.Domain.Configurations;
using src.Domain.Entities;
using src.Domain.Enums;
using src.Services.DTOs;
using src.Services.Exceptions;
using src.Services.Extentions;
using src.Services.IServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace src.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unit;
        private readonly IAttachmentService _attachmentService;
        private readonly IMapper mapper;
        public UserService(IUnitOfWork unit, 
            IAttachmentService attachmentService,IMapper mapper)
        {
            _unit = unit;
            _attachmentService = attachmentService;
            this.mapper = mapper;
        }

        public async Task<User> CreateAsync(UserForCreation dto,Stream stream = null,string name = null)
        {
            User user = mapper.Map<User>(dto);
            user.Create();

            if(stream is not null)
                user.FileId = (await _attachmentService.UploadAsync(stream,name)).Id;

            return await _unit.Users.CreateAsync(user);
        }

        public async Task<bool> DeleteAsync(Expression<Func<User, bool>> expression)
        {
            var res = await _unit.Users.GetAsync(expression);

            if (res == null)
                return false;

            await _unit.Users.DeleteAsync(res);

            return true;
        }

        public async Task<IEnumerable<User>> GetAllAsync(PaginationParams @params = null, Expression<Func<User, bool>> expression = null)
            => _unit.Users.GetAll(expression).ToList();

        public Task<User> GetAsync(Expression<Func<User, bool>> expression)
            => _unit.Users.GetAsync(expression);

        public async Task<User> UpdateAsync(Expression<Func<User, bool>> expression, UserForCreation dto)
        {
            var res = await _unit.Users.GetAsync(expression);

            if (res == null)
                return null;

            res = mapper.Map(dto, res);
            res.Update();

            return await _unit.Users.UpdateAsync(res);
        }
    }
}
