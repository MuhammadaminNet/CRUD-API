using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Services.Mappers;
using src.Data.DbContexts;
using src.Data.IRepositories;
using src.Data.Repositories;
using src.Domain.Entities;
using src.Services.DTOs;
using src.Services.IServices;
using src.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yanabitta.Test.Unit.Services
{
    public partial class UserServiceTest
    {
        private readonly IUnitOfWork unitOfWorkMock;
        private readonly IUserService userService;
        private readonly IMapper mapper;
        private readonly IAttachmentService attachmentService;
        private readonly UserDbContext dbContext;
        private User user;


        public UserServiceTest()
        {
            var options = new DbContextOptionsBuilder<UserDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            this.mapper = new MapperConfiguration(m =>
            {
                m.AddProfile<MappingProfile>();
            }).CreateMapper();

            this.dbContext = new UserDbContext(options);
            this.unitOfWorkMock = new UnitOfWork(dbContext);
            this.attachmentService = new AttachmentService(unitOfWorkMock);
            this.userService = new UserService(unitOfWorkMock,attachmentService,mapper);
            this.user = new User();
        }

        private UserForCreation CreateRandomUser()
            => new UserForCreation()
            {
                Name = Faker.Name.First(),
                Age = Faker.RandomNumber.Next(10, 30),
                Login = Faker.Name.Last(),
                Password = Faker.Name.Last(),
                Role = src.Domain.Enums.UserRole.User
            };


        private Attachment CreateRandomAttachment()
            => new Attachment()
            {
                Name = Faker.Name.First(),
                Path = Faker.Name.Prefix()
            };
    }
}
