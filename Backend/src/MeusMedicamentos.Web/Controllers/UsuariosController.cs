using MeusMedicamentos.Application.DTOs.Usuario;
using MeusMedicamentos.Application.Interfaces;
using MeusMedicamentos.Domain.Notifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace MeusMedicamentos.Web.Controllers
{
    [Authorize]
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
        public async Task<IActionResult> Index()
        {
            var response = await _usuarioService.ObterTodosUsuariosAsync();
            return CustomResponse(response);
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var response = await _usuarioService.ObterPorIdAsync(id);
            return CustomResponse(response);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CriarUsuarioDTO usuarioDTO)
        {
            if (!ModelState.IsValid)
            {
                NotificarErroModelInvalida();
                return View(usuarioDTO);
            }

            var response = await _usuarioService.CriarUsuarioAsync(usuarioDTO.UserName, usuarioDTO.Nome, usuarioDTO.Email);
            return CustomResponse(response, usuarioDTO);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var response = await _usuarioService.ObterPorIdAsync(id);
            if (!response.Success)
            {
                return RedirectToAction(nameof(Index));
            }

            var usuarioDTO = new EditarUsuarioDTO(response.Data.Id, response.Data.UserName, response.Data.Nome, response.Data.Email);
            return View(usuarioDTO);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, EditarUsuarioDTO usuarioDTO)
        {
            if (id != usuarioDTO.Id)
            {
                ModelState.AddModelError(string.Empty, "Id da requisição difere do Id do objeto.");
                return View(usuarioDTO);
            }

            if (!ModelState.IsValid)
            {
                NotificarErroModelInvalida();
                return View(usuarioDTO);
            }

            var response = await _usuarioService.AtualizarUsuarioAsync(id, usuarioDTO.UserName, usuarioDTO.Nome, usuarioDTO.Email);
            return CustomResponse(response, usuarioDTO);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await _usuarioService.RemoverUsuarioAsync(id);
            if (response.Success)
            {
                return Ok();
            }

            return BadRequest(response.Errors);
        }
    }
}
