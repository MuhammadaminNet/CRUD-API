using src.Data.DbContexts;
using src.Data.IRepositories;
using src.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace src.Data.Repositories
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
