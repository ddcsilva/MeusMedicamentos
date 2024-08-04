using Microsoft.AspNetCore.Identity;

namespace MeusMedicamentos.Domain.Entities;

public class Usuario : IdentityUser<Guid>
{
    public string Nome { get; set; } = string.Empty;

    public ICollection<Categoria> CategoriasCriadas { get; init; } = new List<Categoria>();
    public ICollection<Categoria> CategoriasModificadas { get; init; } = new List<Categoria>();
}