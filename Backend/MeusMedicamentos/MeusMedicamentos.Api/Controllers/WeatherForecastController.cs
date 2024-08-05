using MeusMedicamentos.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace MeusMedicamentos.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly ILoggerManager _logger;
    
    public WeatherForecastController(ILoggerManager logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IEnumerable<string> Get()
    {
        _logger.LogInformativo("Esta é uma mensagem informativa.");
        _logger.LogAdvertencia("Esta é uma mensagem de advertência.");
        _logger.LogDebug("Esta é uma mensagem de debug.");
        _logger.LogErro("Esta é uma mensagem de erro.");

        return new[] { "value1", "value2" };
    }
}