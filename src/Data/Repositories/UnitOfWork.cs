using Data.DbContexts;
using Data.IRepositories;
using Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public UserDbContext context;
        public IGenericRepository<User> Users { get; }
        public IGenericRepository<Attachment> Attachments { get; }

        public UnitOfWork(UserDbContext context)
        {
            this.context = context;
            Users = new GenericRepository<User>(context);
            Attachments = new GenericRepository<Attachment>(context);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task SaveChangesAsync()
        {
            context.SaveChanges();
        }
    }
}
