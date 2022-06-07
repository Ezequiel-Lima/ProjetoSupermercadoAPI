using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using ProjetoSupermercado.Infrastructure.Data;
using ProjetoSupermercado.Models;
using System.Security.Claims;

namespace ProjetoSupermercado.Controllers;

[Route("api/[controller]"), ApiController]
public sealed class LoginController : ControllerBase
{
    private readonly IReadRepository<User> _repository;

    public LoginController(IReadRepository<User> repository)
    {
        _repository = repository;
    }

    [HttpPost]
    public async Task<ActionResult> Login(User teste)
    {
        User userLogged =
            _repository.FindByCondition(user => user.Login == teste.Login && user.Password == teste.Password).FirstOrDefault();

        if (userLogged == null)
        {
            return NotFound("Usuario inexistente");
        }

        var claims = new List<Claim>();

        claims.Add(new Claim(ClaimTypes.Name, userLogged.Name!));

        claims.Add(new Claim(ClaimTypes.Sid, userLogged.Id.ToString()));

        var UserIdentity = new ClaimsIdentity(claims, "Acesso");

        ClaimsPrincipal principal = new ClaimsPrincipal(UserIdentity);

        await HttpContext.SignInAsync("CookieAuthentication", principal, new AuthenticationProperties());

        return Ok();
    }

    [HttpPost("teste")]
    public async Task<IActionResult> Logoff()
    {
        await HttpContext.SignOutAsync("CookieAuthentication");
        return Ok();
    }
}