using MeusMedicamentos.Contracts;
using NLog;

namespace MeusMedicamentos.LoggerService;

public class LoggerManager : ILoggerManager
{
    private static ILogger logger = LogManager.GetCurrentClassLogger();

    public LoggerManager()
    {
    }
    
    public void LogInformativo(string mensagem)
    {
        logger.Info(mensagem);
    }
    
    public void LogAdvertencia(string mensagem)
    {
        logger.Warn(mensagem);
    }
    
    public void LogDebug(string mensagem)
    {
        logger.Debug(mensagem);
    }
    
    public void LogErro(string mensagem)
    {
        logger.Error(mensagem);
    }
}