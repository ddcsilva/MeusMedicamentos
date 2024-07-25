using MeusMedicamentos.Domain.Enums;

namespace MeusMedicamentos.Application.DTOs
{
    public class EditarCategoriaDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public EStatus Status { get; set; }
    }
}
