using MeusMedicamentos.Domain.Enums;

namespace MeusMedicamentos.Application.DTOs
{
    public record CategoriaDTO(int Id, string Nome, EStatus Status);
}
