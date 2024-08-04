namespace MeusMedicamentos.Domain.Notifications;

public class NotificacaoErros
{
    private readonly string _mensagem;

    public NotificacaoErros(string mensagem)
    {
        _mensagem = mensagem;
    }
}