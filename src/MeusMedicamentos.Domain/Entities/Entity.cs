using MeusMedicamentos.Domain.Enums;

namespace MeusMedicamentos.Domain.Entities
{
    public abstract class Entity
    {
        protected Entity()
        {
            DataCriacao = DateTime.UtcNow;
            Status = EStatus.Ativo;
        }

        public int Id { get; protected set; }
        public DateTime DataCriacao { get; protected set; }
        public DateTime? DataModificacao { get; protected set; }
        public string UsuarioCriacaoId { get; protected set; } = string.Empty;
        public string? UsuarioModificacaoId { get; protected set; }
        public EStatus Status { get; protected set; }

        public void SetStatus(EStatus status)
        {
            Status = status;
        }

        public void SetUsuarioCriacaoId(string usuarioId)
        {
            UsuarioCriacaoId = usuarioId;
        }

        public void SetUsuarioModificacaoId(string usuarioId)
        {
            UsuarioModificacaoId = usuarioId;
        }

        public void SetDataModificacao()
        {
            DataModificacao = DateTime.UtcNow;
        }
    }
}
