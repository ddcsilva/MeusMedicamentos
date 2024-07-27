using MeusMedicamentos.Application.DTOs;
using MeusMedicamentos.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace MeusMedicamentos.Web.Controllers
{
    public class AutenticacaoController : Controller
    {
        private readonly IAutenticacaoService _autenticacaoService;

        public AutenticacaoController(IAutenticacaoService autenticacaoService)
        {
            _autenticacaoService = autenticacaoService;
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
                return View(loginDTO);
            }

            var token = await _autenticacaoService.AutenticarAsync(loginDTO.Usuario, loginDTO.Senha);

            if (string.IsNullOrEmpty(token))
            {
                ModelState.AddModelError(string.Empty, "Login inválido.");
                return View(loginDTO);
            }

            // Extraia os claims do token JWT
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.NameId);
            var userNameClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.UniqueName);

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
            return RedirectToAction("Login", "Autenticacao");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
