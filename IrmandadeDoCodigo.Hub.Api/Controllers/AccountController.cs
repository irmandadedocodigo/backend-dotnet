using IrmandadeDoCodigo.Hub.Api.Data;
using IrmandadeDoCodigo.Hub.Api.Extensions;
using IrmandadeDoCodigo.Hub.Api.Models;
using IrmandadeDoCodigo.Hub.Api.Services;
using IrmandadeDoCodigo.Hub.Api.ViewModels;
using IrmandadeDoCodigo.Hub.Api.ViewModels.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IrmandadeDoCodigo.Hub.Api.Controllers
{
    [ApiController]
    public class AccountController : ControllerBase
    {

        [HttpPost("v1/account/")]
        public async Task<IActionResult> Post(
            [FromBody] RegisterViewModel model,
            [FromServices] TokenService tokenService,
            [FromServices] AppDataContext context,
            [FromServices] EmailService emailService
        )
        {
            if (!ModelState.IsValid) return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));
            var user = new User()
            {
                Name = model.Name,
                Email = model.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password)
            };


            try
            {
                context.Users.Add(user);
                await context.SaveChangesAsync();

                emailService.Send(user.Name, user.Email, "Bem vindo à Irmandade!", $" Sua senha é <strong>{model.Password}</strong>");

                return Ok(new ResultViewModel<dynamic>(new
                {
                    user.Email,
                }));
            }
            catch (DbUpdateException ex)
            {
                return BadRequest(new ResultViewModel<string>("Este email já está cadastrado."));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<string>("E002 - Falha interna no servidor."));
            }

        }

        [HttpPost("v1/account/login")]
        public async Task<IActionResult> Login(
            [FromBody] LoginViewModel model,
            [FromServices] AppDataContext context,
            [FromServices] TokenService tokenService)
        {
            if (!ModelState.IsValid) return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));

            var user = await context
                .Users
                .AsNoTracking()
                .Include(user => user.Roles)
                .FirstOrDefaultAsync(x => x.Email == model.Email);

            if (user == null)
                return Unauthorized(new ResultViewModel<string>("Usuário ou senha inválidos"));

            if (!BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
                return Unauthorized(new ResultViewModel<string>("Usuário ou senha inválidos"));

            try
            {
                var token = tokenService.GenerateToken(user);
                return Ok(new ResultViewModel<dynamic>(new { token }, null));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<string>("05X04 - Falha interna no servidor"));
            }
        }


        [Authorize(Roles = "user")]
        [HttpGet("v1/user")]
        public IActionResult GetUser() => Ok(User.Identity.Name);

        [Authorize(Roles = "author")]
        [HttpGet("v1/author")]
        public IActionResult GetAuthor() => Ok(User.Identity.Name);

        [Authorize(Roles = "admin")]
        [HttpGet("v1/admin")]
        public IActionResult GetAdmin() => Ok(User.Identity.Name);
    }
}
