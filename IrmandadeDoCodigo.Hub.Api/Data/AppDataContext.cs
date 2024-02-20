using IrmandadeDoCodigo.Hub.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace IrmandadeDoCodigo.Hub.Api.Data
{
    public class AppDataContext : DbContext
    {
        public AppDataContext(DbContextOptions<AppDataContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }

    }
}
