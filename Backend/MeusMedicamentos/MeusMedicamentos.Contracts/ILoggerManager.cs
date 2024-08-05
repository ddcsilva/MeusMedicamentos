namespace MeusMedicamentos.Contracts;

public interface ILoggerManager
{
    void LogInformativo(string mensagem);
    void LogAdvertencia(string mensagem);
    void LogDebug(string mensagem);
    void LogErro(string mensagem);
}