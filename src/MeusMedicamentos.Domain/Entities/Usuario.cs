using Microsoft.AspNetCore.Identity;

namespace MeusMedicamentos.Domain.Entities
{
    public class Usuario : IdentityUser
    {
        public string Nome { get; set; } = string.Empty;
    }
}
