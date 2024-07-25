using FluentValidation;
using FluentValidation.Results;
using MeusMedicamentos.Domain.Notifications;

namespace MeusMedicamentos.Application.Services
{
    public abstract class BaseService
    {
        private readonly INotificadorErros _notificadorErros;

        protected BaseService(INotificadorErros notificadorErros)
        {
            _notificadorErros = notificadorErros;
        }

        protected void Notificar(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                Notificar(error.ErrorMessage);
            }
        }

        protected void Notificar(string mensagem)
        {
            _notificadorErros.Handle(new NotificacaoErros(mensagem));
        }

        protected bool ExecutarValidacao<TV, TE>(TV validacao, TE entidade) where TV : AbstractValidator<TE> where TE : class
        {
            var validator = validacao.Validate(entidade);

            if (validator.IsValid) return true;

            Notificar(validator);

            return false;
        }
    }
}
