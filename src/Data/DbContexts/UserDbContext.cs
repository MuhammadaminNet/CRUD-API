using Microsoft.EntityFrameworkCore;
using src.Domain.Entities;

namespace src.Data.DbContexts
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Attachment> Attachments { get; set; }

    }
}
