using MeusMedicamentos.Application.DTOs;
using MeusMedicamentos.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

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

            HttpContext.Session.SetString("JWToken", token);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("JWToken");
            return RedirectToAction("Login", "Autenticacao");
        }
    }
}
