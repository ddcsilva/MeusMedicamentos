using MeusMedicamentos.Application.DTOs;
using MeusMedicamentos.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Threading.Tasks;
using System.Security.Claims;

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

            // Adicionar o token JWT à sessão e aos cookies
            HttpContext.Session.SetString("JWToken", token);
            Response.Cookies.Append("JWToken", token, new CookieOptions { HttpOnly = true, Secure = true });

            // Autenticar o usuário manualmente usando cookies
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, loginDTO.Usuario),
                new Claim(ClaimTypes.NameIdentifier, loginDTO.Usuario)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties();

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

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
