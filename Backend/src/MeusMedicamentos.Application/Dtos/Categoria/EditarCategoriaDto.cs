using MeusMedicamentos.Domain.Enums;

namespace MeusMedicamentos.Application.DTOs
{
    public record EditarCategoriaDto(Guid Id, string Nome, EStatus Status);
}
