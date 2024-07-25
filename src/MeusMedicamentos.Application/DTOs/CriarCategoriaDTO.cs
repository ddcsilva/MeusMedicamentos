using MeusMedicamentos.Domain.Enums;

namespace MeusMedicamentos.Application.DTOs
{
    public class CriarCategoriaDTO
    {
        public string Nome { get; set; } = string.Empty;
        public Status Status { get; set; }
    }
}
