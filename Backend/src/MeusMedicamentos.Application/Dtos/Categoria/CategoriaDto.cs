using MeusMedicamentos.Domain.Enums;

namespace MeusMedicamentos.Application.DTOs
{
    public record CategoriaDto(Guid Id, string Nome, EStatus Status);
}
