namespace MeusMedicamentos.Domain.Notifications;

public interface INotificadorErros
{
    void Handle(NotificacaoErros notificacao);
    List<NotificacaoErros> ObterNotificacoes();
    bool TemNotificacoes();
}