using IrmandadeDoCodigo.Hub.Api.Models;
using IrmandadeDoCodigo.Hub.Api.Repositories;
using IrmandadeDoCodigo.Hub.Api.ViewModels.Account;

namespace IrmandadeDoCodigo.Hub.Api.Services
{
    public class UserService(UserRepository repository)
    {
        public async Task<User> Create(RegisterViewModel model)
        {
            var user = await repository.Create(model);
            return user;
        }

        public async Task<User?> FindByEmail(string email)
        {
            var user = await repository.FindByEmail(email);
            return user;
        }

        public async Task<User?> FindProfile(string email)
        {
            var user = await repository.FindByEmailWithoutRoles(email);
            return user;
        }
    }
}
