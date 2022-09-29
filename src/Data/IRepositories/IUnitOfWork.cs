using Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Data.IRepositories
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<User> Users { get; }
        IGenericRepository<Attachment> Attachments { get; }

        Task SaveChangesAsync();
    }
}
