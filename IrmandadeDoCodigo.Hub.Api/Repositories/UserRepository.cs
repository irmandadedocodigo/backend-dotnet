using IrmandadeDoCodigo.Hub.Api.Data;
using IrmandadeDoCodigo.Hub.Api.Models;
using IrmandadeDoCodigo.Hub.Api.ViewModels.Account;
using Microsoft.EntityFrameworkCore;

namespace IrmandadeDoCodigo.Hub.Api.Repositories
{
    public class UserRepository(AppDataContext context)
    {
        public async Task<User> Create(RegisterViewModel model)
        {
            var user = new User()
            {
                Name = model.Name,
                Email = model.Email.ToLower(),
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password)
            };
            using (var transaction = context.Database.BeginTransaction())
            {
                var role = await context.Roles.FirstOrDefaultAsync(x => x.Slug == "user");
                user.Roles = [role];
                context.Users.Add(user);
                context.SaveChanges();
                transaction.Commit();
            }
            return user;
        }

        public async Task<User?> FindByEmail(string email) => await context
            .Users
            .AsNoTracking()
            .Include(user => user.Roles)
            .FirstOrDefaultAsync(x => x.Email == email.ToLower());

        public async Task<User?> FindByEmailWithoutRoles(string email) => await context
            .Users
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Email == email.ToLower());

        public async Task<User?> FindById(string id) => await context
            .Users
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == new Guid(id));
    }
}
