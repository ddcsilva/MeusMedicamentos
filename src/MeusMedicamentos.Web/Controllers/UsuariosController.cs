using MeusMedicamentos.Application.DTOs.Usuario;
using MeusMedicamentos.Application.Interfaces;
using MeusMedicamentos.Domain.Notifications;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MeusMedicamentos.Web.Controllers;

public class UsuariosController : MainController
{
    private readonly IUsuarioService _usuarioService;
    private readonly ILogger<UsuariosController> _logger;

    public UsuariosController(IUsuarioService usuarioService, ILogger<UsuariosController> logger, INotificadorErros notificadorErros)
        : base(notificadorErros)
    {
        _usuarioService = usuarioService;
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginDTO loginDTO)
    {
        if (!ModelState.IsValid)
        {
            NotificarErroModelInvalida();
            return View(loginDTO);
        }

        var token = await _usuarioService.AutenticarAsync(loginDTO.UserName, loginDTO.Senha);

        if (string.IsNullOrEmpty(token))
        {
            ModelState.AddModelError(string.Empty, "Login inválido.");
            return View(loginDTO);
        }

        _logger.LogInformation("Token recebido: {Token}", token);

        // Extraia os claims do token JWT
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);

        var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.NameId);
        var userNameClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.UniqueName);

        _logger.LogInformation("Claims extraídos - NameIdentifier: {NameIdentifier}, Name: {Name}", userIdClaim?.Value, userNameClaim?.Value);

        if (userIdClaim == null || userNameClaim == null || string.IsNullOrEmpty(userIdClaim.Value) || string.IsNullOrEmpty(userNameClaim.Value))
        {
            throw new InvalidOperationException("O token JWT não contém os claims necessários.");
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, userNameClaim.Value),
            new Claim(ClaimTypes.NameIdentifier, userIdClaim.Value)
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var authProperties = new AuthenticationProperties();

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

        // Adicionar o token JWT à sessão e aos cookies
        HttpContext.Session.SetString("JWToken", token);
        Response.Cookies.Append("JWToken", token, new CookieOptions { HttpOnly = true, Secure = true });

        return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        HttpContext.Session.Remove("JWToken");
        Response.Cookies.Delete("JWToken");
        return RedirectToAction("Login", "Usuarios");
    }

    [HttpGet]
    public IActionResult AccessDenied()
    {
        return View();
    }
}
