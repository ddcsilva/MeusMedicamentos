using Microsoft.AspNetCore.Mvc;
using MeusMedicamentos.Domain.Notifications;
using MeusMedicamentos.Shared;

namespace MeusMedicamentos.Web.Controllers;

public abstract class MainController : Controller
{
    private readonly INotificadorErros _notificadorErros;

    protected MainController(INotificadorErros notificadorErros)
    {
        _notificadorErros = notificadorErros;
    }

    protected bool OperacaoValida()
    {
        return !_notificadorErros.TemNotificacoes();
    }

    protected void NotificarErro(string mensagem)
    {
        _notificadorErros.Handle(new NotificacaoErros(mensagem));
    }

    protected void NotificarErroModelInvalida()
    {
        var erros = ModelState.Values.SelectMany(e => e.Errors);
        foreach (var erro in erros)
        {
            var errorMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
            NotificarErro(errorMsg);
        }
    }

    protected IActionResult CustomResponse<T>(ApiResponse<T> response)
    {
        if (response.Success)
        {
            return View(response.Data);
        }

        foreach (var error in response.Errors)
        {
            ModelState.AddModelError(string.Empty, error);
        }

        return View();
    }

    protected IActionResult CustomResponse<T>(ApiResponse<T> response, object model)
    {
        if (response.Success)
        {
            return RedirectToAction(nameof(Index));
        }

        foreach (var error in response.Errors)
        {
            ModelState.AddModelError(string.Empty, error);
        }

        return View(model);
    }
}
