using MeusMedicamentos.Domain.Enums;

namespace MeusMedicamentos.Domain.Entities;

public abstract class Entity
{
    protected Entity()
    {
        Id = Guid.NewGuid();
        DataCriacao = DateTime.UtcNow;
        Status = EStatus.Ativo;
    }

    public Guid Id { get; }
    public DateTime DataCriacao { get; }
    public DateTime? DataModificacao { get; private set; }
    public Guid UsuarioCriacaoId { get; private set; }
    public Guid? UsuarioModificacaoId { get; private set; }
    public EStatus Status { get; private set; }

    public void DefinirUsuarioCriacao(Guid usuarioId)
    {
        UsuarioCriacaoId = usuarioId;
    }

    public void DefinirUsuarioModificacao(Guid usuarioId)
    {
        UsuarioModificacaoId = usuarioId;
    }

    public void DefinirDataModificacao()
    {
        DataModificacao = DateTime.UtcNow;
    }
}