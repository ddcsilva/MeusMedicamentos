namespace MeusMedicamentos.Domain.Entities;

public sealed class Categoria : Entity
{
    public Categoria(string nome)
    {
        Nome = nome;
    }

    public string Nome { get; init; }

    public Usuario UsuarioCriacao { get; init; } = null!;
    public Usuario? UsuarioModificacao { get; init; }
}