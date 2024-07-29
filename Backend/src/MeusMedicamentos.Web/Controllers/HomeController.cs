using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MeusMedicamentos.Domain.Notifications;

namespace MeusMedicamentos.Web.Controllers;

[Authorize]
public class HomeController : MainController
{
    public HomeController(INotificadorErros notificadorErros)
        : base(notificadorErros)
    {
    }

    public IActionResult Index()
    {
        return View();
    }
}
