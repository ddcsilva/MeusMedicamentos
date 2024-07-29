using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MeusMedicamentos.Domain.Notifications;
using MeusMedicamentos.Shared;

namespace MeusMedicamentos.API.Controllers;

[ApiController]
public class MainController : ControllerBase
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

    protected ActionResult CustomResponse<T>(ApiResponse<T> response)
    {
        if (response.Success)
        {
            return StatusCode(response.StatusCode, new
            {
                success = response.Success,
                data = response.Data,
                statusCode = response.StatusCode
            });
        }

        return StatusCode(response.StatusCode, new
        {
            success = response.Success,
            errors = response.Errors,
            statusCode = response.StatusCode
        });
    }

    protected ActionResult CustomResponse(ModelStateDictionary modelState)
    {
        if (!modelState.IsValid) NotificarErroModelInvalida(modelState);
        return CustomResponse(new ApiResponse<string>(400));
    }

    protected void NotificarErroModelInvalida(ModelStateDictionary modelState)
    {
        var erros = modelState.Values.SelectMany(e => e.Errors);
        foreach (var erro in erros)
        {
            var errorMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
            NotificarErro(errorMsg);
        }
    }

    protected void NotificarErro(string mensagem)
    {
        _notificadorErros.Handle(new NotificacaoErros(mensagem));
    }
}
