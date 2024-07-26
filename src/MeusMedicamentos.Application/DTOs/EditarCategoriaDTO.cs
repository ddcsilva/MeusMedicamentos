using MeusMedicamentos.Domain.Enums;

namespace MeusMedicamentos.Application.DTOs
{
    public record EditarCategoriaDTO(int Id, string Nome, EStatus Status);
}
