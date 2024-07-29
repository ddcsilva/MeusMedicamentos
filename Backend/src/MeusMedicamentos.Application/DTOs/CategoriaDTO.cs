using MeusMedicamentos.Domain.Enums;

namespace MeusMedicamentos.Application.DTOs
{
    public record CategoriaDTO(Guid Id, string Nome, EStatus Status);
}
