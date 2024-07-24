using MeusMedicamentos.Domain.Enums;

namespace MeusMedicamentos.Domain.Entities;

public abstract class Entity
{
    protected Entity()
    {
        DataCriacao = DateTime.UtcNow;
        Status = Status.Ativo;
    }

    public int Id { get; protected set; }
    public DateTime DataCriacao { get; protected set; }
    public DateTime? DataModificacao { get; protected set; }
    public string UsuarioCriacao { get; protected set; } = string.Empty;
    public string? UsuarioModificacao { get; protected set; }
    public Status Status { get; protected set; }

    public void SetModificacao(string usuario)
    {
        DataModificacao = DateTime.UtcNow;
        UsuarioModificacao = usuario;
    }

    public void SetStatus(Status status)
    {
        Status = status;
    }
}
