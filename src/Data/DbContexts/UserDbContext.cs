using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.DbContexts
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {
        }
        public DbSet<User> Userss { get; set; }
        public DbSet<Attachment> Attachments { get; set; }

    }
}
