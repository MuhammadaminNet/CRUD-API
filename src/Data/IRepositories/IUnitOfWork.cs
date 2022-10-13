using src.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace src.Data.IRepositories
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<User> Users { get; }
        IGenericRepository<Attachment> Attachments { get; }

        Task SaveChangesAsync();
    }
}
