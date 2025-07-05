using Microsoft.EntityFrameworkCore;
using MyoX.Domain.Entities.User;

namespace MyoX.Infrastructure.Database
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<UserTokenEntity> UserToken { get; set; }
    }
}
