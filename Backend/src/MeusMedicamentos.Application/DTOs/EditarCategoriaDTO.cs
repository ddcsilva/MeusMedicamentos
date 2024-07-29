using MeusMedicamentos.Domain.Enums;

namespace MeusMedicamentos.Application.DTOs
{
    public record EditarCategoriaDTO(Guid Id, string Nome, EStatus Status);
}
