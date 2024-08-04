namespace MeusMedicamentos.Domain.Notifications;

public class NotificadorErros : INotificadorErros
{
    private readonly List<NotificacaoErros> _notificacoes;

    public NotificadorErros()
    {
        _notificacoes = new List<NotificacaoErros>();
    }

    public void Handle(NotificacaoErros notificacao)
    {
        _notificacoes.Add(notificacao);
    }

    public List<NotificacaoErros> ObterNotificacoes()
    {
        return _notificacoes;
    }

    public bool TemNotificacoes()
    {
        return _notificacoes.Count != 0;
    }
}