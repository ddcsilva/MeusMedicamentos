using Microsoft.AspNetCore.Identity;
using System;

namespace MeusMedicamentos.Domain.Entities
{
    public class Usuario : IdentityUser<Guid>
    {
        public string Nome { get; set; } = string.Empty;
    }
}
