using IrmandadeDoCodigo.Hub.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace IrmandadeDoCodigo.Hub.Api.Data
{
    public class AppDataContext : DbContext
    {
        public AppDataContext(DbContextOptions<AppDataContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Reaction> Reactions { get; set; }
    }
}
