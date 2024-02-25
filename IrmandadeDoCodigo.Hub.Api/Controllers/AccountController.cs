using IrmandadeDoCodigo.Hub.Api.Extensions;
using IrmandadeDoCodigo.Hub.Api.Services;
using IrmandadeDoCodigo.Hub.Api.ViewModels;
using IrmandadeDoCodigo.Hub.Api.ViewModels.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IrmandadeDoCodigo.Hub.Api.Controllers
{
    [ApiController]
    public class AccountController(UserService userService, TokenService tokenService, EmailService emailService) : ControllerBase
    {

        [HttpPost("v1/account/")]
        public async Task<IActionResult> CreateUser(
            [FromBody] RegisterViewModel model
        )
        {
            if (!ModelState.IsValid) return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));
            try
            {
                var user = await userService.Create(model);
                emailService.Send(user.Name, user.Email, "Bem vindo à Irmandade!", $" Sua senha é <strong>{model.Password}</strong>");
                return Created($"/v1/account/{user.Id}/profile", new ResultViewModel<dynamic>(new { user.Id, user.Email, }));
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
           [FromBody] LoginViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));
            var user = await userService.FindByEmail(model.Email);
            if (user is null) return Unauthorized(new ResultViewModel<string>("Usuário ou senha inválidos"));
            if (!BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash)) return Unauthorized(new ResultViewModel<string>("Usuário ou senha inválidos"));

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
        [HttpGet("v1/account/profile")]
        public async Task<IActionResult> GetProfile()
        {
            try
            {
                var user = await userService.FindProfile(User.Identity.Name);
                return Ok(new ResultViewModel<dynamic>(new { user.Name, user.Id, user.Email }));
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

        [Authorize(Roles = "user")]
        [HttpGet("v1/user")]
        public IActionResult GetUser() => Ok(User.Identity.Name);

        [Authorize(Roles = "admin")]
        [HttpGet("v1/admin")]
        public IActionResult GetAdmin() => Ok(User.Identity.Name);
    }
}
