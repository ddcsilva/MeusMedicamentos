using MeusMedicamentos.Application.DTOs;
using MeusMedicamentos.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using MeusMedicamentos.Domain.Entities;

namespace MeusMedicamentos.Web.Controllers
{
    public class AutenticacaoController : Controller
    {
        private readonly IAutenticacaoService _autenticacaoService;
        private readonly SignInManager<Usuario> _signInManager;

        public AutenticacaoController(IAutenticacaoService autenticacaoService, SignInManager<Usuario> signInManager)
        {
            _autenticacaoService = autenticacaoService;
            _signInManager = signInManager;
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

            // Autentica o usuário no Identity
            var user = await _signInManager.UserManager.FindByNameAsync(loginDTO.Usuario);
            await _signInManager.SignInAsync(user, isPersistent: false);

            HttpContext.Session.SetString("JWToken", token);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            HttpContext.Session.Remove("JWToken");
            return RedirectToAction("Login", "Autenticacao");
        }
    }
}
